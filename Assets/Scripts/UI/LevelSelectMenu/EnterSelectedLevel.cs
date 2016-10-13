using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnterSelectedLevel : MonoBehaviour {

	private CursorIndexTracker myCursorIndex;

	[SerializeField]
	private int starterSceneIndex, earthSceneIndex, windSceneIndex,
	fireSceneIndex, iceSceneIndex;

	void Start(){
		myCursorIndex = this.GetComponent<CursorIndexTracker> ();
	}

	void Update () {
	
		SelectLevel ();

	}

	void SelectLevel(){
		if (Input.GetButtonDown ("Submit")) {

			switch (myCursorIndex.currentCursorIndex) {

			//Starter Level
			case 0:
				{
					SceneManager.LoadScene (starterSceneIndex, LoadSceneMode.Single);
					break;
				}
			//Earth Level
			case 1:
				{
					SceneManager.LoadScene (earthSceneIndex, LoadSceneMode.Single);
					break;
				}
			//Wind Level
			case 2:
				{
					SceneManager.LoadScene (windSceneIndex, LoadSceneMode.Single);
					break;
				}
			//Fire Level
			case 3:
				{
					SceneManager.LoadScene (fireSceneIndex, LoadSceneMode.Single);
					break;
				}
			//Ice Level
			case 4:
				{
					SceneManager.LoadScene (iceSceneIndex, LoadSceneMode.Single);
					break;
				}
			default:
				{
					Debug.Log ("Cursor Index Out of Range");
					break;
				}
			}

		}
	}
}
