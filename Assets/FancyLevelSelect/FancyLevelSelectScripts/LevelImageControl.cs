using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelImageControl : MonoBehaviour {

	public Sprite[] levelImages;

	private int myIndex = 0;

	// Use this for initialization
	void Start () {
		myIndex = Object.FindObjectOfType<FancyLevelSelectController> ().levelSelectIndex;
	}
	
	// Update is called once per frame
	void Update () {
		if (myIndex != Object.FindObjectOfType<FancyLevelSelectController> ().levelSelectIndex) {
			myIndex = Object.FindObjectOfType<FancyLevelSelectController> ().levelSelectIndex;
			this.GetComponent<Image> ().sprite = levelImages [myIndex];
		}
	}
}
