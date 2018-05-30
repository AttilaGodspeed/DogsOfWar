using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICommander : MonoBehaviour {
	private bool defeated;

	// heuristic modifiers
	[SerializeField] private int agressive;
	[SerializeField] private int defensive;
	[SerializeField] private int runner;

	[SerializeField] TurnController controller; // link to controller to send end turn notice
	[SerializeField] Board tileBoard;
	[SerializeField] CombatUI combat;

	private Commander myCommander;
	private Unit currentUnit;

	private readonly float delay = 0.5f;

	private int moves, startX, startY, tempInt, transX, transY, f, maxF;
	private NavNode currentNode, tempNode, navList, bestNode;
	private bool[,] closedList;
	private Tile tempTile;
	private NavHeap movesHeap;

	// Use this for initialization
	void Awake () {
		myCommander = gameObject.GetComponent(typeof(Commander)) as Commander;
		defeated = false;
	}
	
	public void startTurn() {

		// if defeated, doesn't get a turn
		if (defeated) {
			controller.endTurn();
			return;
		}
			
		
		currentUnit = myCommander.getUnitStack();
		// if there are no living units... AI is defeated
		defeated = true;

		// iterate through list of units...
		while (currentUnit != null) {
			// if this unit isn't dead...
			if (!currentUnit.isDead()) {
				
				// generate list of moves, get best, and do the move if there is one
				pickMove();
				//moveWithDelay();

				// generate attacks, pick best, and do it if there is one
				pickAttack();
				//attackWithDelay();

				defeated = false;
			}
			// go to the next unit in the list
			currentUnit = currentUnit.nextUnit;
		}

		if (defeated)
			controller.opponentDown();

		// and end turn~
		controller.endTurn();
	}

	private void pickMove() {
		// start with a blank nav list
		navList = null; 
		// init the closed list
		closedList = new bool[13,13];
		// make the NavNode heap(s)
		movesHeap = new NavHeap(60, false);
		
		// get the move allowance from stats: defense/attack/move/HP
		moves = currentUnit.getStats()[2]; 

		// grab x/y for the center/start tile
		startX = currentUnit.getX();
		startY = currentUnit.getY();

		// make a NavNode for the start
		currentNode = new NavNode();
		currentNode.x = startX;
		currentNode.y = startY;
		currentNode.totalMoveCost = 0;
		bestNode = currentNode;

		// calculate transform numbers for the closed list
		transX = startX - 6;
		transY = startY - 6;

		// loop to greedy search out paths
		do {
			// add viable neighbors to the current node to the heap
			// up
			addNeighbor(currentNode.x, (currentNode.y + 1));
			// down
			addNeighbor(currentNode.x, (currentNode.y - 1));
			// left
			addNeighbor((currentNode.x - 1), currentNode.y);
			// right
			addNeighbor((currentNode.x + 1), currentNode.y);

			// add current node to nav list
			currentNode.nextNode = navList;
			navList = currentNode;
			
			// add current node to the closed list
			closedList[(currentNode.x - transX), (currentNode.y - transY)] = true;

			// get the next best node from the heap
			currentNode = movesHeap.pop();

		} while (currentNode != null);

		// go through the navList and find the max f
		maxF = 0;
		while (navList != null) {
			if (navList.f > maxF) {
				maxF = navList.f;
				bestNode = navList;
			}
			navList = navList.nextNode;
		}

		// move the current unit to the best node
		currentUnit.move(bestNode.x, bestNode.y);
	}

	// function to add neighbor tile at given tile coordinates
	private void addNeighbor(int x, int y) {
		tempTile = tileBoard.GetTile(x, y);
		// if the tile is in bounds...
		if (tempTile != null) {
			// calculate total move cost
			tempInt = currentNode.totalMoveCost + tempTile.getMoveCost();
			// and if we have enough move allowance to navigate here...
			if (moves >= tempInt) {
				// and if the coordinates are not in the closed list...
				if (!closedList[(x - transX), (y - transY)]) {
					// and if the tile is unoccupied...
					if (!tempTile.isOccupied()) {
						// make a NavNode for this tile
						tempNode = new NavNode(tempTile);
						// update it's move cost
						tempNode.totalMoveCost = tempInt;

						// calculate it's f
						f = agressive * agressive - getMinDistance(x, y);
						f += defensive * tempTile.getDefenseMod();
						f += runner * tempInt;
						tempNode.f = f;

						// add it to the heap
						movesHeap.push(tempNode);
					}
				}
			}
		}
	}

	// gets manhattan distance to the closest player (array 0) controlled unit
	private int getMinDistance(int curX, int curY) {
		int minD = 9999;
		int thisD = minD;
		Unit tempUnit = controller.GetCommander(0).getUnitStack();

		// go through stack and find the min distance
		while (tempUnit != null) {
			// if the unit isn't dead...
			if (!tempUnit.isDead()) {
				thisD = Mathf.Abs(curX - tempUnit.getX()) + Mathf.Abs(curY - tempUnit.getY());

				if (thisD < minD)
					minD = thisD;
			}

			// get next unit
			tempUnit = tempUnit.nextUnit;
		}

		return minD;
	}

	private void pickAttack() {
		Unit target = null;
		Unit lookUnit;
		int ux = currentUnit.getX();
		int uy = currentUnit.getY();
		int t = 9999;
		int v;
		int[] stats;
		Tile lookTile;

		// look in each direction for a unit to slay
		lookTile = tileBoard.GetTile(ux, uy +1);
		if (lookTile != null) {
			if (lookTile.isOccupied()) {
				lookUnit = lookTile.GetUnit();
				if (!lookUnit.GetCommander().isAI()) {
					stats = lookUnit.getStats(); // defense/attack/move/HP
					v = stats[0] + stats[3];
					if (v < t) {
						t = v;
						target = lookUnit;
					}
				}
			}
		}

		lookTile = tileBoard.GetTile(ux, uy - 1);
		if (lookTile != null) {
			if (lookTile.isOccupied()) {
				lookUnit = lookTile.GetUnit();
				if (!lookUnit.GetCommander().isAI()) {
					stats = lookUnit.getStats(); // defense/attack/move/HP
					v = stats[0] + stats[3];
					if (v < t) {
						t = v;
						target = lookUnit;
					}
				}
			}
		}

		lookTile = tileBoard.GetTile(ux + 1, uy);
		if (lookTile != null) {
			if (lookTile.isOccupied()) {
				lookUnit = lookTile.GetUnit();
				if (!lookUnit.GetCommander().isAI()) {
					stats = lookUnit.getStats(); // defense/attack/move/HP
					v = stats[0] + stats[3];
					if (v < t) {
						t = v;
						target = lookUnit;
					}
				}
			}
		}

		lookTile = tileBoard.GetTile(ux - 1, uy);
		if (lookTile != null) {
			if (lookTile.isOccupied()) {
				lookUnit = lookTile.GetUnit();
				if (!lookUnit.GetCommander().isAI()) {
					stats = lookUnit.getStats(); // defense/attack/move/HP
					v = stats[0] + stats[3];
					if (v < t) {
						t = v;
						target = lookUnit;
					}
				}
			}
		}

		// attack if viable target
		if (target != null)
			combat.combat(currentUnit, target);
	}

	IEnumerator moveWithDelay() {
		yield return new WaitForSecondsRealtime(0.5f);
		pickMove();
	}

	IEnumerator attackWithDelay() {
		yield return new WaitForSecondsRealtime(0.5f);
		pickAttack();
	}

	public bool isDefeated() {
		return defeated;
	}
}
