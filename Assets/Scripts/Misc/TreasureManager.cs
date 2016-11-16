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
	 
	IEnumerator Start(){
		yield return new WaitForSeconds (0.1f);
		if (checkTreasures) {
			for (int list = 0; list < treasureLists.Length; list++) {
				CheckList (treasureLists [list]);
			}
		}
	}

	void Update(){
		if (assignValues) {
			AssignValues ();
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
			if (GameManager.GetGameStats != null) {
				myTreasure.ChangeState (GameManager.getCurLevel.getMyState (TreasureType.blue, myTreasure.MyIndex));
			}
		break;
		case TreasureType.red:
			redTreasure.Add (myTreasure);
			if (GameManager.GetGameStats != null) {
				myTreasure.ChangeState (GameManager.getCurLevel.getMyState (TreasureType.red, myTreasure.MyIndex));
			}
		break;
		case TreasureType.green:
			greenTreasure.Add (myTreasure);
			if (GameManager.GetGameStats != null) {
				myTreasure.ChangeState (GameManager.getCurLevel.getMyState (TreasureType.green, myTreasure.MyIndex));
			}
		break;
		case TreasureType.yellow:
			yellowTreasure.Add (myTreasure);
			if (GameManager.GetGameStats != null) {
				myTreasure.ChangeState (GameManager.getCurLevel.getMyState (TreasureType.yellow, myTreasure.MyIndex));
			}
		break;
		}




	}

	public int getCollectedTreasureAmount(TreasureType color){
		int collectedAmount = 0;
		switch (color) {
		case TreasureType.blue:
			for (int i = 0; i < blueTreasure.Count; i++) {
				if (blueTreasure [i].IsCollected) {
					collectedAmount++;
				}
			}
			return collectedAmount;
			break;
		case TreasureType.green:
			for (int i = 0; i < greenTreasure.Count; i++) {
				if (greenTreasure [i].IsCollected) {
					collectedAmount++;
				}
			}
			return collectedAmount;
			break;
		case TreasureType.red:
			for (int i = 0; i < redTreasure.Count; i++) {
				if (redTreasure [i].IsCollected) {
					collectedAmount++;
				}
			}
			return collectedAmount;
			break;
		case TreasureType.yellow:
			for (int i = 0; i < yellowTreasure.Count; i++) {
				if (yellowTreasure [i].IsCollected) {
					collectedAmount++;
				}
			}
			return collectedAmount;
			break;
		default:
			Debug.LogError ("Trying to get an unset treasure list");
			return 0;
			break;
		}
	}

	public List<Treasure> getTreasureList(TreasureType color){
		switch (color) {
		case TreasureType.blue:
			return blueTreasure;
			break;
		case TreasureType.green:
			return greenTreasure;
			break;
		case TreasureType.red:
			return redTreasure;
			break;
		case TreasureType.yellow:
			return yellowTreasure;
			break;
		default:
			Debug.LogError ("Trying to get an unset treasure list");
			return null;
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
