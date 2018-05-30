using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCommands : MonoBehaviour {

	[SerializeField] private UnitCommandUI commandUI;
	private Unit myUnit;

	// Auto-Linki self to the object's Unit script
	void Start () {
		myUnit = gameObject.GetComponent(typeof(Unit)) as Unit;
	}
	
	// activates UI when clicked on, but only if the UI isn't already engaged
	void OnMouseDown() {
		// is it this unit's commander's turn?
		if (myUnit.GetCommander().isTurn()) {
			// if the unit can move and/or attack, open the command UI
			if (myUnit.isCanAttack() || myUnit.isCanMove())
				commandUI.openUI(myUnit);
		}
	}
}
