using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	[SerializeField]
	private GameObject pauseMenu;
	[SerializeField]
	private GameObject yesNoMenu;

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
