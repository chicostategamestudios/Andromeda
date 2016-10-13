using UnityEngine;
using System.Collections;

public class PauseMenuSelect : MonoBehaviour {

	public float SelectedValue{
		get {
			return selectedValue;
		}
	}

	private CursorIndexTracker myCursorIndex;
	private LevelSelectController mySelectController;
	private GameObject yesNoMenu;
	private bool canPress = true;
	public float selectedValue;


	// Use this for initialization
	void Start () {

		myCursorIndex = this.GetComponent<CursorIndexTracker> ();
		mySelectController = this.GetComponent<LevelSelectController> ();

		foreach (RectTransform child in this.GetComponent<RectTransform>()) {
			if (child.gameObject.name == "AreYouSureMenu") {
				yesNoMenu = child.gameObject;
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		PauseSelect ();
	
	}
		

	void PauseSelect(){
		if (Input.GetButtonDown ("Submit")) {

			switch (myCursorIndex.currentCursorIndex) {
			//Restart Level
			case 0:
				{
					//Go to yes no
					selectedValue = 0;
					yesNoMenu.SetActive (true);
					yesNoMenu.GetComponent<PauseMenuYesNo> ().SetSelectedValue (selectedValue);
					mySelectController.enabled = false;
					break;
				}
			case 1:
				{
					//Go to yes no
					selectedValue = 1;
					yesNoMenu.SetActive (true);
					yesNoMenu.GetComponent<PauseMenuYesNo> ().SetSelectedValue (selectedValue);
					mySelectController.enabled = false;
					break;
				}
			case 2:
				{
					//Go to yes no
					selectedValue = 2;
					yesNoMenu.SetActive (true);
					yesNoMenu.GetComponent<PauseMenuYesNo> ().SetSelectedValue (selectedValue);
					mySelectController.enabled = false;
					break;
				}
			}




			canPress = false;
		}

		if (Input.GetButtonUp ("Submit")) {
			canPress = true;
		}
		
	}
}
