using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTile : MonoBehaviour {

	[SerializeField] AttackGenerator attackGenerator;
	[SerializeField] Board tileBoard;
	[SerializeField] CombatUI comUI;
	private Transform myTransform;
	private Unit myUnit, target;
	private Tile tempTile;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.GetComponent(typeof(Transform)) as Transform;
	}
	
	void OnMouseDown() {
		// fetch the defender
		int x = (int)Mathf.Floor(myTransform.position.x);
		int y = (int)Mathf.Floor(myTransform.position.y);
		tempTile = tileBoard.GetTile(x, y);
		target = tempTile.GetUnit();

		// summon the combat UI, clear tiles
		comUI.combat(myUnit, target);
		attackGenerator.clearTiles();
	}

	public void deploy(Unit unit, int xOff, int yOff) {
		gameObject.SetActive(true);
		myTransform.position = new Vector2(unit.getX() + xOff + 0.5f, unit.getY() + yOff + 0.5f);
		myUnit = unit;
	}
}
