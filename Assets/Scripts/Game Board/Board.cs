// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	// number of tiles in height/width in the game board
	[SerializeField] private int height; 
	[SerializeField] private int width; 
	private Tile[,] tiles;

	private int tileCounter, tileMax; // methods for counting tiles;

	void Awake () {
		tileCounter = 0;
		tileMax = height * width;
		tiles = new Tile[width, height];
	}

	// function to add a tile to the board
	public void addTile(int x, int y, Tile _tile) {
		tiles[x,y] = _tile;
		tileCounter++; // update count so we know when it's full
	}

	// returns if we've added all the tiles yet or not
	public bool isFull() {
		return tileCounter >= tileMax;
	}

	public int getWidth() {
		return width;
	}
	public int getHeight() {
		return height;
	}

	public Tile GetTile(int x, int y) {
		if (x < width && x >= 0 && y < height && y >= 0)
			return tiles[x, y];
		else
			return null;
	}
	/*// x, y =  coordinate of center tile, size = tiles from center to grab
	public GhostTile[,] getSubBoard(int x, int y, int size) {

		int xMin = x - size;
		int xMax = x + size;
		int yMin = y - size;
		int yMax = y + size;

		// clamp min/max to the bounds of the board
		if (xMin < 0)
			xMin = 0;
		if (xMax >= width)
			xMax = width - 1;

		if (yMin < 0)
			xMin = 0;
		if (yMax >= height)
			yMax = height -1;

		// create sub-board based off sizes
		GhostTile[,] temp = new GhostTile[xMax - xMin + 1, yMax - yMin +1];
		
		// loop to create sub-board
		for (int i = 0; i < temp.GetLength(1); i++) {
			for(int j = 0; j < temp.GetLength(0); j++) {
				temp[i,j] = tiles[xMin + i, yMin + j].makeGhost();
			}
		}

		return temp;
	}*/


}
