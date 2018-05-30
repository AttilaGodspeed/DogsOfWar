//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

// Terrain Tile class, holds terrain data and info for pathfinding

public class Tile : MonoBehaviour {

	// the game board to which this tile belongs
	[SerializeField] private Board gameBoard;

	// the position of the Tile, in the grid of Tiles
	private Vector3 location;
	private int x, y;
	// if the Tile is occupied by a unit or not
	//private bool occupied;
	// the movement cost of traversing this Tile, set in inspector
	[SerializeField] private int moveCost = 5;
	// the attack bonus/penalty this tile adds to the occupying unit
	[SerializeField] private int attackMod = 0;
	// the defense bonus/penalty this tile adds to the occupying unit
	[SerializeField] private int defenseMod = 0;
	// numerical type, referening the type table for name/sprite info
	[SerializeField] private int type = 0;

	// pointer to the unit that's sitting on this tile
	private Unit occupyingUnit;

	// initialize position, occupation is updated by units themselves
	void Awake () {
		// TODO: code to convert/pull position
		location = transform.position;
		x = (int)Mathf.Floor(location.x);
		y = (int)Mathf.Floor(location.y);
		occupyingUnit = null;
	}

	// Add this tile to the board array
	void Start () {
		gameBoard.addTile(x, y, this);
	}

	// returns an array, with a set of values representing it's stats
	// move cost / attack bonus / defense bonus / type
	public int getType() {
		return type;
	}

	// method to create a Ghost Tile Copy of this tile
	/*// is this needed?
	public GhostTile makeGhost () {
		return new GhostTile(x, y, moveCost, occupied);
	}*/

	// methods to get/set occupied status
	// TODO: needs to be passed a pointer to the unit that's occupying
	public void occupy(Unit unit) {
		occupyingUnit = unit;
	}
	public void vacate() {
		occupyingUnit = null;
	}
	public bool isOccupied() {
		return occupyingUnit != null;
	}

	public Unit GetUnit() {
		return occupyingUnit;
	}

	public int getAttackMod() {
		return attackMod;
	}
	public int getDefenseMod() {
		return defenseMod;
	}
	public int getMoveCost() {
		return moveCost;
	}

	public int getX() {
		return x;
	}
	public int getY() {
		return y;
	}
}
