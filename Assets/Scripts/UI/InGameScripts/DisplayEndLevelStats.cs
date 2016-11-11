using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayEndLevelStats : MonoBehaviour {

	private static DisplayEndLevelStats EndLevelStats;

	private Text greenTreasureTextEnd, redTreasureTextEnd, blueTreasureTextEnd, yellowTreasureTextEnd, timerEndText, endGradeText;

	private string green = "EndGreen";
	private string yellow = "EndYellow";
	private string red = "EndRed";
	private string blue = "EndBlue";
	private string timer = "Time";
	private string grade = "Grade";

	public GameObject FinalScreenCanvas;

	// Use this for initialization
	void Start () {
		FinalScreenCanvas.gameObject.SetActive (false);

		Text[] colorTxt = GetComponentsInChildren<Text> ();
		for (int color = 0; color < colorTxt.Length; color++) {
			if (colorTxt [color].transform.name.Contains (green)) {
				greenTreasureTextEnd = colorTxt [color];
			}
			if (colorTxt [color].transform.name.Contains (red)) {
				redTreasureTextEnd = colorTxt [color];
			}
			if (colorTxt [color].transform.name.Contains (yellow)) {
				yellowTreasureTextEnd = colorTxt [color];
			}
			if (colorTxt [color].transform.name.Contains (blue)) {
				blueTreasureTextEnd = colorTxt [color];
			}
			if (colorTxt [color].transform.name.Contains (timer)) {
				timerEndText = colorTxt [color];
			}
			if (colorTxt [color].transform.name.Contains (grade)) {
				endGradeText = colorTxt [color];
			}
		}

		EndLevelStats = this;

	//	FeedEndLevelStats (1, 2, 3, 4, "asdf");
	}

	public static DisplayEndLevelStats getEndStats{
		get { return EndLevelStats; }
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void FeedEndLevelStats (LevelData myData){
		
		greenTreasureTextEnd.text = "X " + TreasureManager.GetManager.getCollectedTreasureAmount(TreasureType.green);
		redTreasureTextEnd.text = "X " + TreasureManager.GetManager.getCollectedTreasureAmount(TreasureType.red);
		blueTreasureTextEnd.text = "X " + TreasureManager.GetManager.getCollectedTreasureAmount(TreasureType.blue);
		yellowTreasureTextEnd.text = "X " + TreasureManager.GetManager.getCollectedTreasureAmount(TreasureType.yellow);
		timerEndText.text = "Time: " + myData.getFinaltime;
		endGradeText.text = "Grade: F";

		FinalScreenCanvas.gameObject.SetActive (true);
	}
}
