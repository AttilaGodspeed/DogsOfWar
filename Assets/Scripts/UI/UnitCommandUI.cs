using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCommandUI : MonoBehaviour {

	
	[SerializeField] private Button moveButton;
	[SerializeField] private Button attackButton;
	[SerializeField] private Button restButton;
	[SerializeField] private Button cancelButton;
	[SerializeField] private MoveGenerator moveGenerator;
	[SerializeField] private AttackGenerator attackGenerator;


	[SerializeField] private Image unitImage;
	[SerializeField] private Text moveText;
	[SerializeField] private Text defText;
	[SerializeField] private Text atkText;
	[SerializeField] private Text hpText;


	private Unit myUnit;
	private int[] stats;

	// Use this for initialization
	void Start () {
		closeUI();
	}

	// open the UI for the selected unit
	public void openUI(Unit unit) {
		myUnit = unit;
		
		moveGenerator.clearMoves();

		stats = myUnit.getStats(); // defense/attack/move/HP

		// set all UI elements to match the unit
		unitImage.gameObject.SetActive(true);
		unitImage.sprite = myUnit.GetSprite();
		moveText.text = "MVMT: " + stats[2];
		defText.text = "DEF: " + stats[0];
		atkText.text = "ATK: " + stats[1];
		hpText.text = "HP: " + stats[3];

		// activate all buttons
		if (myUnit.isCanMove())
			moveButton.gameObject.SetActive(true);
		if (myUnit.isCanAttack())
			attackButton.gameObject.SetActive(true);
		
		restButton.gameObject.SetActive(true);
		cancelButton.gameObject.SetActive(true);		
	}

	public void closeUI() {
		// close all the things
		moveGenerator.clearMoves();

		unitImage.gameObject.SetActive(false);
		moveText.text = "";
		defText.text = "";
		atkText.text = "";
		hpText.text = "";

		moveButton.gameObject.SetActive(false);
		attackButton.gameObject.SetActive(false);
		restButton.gameObject.SetActive(false);
		cancelButton.gameObject.SetActive(false);
	}

	public void moveCommand() {
		moveButton.gameObject.SetActive(false);
		moveGenerator.generateMoves(myUnit);

		// check if all actions used up
		if (!myUnit.isCanAttack() && !myUnit.isCanMove())
			closeUI();
	}
	
	public void attackCommand() {
		attackButton.gameObject.SetActive(false);
		attackGenerator.placeTiles(myUnit);
		
		// check if all actions used up
		if (!myUnit.isCanAttack() && !myUnit.isCanMove())
			closeUI();
	}

	public void restCommand() {
		myUnit.rest();
		closeUI();
	}
}
