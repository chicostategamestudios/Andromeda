using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSelectCursorBehavior : MonoBehaviour {

	public int chosedLevelToLoad;
	CursorIndexTracker myCursor;
	void Update () {
		if (myCursor == null) {
			myCursor = this.gameObject.GetComponent<CursorIndexTracker> ();
		}if (myCursor == null) {
			Debug.LogError ("trying to get a cursor but can't find it");
			return;
		}
		LoadLevels (chosedLevelToLoad);
	}

	void LoadLevels (int whichLevel){
		if (Input.GetButtonDown ("Submit")) {
			SceneManager.LoadScene (whichLevel);
			GameManager.LoadGame (myCursor.currentCursorIndex);
		}
	}
}
