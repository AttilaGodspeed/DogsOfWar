using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	[SerializeField] private Tile startTile;
	[SerializeField] private string type; // string name for the type of this unit
	[SerializeField] private int movement = 15;
	[SerializeField] private int attack = 20;
	[SerializeField] private int defense = 5;
	[SerializeField] private int HP = 40;

	[SerializeField] private Commander commander;

	// links to objects 'n stuff
	private Transform location;
	private Tile myTile;
	[SerializeField] private Sprite mySprite;
	[SerializeField] private Board tileBoard;

	public Unit nextUnit;
	private bool dead;

	// calculation variables
	private int currentAttack, currentDefense, currentHP, x, y;

	// action counters
	private bool canMove, canAttack, hasTurn;	

	void Start () {
		dead = false;
		location = gameObject.transform;
		currentHP = HP;
		canMove = true; // kill these?
		canAttack = true;
		// add self to the tile it's sitting on
		x = (int)Mathf.Floor(location.position.x);
		y = (int)Mathf.Floor(location.position.y);
		
		myTile = startTile;
		startTile.occupy(this);
		// update current attack/defense
		currentAttack = attack + myTile.getAttackMod();
		currentDefense = defense + myTile.getDefenseMod();

		// add self to commander's unit list
		commander.pushUnit(this);
	}
	
	// move the unit to the designated tile
	public void move (int targetX, int targetY) {
		location.position = new Vector2((targetX + 0.5f), (targetY + 0.5f));
		myTile.vacate();
		
		//Debug.Log("occupying tile at: (" + targetX + "," + targetY +")");
		// update myTile
		myTile = tileBoard.GetTile(targetX, targetY);
		if (myTile != null)
			myTile.occupy(this);
		else
			Debug.Log("Cannot attach self to null tile!!");						

		// update current attack/defense
		currentAttack = attack + myTile.getAttackMod();
		currentDefense = defense + myTile.getDefenseMod();

		// update x/y
		x = targetX;
		y = targetY;

		// update canMove
		canMove = false;
	}

	public void damage(int dmg) { 
		currentHP -= dmg;
		if (currentHP <= 0) {
			dead = true;
			myTile.vacate();
			gameObject.SetActive(false);
			commander.killedUnit();
		}
	}

	public void refresh() {
		canAttack = true;
		canMove = true;
	}

	// returns an array of the current stats of this unit:
	// defense/attack/move/HP
	public int[] getStats() {
		return new int[] {currentDefense, currentAttack, movement, currentHP};
	}

	public void rest() {
		canMove = false;
		canAttack = false;
	}

	public int getX() {
		return x;
	}
	public int getY() {
		return y;
	}

	public Sprite GetSprite() {
		return mySprite;
	}

	public string getType() {
		return type;

	}

	public bool isCanMove() {
		return canMove;
	}
	public void hasAttacked() {
		canAttack = false;
	}
	public bool isCanAttack() {
		return canAttack;
	}

	public Commander GetCommander() {
		return commander;
	}

	public bool isDead() {
		return dead;
	}
}
