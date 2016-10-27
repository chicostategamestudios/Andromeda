using UnityEngine;
using System.Collections;

public class VisualOptionsBehavior : MonoBehaviour {

	private GameObject windowedDropDown, resolutionDropdown, qualityDropDown;
	private float windowedValue, resolutionValue, qualityValue;

	void Start () {

		foreach (RectTransform optionsChild in GetComponent<RectTransform>()) {
			if (optionsChild.name == "Windowed_Dropdown_Menu") {
				windowedDropDown = optionsChild.gameObject;
				windowedValue = windowedDropDown.GetComponent<DropDownValueHolder> ().currentDropDownValue;

			}
			if (optionsChild.name == "Resolution_Dropdown_Menu") {
				resolutionDropdown = optionsChild.gameObject ;
				resolutionValue = resolutionDropdown.GetComponent<DropDownValueHolder> ().currentDropDownValue;
			}
			if (optionsChild.name == "Quality_Dropdown_Menu") {
				qualityDropDown = optionsChild.gameObject;
				qualityValue = qualityDropDown.GetComponent<DropDownValueHolder> ().currentDropDownValue;
			}
		}
	
	}

	void Update () {
		ApplyDropMenuValue ();
	}

	void ApplyDropMenuValue(){
		//Controls windowed options
		if (windowedDropDown.GetComponent<DropDownValueHolder> ().currentDropDownValue != windowedValue) {
			windowedValue = windowedDropDown.GetComponent<DropDownValueHolder> ().currentDropDownValue;
			if (windowedValue == 0) {
				Screen.fullScreen = false;
			} else if (windowedValue == 1) {
				Screen.fullScreen = true;
			}
		}

		//Controls resolution options
		if (resolutionDropdown.GetComponent<DropDownValueHolder>().currentDropDownValue != resolutionValue){
			resolutionValue = resolutionDropdown.GetComponent<DropDownValueHolder> ().currentDropDownValue;
			if (resolutionValue == 0) {
				Screen.SetResolution (1920, 1080, Screen.fullScreen);
			} else if (resolutionValue == 1) {
				Screen.SetResolution (1600, 900, Screen.fullScreen);
			} else if (resolutionValue == 2) {
				Screen.SetResolution (1280, 720, Screen.fullScreen);
			} else if (resolutionValue == 3) {
				Screen.SetResolution (1024, 600, Screen.fullScreen);
			}
		}

		//Controls quality options
		if (qualityDropDown.GetComponent<DropDownValueHolder>().currentDropDownValue != qualityValue) {
			qualityValue = qualityDropDown.GetComponent<DropDownValueHolder> ().currentDropDownValue;
			if (qualityValue == 0) {
				QualitySettings.SetQualityLevel (5, false);
			} else if (qualityValue == 1) {
				QualitySettings.SetQualityLevel (3, false);
			} else if (qualityValue == 2) {
				QualitySettings.SetQualityLevel (1, false);
			}
		}
	}
}
