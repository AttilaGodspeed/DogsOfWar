using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void playGame() {
		// goes to the play scene
		SceneManager.LoadScene(1);
	}

	public void exitGame() {
		// exits the game
		Application.Quit();
	}
}
