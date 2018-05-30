using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgamePannel : MonoBehaviour {

	[SerializeField] private Image endgamePannel;
	[SerializeField] private Text endgameText;
	[SerializeField] private Button menuButton;

	[SerializeField] private Sprite visible;
	[SerializeField] private Sprite invisible;

	private bool isActive, isToggleable;
	
	private readonly string VICTORY = "Congratulations your enemies have been utterly destroyed!";
	private readonly string DEFEAT = "All your forces have fallen to the enemy... you are defeated.";
	private readonly string RETURN = "  Return to the menu to play again or exit the game";
	private readonly string PAUSED = "GAME PAUSED, Press escape to return to game.";

	void Start () {
		// start hidden
		isActive = false;
		isToggleable = true;
		endgameText.text = "";
		menuButton.gameObject.SetActive(false);
		endgamePannel.sprite = invisible;
	}

	void Update () {
		// toggle menu with escape if able
		if (isToggleable) {
			if (Input.GetKeyDown("escape")) {
				//Debug.Log("Key Pressed!");
				isActive = !isActive;
				if (isActive) {
					endgameText.text = PAUSED;
					endgamePannel.sprite = visible;
				}
				else {
					endgameText.text = "";
					endgamePannel.sprite = invisible;
				}
				
				menuButton.gameObject.SetActive(isActive);
			}
		}
	}
	
	private void activateUI() {
		isActive = true;
		menuButton.gameObject.SetActive(true);
		endgamePannel.sprite = visible;
	}

	public void victory() {
		isToggleable = false;
		activateUI();
		endgameText.text = VICTORY + RETURN;
	}

	public void defeat() {
		isToggleable = false;
		activateUI();
		endgameText.text = DEFEAT + RETURN;
	}



}
