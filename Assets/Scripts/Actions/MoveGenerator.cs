using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGenerator : MonoBehaviour {

	[SerializeField] private Board tileBoard;
	[SerializeField] private MoveTileStack tileStack;

	private int moves, startX, startY, tempInt, transX, transY;
	private NavNode currentNode, tempNode, navList;
	private bool[,] closedList;
	private Tile tempTile;
	private NavHeap heap;

	// Use this for initialization
	void Start () {
		
	}

	public void clearMoves() {
		// clears any deployed move tiles
		tileStack.clearTiles();
	}

	public void generateMoves(Unit myUnit) {
		// start with a blank nav list
		navList = null; 
		// init the closed list
		closedList = new bool[13,13];
		// make a NavNode heap
		heap = new NavHeap(60, false);

		// get the move allowance from stats: defense/attack/move/HP
		moves = myUnit.getStats()[2];  

		// grab x/y for the center/start tile
		startX = myUnit.getX();
		startY = myUnit.getY();

		// make a NavNode for the start
		currentNode = new NavNode();
		currentNode.x = startX;
		currentNode.y = startY;
		currentNode.totalMoveCost = 0;

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
			//Debug.Log("Adding tile (" + (currentNode.x - transX - 6) + ","+ 
						//(currentNode.y - transY - 6) + ") to closed list.");
			closedList[(currentNode.x - transX), (currentNode.y - transY)] = true;

			// get the next best node from the heap
			currentNode = heap.pop();

		} while (currentNode != null);

		// deploy the list of navigatable nodes (except the last one (starting node))
		while (navList.nextNode != null) {
			// deploy a move indicator to location at nav node's location
			tileStack.deployTile(navList.x, navList.y, myUnit);
			// get next node in the list
			navList = navList.nextNode;
		}
	}

	// function to add neighbor tile at given tile coordinates
	private void addNeighbor(int x, int y) {
		tempTile = tileBoard.GetTile(x, y);
		//Debug.Log("Examining Tile at : (" + x + "," + y + ")");
		// if the tile is in bounds...
		if (tempTile != null) {
			// calculate total move cost
			tempInt = currentNode.totalMoveCost + tempTile.getMoveCost();
			//Debug.Log("Total Move Cost is: " + tempInt);
			// and if we have enough move allowance to navigate here...
			if (moves >= tempInt) {
				// and if the coordinates are not in the closed list...
				//Debug.Log("Tile corresponds to closedlist: " + (x - transX -6) + ", " + (y - transY -6));
				if (!closedList[(x - transX), (y - transY)]) {
					// and if the tile is unoccupied...
					if (!tempTile.isOccupied()) {
						// make a NavNode for this tile
						tempNode = new NavNode(tempTile);
						// update it's move cost
						tempNode.totalMoveCost = tempInt;
						// add it to the heap
						heap.push(tempNode);
					}
				}
			}
		}
	}
}
