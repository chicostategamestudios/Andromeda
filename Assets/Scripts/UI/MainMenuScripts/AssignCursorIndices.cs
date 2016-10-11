using UnityEngine;
using System.Collections;

public class AssignCursorIndices : MonoBehaviour {

	private GameObject[] mainmenuSelections;

	void Start () {
		AssignIndiciesToChildren();
	}

	void AssignIndiciesToChildren(){
		int i = 0;
		foreach (RectTransform cursorChild in this.GetComponent<RectTransform>()) {
			{
				cursorChild.gameObject.GetComponent<MenuImageSelectedBehavior> ().selectionIndex = i;
				i++;
			}
		}
	}
}
