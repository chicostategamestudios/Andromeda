using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayTreasureCollected : MonoBehaviour {

	private Text greenTreasureText, redTreasureText, blueTreasureText, yellowTreasureText;
	private TreasureManager myTManager;
	private int greenTreasureAmount, redTreasureAmount, blueTreasureAmount, yellowTreasureAmount;

	private string green = "Green";
	private string yellow = "Yellow";
	private string red = "Red";
	private string blue = "Blue";


	void Start () {
		myTManager = TreasureManager.GetManager;

		//Initialize red, yellow, blue, and green text
		Text[] colorTxt = GetComponentsInChildren<Text> ();
		for (int color = 0; color < colorTxt.Length; color++) {
			if(colorTxt[color].transform.name.Contains(green)){
				greenTreasureText = colorTxt[color];
			}
			if(colorTxt[color].transform.name.Contains(red)){
				redTreasureText = colorTxt[color];
			}
			if(colorTxt[color].transform.name.Contains(yellow)){
				yellowTreasureText = colorTxt[color];
			}
			if(colorTxt[color].transform.name.Contains(blue)){
				blueTreasureText = colorTxt[color];
			}
		}

		UpdateTreasureCollected ();

	}
	

	void Update () {
		
	}

	public void UpdateTreasureCollected(){
		greenTreasureAmount = myTManager.getCollectedTreasureAmount (TreasureType.green);
		greenTreasureText.text = "" + greenTreasureAmount;
		redTreasureAmount = myTManager.getCollectedTreasureAmount (TreasureType.red);
		redTreasureText.text = "" + redTreasureAmount;
		blueTreasureAmount = myTManager.getCollectedTreasureAmount (TreasureType.blue);
		blueTreasureText.text = "" + blueTreasureAmount;
		yellowTreasureAmount = myTManager.getCollectedTreasureAmount (TreasureType.yellow);
		yellowTreasureText.text = "" + yellowTreasureAmount;

	}
}
