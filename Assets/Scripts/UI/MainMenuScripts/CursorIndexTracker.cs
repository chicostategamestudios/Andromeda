//Original Author: Michael Kennedy? || Last Edited: Alexander Stamatis [A.S] | Modified on Feb 19, 2017

using UnityEngine;
using System.Collections;

public class CursorIndexTracker : MonoBehaviour {
	public int currentCursorIndex;
	public int cursorIndexMax;
	public int[] cursorYPositions;
	public int[] cursorXPositions;

	//Added this awake function [A.S.]
	//issue with most of the menu functions is that the inspector connections break
	void Awake(){

		//if this gameobject that this script is attached to, do the following
		if(this.gameObject.name == "PauseMenu"){
			cursorIndexMax = 3;

			//Assigning Y stuff
			cursorYPositions = new int[4];
			cursorYPositions [0] = 5;
			cursorYPositions [1] = 10;
			cursorYPositions [2] = 10;
			cursorYPositions [3] = 10;

			//Assigning X stuff
			cursorXPositions = new int[4];
			cursorXPositions [0] = 5;
			cursorXPositions [1] = -25;
			cursorXPositions [2] = -25;
			cursorXPositions [3] = -25;
		}

		if (this.gameObject.name == "AreYouSureMenu") {
			cursorIndexMax = 2;

			//Assigning Y stuff
			cursorYPositions = new int[3];
			cursorYPositions [0] = 5;
			cursorYPositions [1] = 10;
			cursorYPositions [2] = 10;

			//Assigning X stuff
			cursorXPositions = new int[3];
			cursorXPositions [0] = 5;
			cursorXPositions [1] = -25;
			cursorXPositions [2] = -25;
		}
	}
}
