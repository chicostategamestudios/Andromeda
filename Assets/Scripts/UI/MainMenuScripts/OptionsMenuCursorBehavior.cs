using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenuCursorBehavior : MonoBehaviour {

	public GameObject optionsMenu, mainMenuController;
	private CursorIndexTracker cursorIndex;
	private GameObject masterVolumeSlider, musicVolumeSlider, gameVolumeSlider;


	void Start () {
		cursorIndex = this.GetComponent<CursorIndexTracker> ();

		foreach (RectTransform optionsChild in optionsMenu.GetComponent<RectTransform>()) {
			if (optionsChild.gameObject.name == "Master_Volume_Slider") {
				masterVolumeSlider = optionsChild.gameObject;
			}
			if (optionsChild.gameObject.name == "Music_Volume_Slider") {
				musicVolumeSlider = optionsChild.gameObject;
			}
			if (optionsChild.gameObject.name == "Game_Volume_Slider") {
				gameVolumeSlider = optionsChild.gameObject;
			}
		}
	
	}
	

	void Update () {
		AdjustSliders ();
	}

	void AdjustSliders(){
		//Adjusting Master Volume
		if (cursorIndex.currentCursorIndex == 0) {
			if (Input.GetAxis ("Horizontal") > 0.0f) {
				masterVolumeSlider.GetComponent<Slider> ().value++;
			}
			if (Input.GetAxis ("Horizontal") < 0.0f) {
				masterVolumeSlider.GetComponent<Slider> ().value--;
			}
		} 
		//Adjusting Music Volume
		else if (cursorIndex.currentCursorIndex == 1) {
			if (Input.GetAxis ("Horizontal") > 0.0f) {
				musicVolumeSlider.GetComponent<Slider> ().value++;
			}
			if (Input.GetAxis ("Horizontal") < 0.0f) {
				musicVolumeSlider.GetComponent<Slider> ().value--;
			}
		}
		//Adjusting Game Volume
		else if (cursorIndex.currentCursorIndex == 2) {
			if (Input.GetAxis ("Horizontal") > 0.0f) {
				gameVolumeSlider.GetComponent<Slider> ().value++;
			}
			if (Input.GetAxis ("Horizontal") < 0.0f) {
				gameVolumeSlider.GetComponent<Slider> ().value--;
			}
		}
	}
}
