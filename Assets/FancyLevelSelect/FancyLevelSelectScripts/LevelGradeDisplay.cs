using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelGradeDisplay : MonoBehaviour {

	private string levelGradeString;

	// Use this for initialization
	void Start () {
		//levelGradeString = "Grade: " + stored grade value
		this.GetComponent<Text>().text = levelGradeString;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
