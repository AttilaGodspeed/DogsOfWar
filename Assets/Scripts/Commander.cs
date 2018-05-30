using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour {

	//[SerializeField] TurnController controller;
	[SerializeField] private string commanderName;
	[SerializeField] private Sprite portrait;
	[SerializeField] private bool ai;
	private AICommander aiComponent;
	

	private Unit unitStack, tempUnit;
	private int units;
	private bool turn;

	void Awake () {
		units = 0;
		turn = true;

		if (ai)
			aiComponent = gameObject.GetComponent(typeof(AICommander)) as AICommander;
	}

	public void pushUnit(Unit unit) {
		// add a unit to the stack
		unit.nextUnit = unitStack;
		unitStack = unit;
		units ++;
	}

	public void newTurn() {
		// refresh actions for all units still alive
		tempUnit = unitStack;
		while (tempUnit != null) {
			if (!tempUnit.isDead()) {
				tempUnit.refresh();
			}
			tempUnit = tempUnit.nextUnit;
		}

		// start AI behavior if relevent
		if (ai)
			aiComponent.startTurn();
	}

	public void killedUnit() {
		units --;
	}

	public Unit getUnitStack() {
		return unitStack;
	}

	public bool isAI() {
		return ai;
	}

	public bool isTurn() {
		return turn;
	}
	public void turnOn() {
		turn = true;
	}
	public void turnOff() {
		turn = false;
	}

	public string getName() {
		return commanderName;
	}
	public Sprite getPortrait() {
		return portrait;
	}
}
