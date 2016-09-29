using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuImageSelectedBehavior : MonoBehaviour {

	public Sprite[] onOffSprites;
	public int selectionIndex;

	void Update(){
		AssignProperSprite ();
	}

	void AssignProperSprite(){
		if (this.gameObject.GetComponentInParent<CursorIndexTracker> ().currentCursorIndex == selectionIndex) {
			this.GetComponent<Image> ().sprite = onOffSprites [1];
		} else {
			this.GetComponent<Image> ().sprite = onOffSprites [0];
		}
	}
}
