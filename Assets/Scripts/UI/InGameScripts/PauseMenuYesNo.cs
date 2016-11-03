using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuYesNo : MonoBehaviour {

	[SerializeField]
	private GameObject pauseMenuMain;

	private CursorIndexTracker myCursorIndex;
	private float selectedValue;

	[SerializeField]
	private int mainMenuSceneIndex, levelSelectSceneIndex;

	// Use this for initialization
	void Start () {

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
