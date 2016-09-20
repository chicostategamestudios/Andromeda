using UnityEngine;
using System.Collections;

public class ExitCursorBehavior : MonoBehaviour {

	private CursorIndexTracker myPosition;

	[SerializeField]
	private GameObject mainMenu;

	void Start(){
		myPosition = GetComponent<CursorIndexTracker> ();
	}

	void Update () {
		if (Input.GetButtonDown ("Submit") && myPosition.currentCursorIndex == 0) {
			Debug.Log ("You quit the game using Application.Quit()!");
			Application.Quit ();
		} else if (Input.GetButtonDown ("Submit") && myPosition.currentCursorIndex == 1) {
			mainMenu.GetComponentInParent<MainMenuController>().ReturnToPreviousMenu();
		}
	
	}
}
