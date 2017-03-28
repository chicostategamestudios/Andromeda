//Original Author: Michael Kennedy? || Last Edited: Alexander Stamatis [A.S] | Modified on Feb 19, 2017

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuYesNo : MonoBehaviour {

	[SerializeField]
	private GameObject pauseMenuMain;

	private CursorIndexTracker myCursorIndex;
	private float selectedValue;

	
	private int mainMenuSceneIndex, levelSelectSceneIndex;

	//Added this awake function [A.S.]
	private void Awake()
	{
		//Assign PauseMenu OBJ - A.S.
		if (pauseMenuMain == null)
		{
			//Get GameObject named "GUI 1", go to first child named "Canvas" then get child #3 named "Pause Menu"
			//Its important that the PauseMenu is in the correct hierarchy under parent "Canvas"
			if (GameObject.Find("GUI 1").transform.GetChild(0).GetChild(2).gameObject)
			{
				pauseMenuMain = GameObject.Find("GUI 1").transform.GetChild(0).GetChild(2).gameObject;
			}
		} else
		{
			Debug.LogError("pauseMenu is not assigned");
		}
	}

	// Use this for initialization
	void Start () {
		mainMenuSceneIndex = SceneRef.getMainMenu;
		levelSelectSceneIndex = SceneRef.getLevelSelect;
		myCursorIndex = this.GetComponent<CursorIndexTracker> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		YesNoSelect ();
	
	}

	public void SetSelectedValue(float setValue){

		selectedValue = setValue;

	}
		

	private void YesNoSelect(){

		if (Input.GetButtonDown ("Submit")) {
			switch (myCursorIndex.currentCursorIndex) {
			//Yes
			case 0:
				{
					if (selectedValue == 0) {
						//Restart
						Time.timeScale = 1.0f;
						SceneManager.LoadScene (SceneManager.GetActiveScene ().name, LoadSceneMode.Single);

					} else if (selectedValue == 1) {
						//Level Select
						Time.timeScale = 1.0f;
						SceneManager.LoadScene (levelSelectSceneIndex, LoadSceneMode.Single);

					} else if (selectedValue == 2) {
						//Save and Quit
						Time.timeScale = 1.0f;
						SceneManager.LoadScene (mainMenuSceneIndex, LoadSceneMode.Single);
					} else {
						//Indice out of range
					}
				
					break;
				}
			//No
			case 1:
				{
					myCursorIndex.currentCursorIndex = 0;
					pauseMenuMain.GetComponent<LevelSelectController> ().enabled = true;
					this.gameObject.SetActive (false);
					break;
				}
			default:
				{
					//Out of indice range
					break;
				}
			}
		}

		if (Input.GetButtonDown ("Cancel")) {
			Debug.Log (pauseMenuMain.name);
			pauseMenuMain.GetComponent<LevelSelectController> ().enabled = true;
			this.gameObject.SetActive (false);
		}
	}
}
