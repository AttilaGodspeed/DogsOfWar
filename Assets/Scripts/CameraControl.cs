using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	// the speed the camera will move when mouse is at edge of the screen
	[SerializeField] private float scrollSpeed = 3f;
	// how close (in pixels) the mouse needs to be to the edge for panning ot occur
	[SerializeField] private int buffer = 10;

	// the dimensions of the camera view in pixels
	private int width, height;
	// the transform for this object (camera)
	private Transform myTransform;

	// variables for calculations
	private Vector3 cursor;
	private float clock, leftRight, upDown;

	// Use this for initialization
	void Start () {
		myTransform = transform;
		// get boundary info for the visible area
		width = Screen.width;
		height = Screen.height;
		// TODO: get boundary info for the game board as a whole	
	}
	
	// Update is called once per frame
	void Update () {
		// get mouse position
		cursor = Input.mousePosition;
		// get deltaTime
		clock = scrollSpeed * Time.deltaTime;
		// init move variables;
		leftRight = myTransform.position.x;
		upDown = myTransform.position.y;

		// check left/right panning
		if (cursor.x <= (0 + buffer)) {
			leftRight -= clock;
			//Debug.Log("moving left...");
		}
		else if (cursor.x >= (width - buffer)) {
			leftRight += clock;
			//Debug.Log("moving right...");
		}
		// TODO: clamp x position to the bounds of the game area

		// check up/down panning
		if (cursor.y >= (height - buffer)) {
			upDown += clock;
			//Debug.Log("moving up...");
		}
		else if (cursor.y <= (0 + buffer)) {
			upDown -= clock;
			//Debug.Log("moving down...");
		}
		// TODO: clamp y position to the bounds of the game area

		// update camera position
		myTransform.position = new Vector3(leftRight, upDown, -10f);
	}
}
