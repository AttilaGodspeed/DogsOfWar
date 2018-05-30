using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour {

	[SerializeField] private MoveTileStack tileStack;
	private Transform myTransform;
	private Unit myUnit; // keeps track of unit so can move it when it's clicked

	private MoveTile nextTile; // link for link listing

	void Start () {
		myTransform = gameObject.GetComponent(typeof(Transform)) as Transform;
		// add self to the tile stack
		nextTile = null;
		clear();
	}

	void OnMouseDown() { // when click on, move the unit to this position, the clear all the tiles
		myUnit.move((int)Mathf.Floor(myTransform.position.x), (int)Mathf.Floor(myTransform.position.y));
		tileStack.clearTiles();
	}

	public void clear() {
		// clear tile from the board and add it to the MoveTileStack
		tileStack.pushTile(this);
		myUnit = null;
		gameObject.SetActive(false);
	}

	public void deploy(int x, int y, Unit unit) {
		myUnit = unit;
		// deploy the tile to specified coordinates
		gameObject.SetActive(true);
		myTransform.position = new Vector2(x + 0.5f, y + 0.5f);
	}
	public void setNextTile(MoveTile tile) {
		nextTile = tile;
	}
	public MoveTile getNextTile() {
		return nextTile;
	}
}
