using UnityEngine;
using System.Collections;

public class DebugLocalYPos : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			Debug.Log ("Local X = " + transform.localRotation.eulerAngles.x);
			Debug.Log ("Local Y = " + transform.localRotation.eulerAngles.y);
			Debug.Log ("Local Z = " + transform.localRotation.eulerAngles.z);
		}
	}
}
