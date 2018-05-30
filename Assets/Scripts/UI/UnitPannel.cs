using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitPannel : MonoBehaviour {

	// references to other game objects
	[SerializeField] private Camera mainCamera;
	[SerializeField] private Board tileBoard;

	// UI elements
	[SerializeField] private Text movementText;
	[SerializeField] private Text defenseText;
	[SerializeField] private Text attackText;
	[SerializeField] private Text hpText;
	[SerializeField] private Text typeText;
	//[SerializeField] private Text descriptionText;
	[SerializeField] private Image unitPicture; // sprite for the unit being displayed
	[SerializeField] private Image panelSprite; // sprite for this panel

	// 'invisible' and 'visible' sprites
	[SerializeField] private Sprite visible;
	[SerializeField] private Sprite invisible;

	// base text values for move/attack/defense fields
	private readonly string DEFAULT_MOVE = "MOVEMENT: ";
	private readonly string DEFAULT_ATTACK = "ATTACK: ";
	private readonly string DEFAULT_DEFENSE = "DEFENSE: ";
	private readonly string DEFAULT_HP = "HP: ";
	private readonly string BLANK = " ";

	private int x, y;
	private Vector3 cursor;
	private Tile myTile;
	private Unit myUnit;
	private int[] unitStats; // unit's stats: defense/attack/move/HP

	// Use this for initialization
	void Start () {
		
	}
	
	
	void Update () {
		// figure out which square the pointer is pointing at
		cursor = mainCamera.ScreenToWorldPoint(Input.mousePosition);

		// convert world coordinates to points for the grid
		x = (int)Mathf.Floor(cursor.x);
		y = (int)Mathf.Floor(cursor.y);

		// poll that square for it's stats and update UI (blank if it returns null)
		myTile = tileBoard.GetTile(x, y);
		if (myTile != null) {
			if(myTile.isOccupied()) {
				myUnit = myTile.GetUnit();
				// get unit's stats: defense/attack/move/HP
				unitStats = myUnit.getStats();
				
				// update UI components
				defenseText.text = DEFAULT_DEFENSE +  unitStats[0];
				attackText.text = DEFAULT_ATTACK +  unitStats[1];
				movementText.text = DEFAULT_MOVE +  unitStats[2];
				hpText.text = DEFAULT_HP + unitStats[3];

				typeText.text = myUnit.getType();
				unitPicture.sprite = myUnit.GetSprite();

				panelSprite.sprite = visible;
			}
			else {
				// hide all UI components
				hideUI();
			}
		}
		else {
			// hide all UI components
			hideUI();
		}
	}

	// function to hide all UI elements for the panel
	private void hideUI() {
		attackText.text = BLANK;
		defenseText.text = BLANK;
		movementText.text = BLANK;
		hpText.text = BLANK;
		typeText.text = BLANK;

		unitPicture.sprite = invisible;
		panelSprite.sprite = invisible;
	}
}
