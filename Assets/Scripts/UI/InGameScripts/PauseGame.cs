//Original Author: Michael Kennedy? || Last Edited: Alexander Stamatis [A.S] | Modified on Feb 19, 2017

using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	[SerializeField]
	private GameObject pauseMenu;
	[SerializeField]
	private GameObject yesNoMenu;

	//Added this awake function [A.S.]
	private void Awake()
	{
		//Assign PauseMenu OBJ - A.S.
		if (pauseMenu == null)
		{
			//Get GameObject named "GUI 1", go to first child named "Canvas" then get child #3 named "Pause Menu"
			//Its important that the PauseMenu is in the correct hierarchy under parent "Canvas"
			if (GameObject.Find("GUI 1").transform.GetChild(0).GetChild(2).gameObject)
			{
				pauseMenu = GameObject.Find("GUI 1").transform.GetChild(0).GetChild(2).gameObject;
			}
		} else
		{
			Debug.LogError("pauseMenu is not assigned");
		}
		//Assign yesNoMenu OBJ - A.S.
		if (yesNoMenu == null)
		{
			yesNoMenu = GameObject.Find("GUI 1").transform.GetChild(0).GetChild(2).GetChild(3).gameObject;
		}
		else
		{
			Debug.LogError("yesNoMenu obj is not assigned");
		}

//		if(obj_canvas == null)
//		{
//			obj_canvas = GameObject.Find("Canvas").gameObject;
//		}
		//Sprite tempimage = AssetDatabase.LoadAssetAtPath("Assets/Art/GUI/In_Game_UI/Pause menu/Restart(Selected)",typeof(Sprite)) as Sprite;
		//GameObject.Find("Restart").GetComponent<Image>().sprite.name = "";

	}

	void Update () {
		GamePause ();
	}

	private void GamePause(){
		if (Input.GetButtonDown ("Start")) {
			if (!pauseMenu.activeInHierarchy) {
				pauseMenu.SetActive (true);
				yesNoMenu.SetActive (false);
				pauseMenu.GetComponent<LevelSelectController> ().enabled = true;
				pauseMenu.GetComponent<CursorIndexTracker> ().currentCursorIndex = 0;
				Time.timeScale = 0.0f;
			} else {
				pauseMenu.GetComponent<LevelSelectController> ().enabled = true;
				pauseMenu.GetComponent<CursorIndexTracker> ().currentCursorIndex = 0;
				pauseMenu.SetActive (false);
				Time.timeScale = 1.0f;
			}
		}

		if (Input.GetButtonDown ("Cancel")) {
			if (pauseMenu.activeInHierarchy) {
				if (!yesNoMenu.activeInHierarchy) {
					yesNoMenu.SetActive (false);
					pauseMenu.GetComponent<LevelSelectController> ().enabled = true;
					pauseMenu.GetComponent<CursorIndexTracker> ().currentCursorIndex = 0;
					pauseMenu.SetActive (false);
					Time.timeScale = 1.0f;
				}
			}
		}
	}
}
