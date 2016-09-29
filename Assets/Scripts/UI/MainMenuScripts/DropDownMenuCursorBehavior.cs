using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DropDownMenuCursorBehavior : MonoBehaviour {
	
	public GameObject grandFatherMenu;
	public Text displayedTextUncle;

	private DropMenuDataTracker parentDropMenuData;
	private CursorIndexTracker dropDownIndices;

	void Start () {
		parentDropMenuData = this.GetComponentInParent<DropMenuDataTracker> ();
		dropDownIndices = this.GetComponent<CursorIndexTracker> ();
	}
		

	//The data is passed in Main Menu Controller under the DropDownSelection() function;
	public void PassSelectedDropDownData(){
		if (Input.GetButtonDown ("Submit")) {
			displayedTextUncle.text = parentDropMenuData.myData [dropDownIndices.currentCursorIndex];
			grandFatherMenu.GetComponent<DropDownValueHolder> ().currentDropDownValue = dropDownIndices.currentCursorIndex;
		}

	}
}
