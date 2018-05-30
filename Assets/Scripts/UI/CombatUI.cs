using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour {

	[SerializeField] private Text atk;
	[SerializeField] private Text def;
	[SerializeField] private Text dmg;
	[SerializeField] private Text hp;

	[SerializeField] private Image atkImage;
	[SerializeField] private Image defImage;

	[SerializeField] private Button closeButton;

	private int temp, mod;

 	//private Unit attacker, defender;

	// Use this for initialization
	void Start () {
		close();
	}
	
	public void close() {
		// close/hide all elements
		atk.text = "";
		def.text = "";
		dmg.text = "";
		hp.text = "";

		atkImage.gameObject.SetActive(false);
		defImage.gameObject.SetActive(false);

		closeButton.gameObject.SetActive(false);

		gameObject.SetActive(false);
	}

	public void combat(Unit attacker, Unit defender) {
		gameObject.SetActive(true);

		// load info into UI elements
		atkImage.gameObject.SetActive(true);
		atkImage.sprite = attacker.GetSprite();
		defImage.gameObject.SetActive(true);
		defImage.sprite = defender.GetSprite();

		attacker.hasAttacked();

		// determine type-based attack modifier
		mod = 0;
		if (attacker.getType() == "Cavalry") {
			if (defender.getType() == "Infantry")
				mod = 3;
			else if (defender.getType() == "Mages")
				mod = -3;
		}
		else if (attacker.getType() == "Infantry") {
			if (defender.getType() == "Mages")
				mod = 3;
			else if (defender.getType() == "Cavalry")
				mod = -3;
		}
		else if (attacker.getType() == "Mages") {
			if (defender.getType() == "Cavalry")
				mod = 3;
			else if (defender.getType() == "Infantry")
				mod = -3;
		}

		// defense/attack/move/HP
		atk.text = "ATK: " + (attacker.getStats()[1] + mod);
		def.text = "DEF: " + defender.getStats()[0];

		temp = attacker.getStats()[1] - defender.getStats()[0] + mod;
		// no nedative damage, lol XD
		if (temp < 0)
			temp = 0;

		dmg.text = "ATTACK DMG: " + temp;
		hp.text = "CURRENT HP: " + (defender.getStats()[3] - temp);
		defender.damage(temp);

		// activate close button
		closeButton.gameObject.SetActive(true);
	}
}
