using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnController : MonoBehaviour {

	[SerializeField] private Commander[] commanders; // list of commanders, player must be 0
	private int current, troopCount;
	private bool showButton;
	private Unit tempUnit;

	[SerializeField] Text commanderText;
	[SerializeField] Image commanderImage;
	[SerializeField] Button endTurnButton;

	[SerializeField] EndgamePannel endgamePannel;

	private int opponents; // tracker of how many ai's are left, for victory conditions

	void Start () {
		current = 0;
		opponents = commanders.Length - 1;
		startTurn();
	}

	private void updateUI() {
		// put current commander's info in the turn UI
		commanderText.text = commanders[current].getName();
		commanderImage.sprite = commanders[current].getPortrait();

		// only display end turn button if it's player's turn
		showButton = !commanders[current].isAI();
		endTurnButton.gameObject.SetActive(showButton);
	}

	private void startTurn() {
		commanders[current].newTurn();
		commanders[current].turnOn();
		updateUI();
	}

	public void endTurn() {
		// button does this, signals end of turn by ai or player
		commanders[current].turnOff();
		
		current++;
		// wrap to beginning if we're at the end
		if (!(current < commanders.Length))
			current  = 0;

		// check for loss condition for player: ANNILIATION!
		if (current == 0) {
			troopCount = 0;
			tempUnit = commanders[0].getUnitStack();
			// count living units
			while (tempUnit != null) {
				if (!tempUnit.isDead())
					troopCount ++;
				tempUnit = tempUnit.nextUnit;
			}
			if (troopCount == 0) {
				// do losing stuff
				endgamePannel.defeat();
				return;
			}
		}

		startTurn();
	}	

	public Commander GetCommander(int index) {
		return commanders[index];
	}

	public void opponentDown() {
		opponents --;
		// check for victory
		if (opponents <= 0) {
			// do victory stuffs
			endgamePannel.victory();
		}
	}

	public void returnToMenu() {
		// change to the menu scene
		SceneManager.LoadScene(0);
	}
}
