using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTileStack : MonoBehaviour {

	
	private MoveTile deployedStack; // the first tile deployed, and permanent head of the stack
	private MoveTile headTile, tempTile; // current head of the stack, and a temp tile for deployment

	// Use this for initialization
	void Awake () {
		// intialize anything that needs to
		headTile = null; 
		deployedStack = null;
	}
	
	public void pushTile(MoveTile tile) {
		// add this tile to the stack
		tile.setNextTile(headTile);
		headTile = tile;
	}

	public void deployTile(int x, int y, Unit myUnit) { // links to the unit it's deploying for
		// remove tile from the main stack
		tempTile = headTile;

		// if we have tiles still in stack...
		if (headTile != null) {
			headTile = tempTile.getNextTile();
		}

		// add removed tile to the deployed stack
		tempTile.setNextTile(deployedStack);
		deployedStack = tempTile;

		// deploy the tile to the specified coordinates
		deployedStack.deploy(x, y, myUnit);
	}

	public void clearTiles() {
		// iterate through deployed tiles list and remove them/add them back into the stack
		while (deployedStack != null) {
			tempTile = deployedStack;
			deployedStack = deployedStack.getNextTile();

			tempTile.clear();
		}
	}
}
