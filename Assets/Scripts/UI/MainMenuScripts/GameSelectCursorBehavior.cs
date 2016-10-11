using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSelectCursorBehavior : MonoBehaviour {

	public int chosedLevelToLoad;

	void Update () {
		LoadLevels (chosedLevelToLoad);
	}

	void LoadLevels (int whichLevel){
		if (Input.GetButtonDown ("Submit")) {
			SceneManager.LoadScene (whichLevel);
		}
	}
}
