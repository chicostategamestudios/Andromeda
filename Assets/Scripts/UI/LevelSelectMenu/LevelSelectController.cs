using UnityEngine;
using System.Collections;

public class LevelSelectController : MonoBehaviour {

	[SerializeField]
	private float thumbstickCursorThreshold = 0.8f;

	private float verticalThumb = 0.0f;
	private bool canMoveCursor = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		this.gameObject.GetComponent<CursorIndexTracker> ().currentCursorIndex = 
			VerticalCursorMovement (thumbstickCursorThreshold, this.gameObject,
				this.gameObject.GetComponent<CursorIndexTracker> ().currentCursorIndex, 
				this.gameObject.GetComponent<CursorIndexTracker> ().cursorIndexMax);
		verticalThumb = Input.GetAxis ("Vertical");
	
	}

	int VerticalCursorMovement(float thumbStickThreshold, GameObject cursor, int cursorPositionIndex, int cursorPositionIndexMax){

		int[] cursorYPositions = cursor.GetComponent<CursorIndexTracker> ().cursorYPositions;
		Vector3 cursorPosition = new Vector3 (cursor.GetComponent<RectTransform> ().localPosition.x, cursor.GetComponent<RectTransform> ().localPosition.y, cursor.GetComponent<RectTransform> ().localPosition.z);

		//If the thumbstick has been pushed far enough
		if (Mathf.Abs (Input.GetAxis ("Vertical")) >= thumbStickThreshold && canMoveCursor) {
			canMoveCursor = false;
			//If the thumbstick has been pushed up
			//Move the cursor up
			if (Input.GetAxis ("Vertical") > 0) {
				if (cursorPositionIndex == 0) {
					cursorPositionIndex = cursorPositionIndexMax;
				} else {
					cursorPositionIndex--;
				}
				//cursor.GetComponent<RectTransform> ().localPosition = new Vector2 (cursorPosition.x, cursorYPositions [cursorPositionIndex]);
				return cursorPositionIndex;
			}
			//If the thumbstick has been pushed down
			//Move the cursor down
			else if (Input.GetAxis ("Vertical") < 0) {
				if (cursorPositionIndex == cursorPositionIndexMax) {
					cursorPositionIndex = 0;
				} else {
					cursorPositionIndex++;
				}
				//cursor.GetComponent<RectTransform> ().localPosition = new Vector2 (cursorPosition.x, cursorYPositions [cursorPositionIndex]);
				return cursorPositionIndex;
			} 
			//No movement done (must return something)
			else {
				return cursorPositionIndex;
			}
		} 
		//Thumbstick lower than threshold
		else if (Mathf.Abs(Input.GetAxis ("Vertical")) <= thumbStickThreshold/2) {
			canMoveCursor = true;
			return cursorPositionIndex;
		} 
		//No movement done (must return something)
		else {
			return cursorPositionIndex;
		}
	}
}
