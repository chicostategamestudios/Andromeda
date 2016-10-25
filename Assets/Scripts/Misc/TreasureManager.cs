using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This is the treasure manager. It will be used to manage the spawning of treasures, 

public class TreasureManager : MonoBehaviour {
	private static TreasureManager _treasureManager;
	public List<Treasure>[] treasureLists = new System.Collections.Generic.List<Treasure>[4];
	List<Treasure> redTreasure = new List<Treasure>();//AddTreasure will be ran by each treasure themselves, the
	List<Treasure> yellowTreasure = new List<Treasure>();//method here will sort them
	List<Treasure> blueTreasure = new List<Treasure>(); //These will be filled in AddTreasure.
	List<Treasure> greenTreasure = new List<Treasure>();//we will hold a different list for each type of treasure

	public bool checkTreasures = false;

	public bool assignValues = false;

	// Use this for initialization
	void Awake () { //standard singleton shit
		if (_treasureManager == null) { 
			_treasureManager = this;
		} else {
			Destroy (this);
		}
		treasureLists [0] = redTreasure;
		treasureLists [1] = blueTreasure;
		treasureLists [2] = greenTreasure;
		treasureLists [3] = yellowTreasure;

	}
	 
	void Start(){
		AssignValues ();
		if (checkTreasures) {
			for (int list = 0; list < treasureLists.Length; list++) {
				CheckList (treasureLists [list]);
			}
		}
	}

	void Update(){
		if (assignValues) {
			assignValues = false;

		}

	}

	void AssignValues(){
		for (int list = 0; list < treasureLists.Length; list++) {
			for (int treasure = 0; treasure < treasureLists[list].Count; treasure++) {
				(treasureLists [list]) [treasure].MyIndex = treasure;
			}
		}

	}

	public static TreasureManager GetManager{ //neato singleton getter that stuff uses to run things like addTreasure
		get { 
			if (_treasureManager == null) {
				_treasureManager = FindObjectOfType<TreasureManager> ();
			}if (_treasureManager == null) {
				GameObject treasure = new GameObject ();
				_treasureManager = treasure.AddComponent<TreasureManager> ();
			}

			return _treasureManager; }
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

						Debug.LogError("Object: " + TreasureList[nestedTreasure].transform.name +  " Treasure Type: " + TreasureList[nestedTreasure]._treasureType + " has the index of " + TreasureList[nestedTreasure].MyIndex + " multiple times, fix it yo");
						TreasureList.Remove (TreasureList [nestedTreasure]);
					}
				}
			}
		}
	}


	public void RespawnRelic(){
		if (redTreasure.Count > 0) {
			redTreasure [Random.Range (0, redTreasure.Count - 1)].ChangeState (TreasureState.lost);
			//respawn red
		} else if (yellowTreasure.Count > 0) {
			yellowTreasure [Random.Range (0, yellowTreasure.Count - 1)].ChangeState (TreasureState.lost);
			//respawn yellow
		} else if (blueTreasure.Count > 0) {
			blueTreasure [Random.Range (0, blueTreasure.Count - 1)].ChangeState (TreasureState.lost);
			//respawn blue
		} else if (greenTreasure.Count > 0) {
			greenTreasure [Random.Range (0, greenTreasure.Count - 1)].ChangeState (TreasureState.lost);
			//respawn green
		} else {
			//nothin
		}
	}
}
