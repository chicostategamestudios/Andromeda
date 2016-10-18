using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This is the treasure manager. It will be used to manage the spawning of treasures, 

public class TreasureManager : MonoBehaviour {
	private static TreasureManager _treasureManager;

	List<Treasure> redTreasure = new List<Treasure>(); //we will hold a different list for each type of treasure
	List<Treasure> blueTreasure = new List<Treasure>(); //These will be filled in AddTreasure.
	List<Treasure> greenTreasure = new List<Treasure>(); //AddTreasure will be ran by each treasure themselves, the
	List<Treasure> yellowTreasure = new List<Treasure>(); //method here will sort them

	// Use this for initialization
	void Awake () { //standard singleton shit
		if (_treasureManager == null) { 
			_treasureManager = this;
		} else {
			Destroy (this);
		}
	}
	 
	public static TreasureManager GetManager{ //neato singleton getter that stuff uses to run things like addTreasure
		get { return _treasureManager; }
	}

	public void AddTreasure(Treasure myTreasure){ //Ran by anything with the Treasure componenet, this will sort them and add them to the appropriate scripts
		switch (myTreasure._treasureType){
		case TreasureType.blue:
			blueTreasure.Add (myTreasure);
		break;
		case TreasureType.red:
			redTreasure.Add (myTreasure);
		break;
		case TreasureType.green:
			greenTreasure.Add (myTreasure);
		break;
		case TreasureType.yellow:
			yellowTreasure.Add (myTreasure);
		break;
		}
	}

	public void CheckList(List<Treasure> TreasureList){ //This function can be with each treasure list as input, it will make sure 
														//level design did not put in duplicate indexes of treausres, and if they did it will tell them what they did wrong
		for (int treasure = 0; treasure < TreasureList.Count; treasure++) {
			for (int nestedTreasure = 0; nestedTreasure < TreasureList.Count; nestedTreasure++) {
				if (TreasureList [nestedTreasure] != TreasureList [treasure]) {
					if (TreasureList [nestedTreasure].MyIndex == TreasureList [treasure].MyIndex) {
						Debug.LogError("Treasure Type: " + TreasureList[nestedTreasure]._treasureType + " has the index of " + TreasureList[nestedTreasure].MyIndex + " multiple times, fix it yo");
					}
				}

			}


		}


	}




}
