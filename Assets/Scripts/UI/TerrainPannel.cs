using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// manages the elements of the terrain pannel
public class TerrainPannel : MonoBehaviour {

	// references for tile board, terrain table, and camera
	[SerializeField] private Board tileBoard;
	[SerializeField] private TerrainTable table;
	[SerializeField] private Camera mainCamera;

	// text fields for terrain info
	[SerializeField] private Text movementText;
	[SerializeField] private Text defenseText;
	[SerializeField] private Text attackText;
	[SerializeField] private Text typeText;
	[SerializeField] private Text descriptionText;
	// sprite for the terrain being displayed
	[SerializeField] private Image terrainPicture;
	// sprite for this panel
	[SerializeField] private Image panelSprite;

	// 'invisible' and 'visible' sprites
	[SerializeField] private Sprite visible;
	[SerializeField] private Sprite invisible;
	private Vector3 cursor;
	private int x, y, type; 
	private Tile myTile;

	// base text values for move/attack/defense fields
	private readonly string DEFAULT_MOVE = "MOVEMENT: -";
	private readonly string DEFAULT_ATTACK = "ATTACK: ";
	private readonly string DEFAULT_DEFENSE = "DEFENSE: ";
	private readonly string BLANK = " ";

	// capture the board size to prevent lookups if pointer is off the board
	void Start () {	}
	
	// Refresh the Terrain panel based on what the pointer is poiting at
	void Update () {
		// figure out which square the pointer is pointing at
		cursor = mainCamera.ScreenToWorldPoint(Input.mousePosition);

		// convert world coordinates to points for the grid
		x = (int)Mathf.Floor(cursor.x);
		y = (int)Mathf.Floor(cursor.y);

		// poll that square for it's stats and update UI (blank if it returns null)
		myTile = tileBoard.GetTile(x, y);
		if (myTile == null) {
			movementText.text = BLANK;
			defenseText.text = BLANK;
			attackText.text = BLANK;
			typeText.text = BLANK;
			descriptionText.text = BLANK;

			terrainPicture.sprite = invisible;

			panelSprite.sprite = invisible;
		}
		else {
			panelSprite.sprite = visible;

			// translate/query stats and update panel elements
			type = myTile.getType();

			movementText.text = DEFAULT_MOVE + table.moveCost[type];
			attackText.text = DEFAULT_ATTACK + table.attackBonus[type];
			defenseText.text = DEFAULT_DEFENSE + table.defenseBonus[type];

			typeText.text = table.names[type];
			descriptionText.text = table.descriptions[type];

			terrainPicture.sprite = table.sprites[type];
		}

		
	}
}
