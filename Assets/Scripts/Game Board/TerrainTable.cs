using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// table to reference terrain numerical type to strings with names and sprites
public class TerrainTable : MonoBehaviour {

	public string[] names;
	public string[] descriptions;
	public Sprite[] sprites;

	public int[] moveCost;
	public int[] attackBonus;
	public int[] defenseBonus;

}
