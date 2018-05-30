using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode {//: MonoBehaviour {
	 
	public NavNode nextNode;
	public int totalMoveCost, x, y; // common algorithm parameters
	public int minDistanceEnemy, defense, f; // AI algorithm parameters

	public NavNode () {

	}

	// overwritten constructor to pull data from a tile directly
	public NavNode(Tile myTile) {
		totalMoveCost = myTile.getMoveCost();
		x = myTile.getX();
		y = myTile.getY();
		defense = myTile.getDefenseMod();
	}

}
