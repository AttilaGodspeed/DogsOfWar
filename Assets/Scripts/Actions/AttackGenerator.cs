using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGenerator : MonoBehaviour {

	[SerializeField] private Board tileBoard;

	[SerializeField] private AttackTile atkUp;
	[SerializeField] private AttackTile atkDown;
	[SerializeField] private AttackTile atkLeft;
	[SerializeField] private AttackTile atkRight;

	private Tile tempTile;

	private int x, y;

	// Use this for initialization
	void Start () {
		//disable all tiles
		clearTiles();
	}
	
	public void placeTiles(Unit unit) {
		x = unit.getX();
		y = unit.getY();

		// place tiles where appropriate units exist
		if (queryTile(x, y+1)) 
			atkUp.deploy(unit, 0, 1);
		if (queryTile(x, y-1))
			atkDown.deploy(unit, 0, -1);
		if (queryTile(x-1, y))
			atkLeft.deploy(unit, -1, 0);
		if (queryTile(x+1, y))
			atkRight.deploy(unit, 1, 0);
	}

	public void clearTiles() {
		atkDown.gameObject.SetActive(false);
		atkLeft.gameObject.SetActive(false);
		atkRight.gameObject.SetActive(false);
		atkUp.gameObject.SetActive(false);
	}

	private bool queryTile(int qx, int qy) {
		tempTile = tileBoard.GetTile(qx, qy);
		if (tempTile != null) {
			if (tempTile.isOccupied()) {
				return tempTile.GetUnit().GetCommander().isAI();
			}
		}
		return false;
	}
}
