using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelData {

	string finalTime;
	List<Treasure> yellowTreasures,greenTreasures, redTreasures, blueTreasures;
	//Also grade and other stuff

	public void GetData(){
		//get time from timer
		finalTime = TimerDisplay.getTimer.GetFinalTime();

		greenTreasures = TreasureManager.GetManager.getTreasureList (TreasureType.green);
		redTreasures = TreasureManager.GetManager.getTreasureList (TreasureType.red);
		blueTreasures = TreasureManager.GetManager.getTreasureList (TreasureType.blue);
		yellowTreasures = TreasureManager.GetManager.getTreasureList (TreasureType.yellow);
		//get treasure values from treasure manager
	}

	public string getFinaltime
	{
		get { return finalTime; }
	}

	public List<Treasure> getTreasure(TreasureType myTreasure){
		List<Treasure> returnList = null;

		switch (myTreasure) {
		case TreasureType.blue:
			returnList = blueTreasures;
			break;
		case TreasureType.green:
			returnList = greenTreasures;
			break;
		case TreasureType.red:
			returnList = redTreasures;
			break;
		case TreasureType.yellow:
			returnList = yellowTreasures;
			break;
		}
		return returnList;
	}











}
