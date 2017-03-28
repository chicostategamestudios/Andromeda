using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RelicIconDisplay : MonoBehaviour {

	public GameObject relicIconPrefab;
	public Sprite[] redRelicIcons;
	public Sprite[] blueRelicIcons;
	public Sprite[] yellowRelicIcons;
	public Sprite[] greenRelicIcons;

	public Text timerTime;
	public Text curGrade;
	public Text lockedState;

	private FancyLevelSelectController myLevelControl;

	private LevelToUnlock currentLevel;
	private LevelStats currentLevelStats;
	string timerPrefix = "Time: ";
	string gradePrefix = "Grade: ";
	private float XStart = -850f;
	private float YStart = -900f;
	private float ZStart = -65f;
	private float XLimit = 1550f;
	private float YLimit = -490f;
	private float XAddAmount = 150;
	private float YAddAmount = -75f;
	private float currentXAdd = 0;
	private float currentYAdd = 0;
	private GameObject currentRelicIcon;

	List<GameObject> myRelics = new List<GameObject>();

	GameObject spawnRelicsAt;

	// Use this for initialization
	void Start () {
		spawnRelicsAt = this.transform.GetComponentInChildren<GridLayoutGroup>().gameObject;
		myLevelControl = Object.FindObjectOfType<FancyLevelSelectController> ();
		currentLevel = myLevelControl.GetLevel;
		SpawnRelicIcon ();
		
		//currentLevelStats = 
	}
	
	// Update is called once per frame
	void Update () {

		if (myLevelControl.GetLevel != currentLevel) {
			GameObject[] myGUI = myRelics.ToArray();

			for (int gui = 0; gui < myGUI.Length; gui++)
			{
				Destroy(myGUI[gui]);
			}
			myRelics.Clear();

			currentLevel = myLevelControl.GetLevel;
			ChangeTimer (SaveGame.GetGameSaver.GetGameStats.GetStats (currentLevel));
			SpawnRelicIcon ();

			
		}

		if(Input.GetButton("Jump")){
			SpawnRelicIcon ();
		}
	
	}

	void ChangeTimer(LevelStats CurLevel){


		//Debug.Log (CurLevel.completionTime);

		//lockedState.text = "Completed: " + !CurLevel.locked;
		timerTime.text = timerPrefix + CurLevel.completionTime;
		curGrade.text = gradePrefix + CurLevel.grade.ToString();


	}

	void SpawnSprites(SerializableTreasure currentDisplayingTreasure, Sprite[] spriteState){

		currentRelicIcon = GameObject.Instantiate (relicIconPrefab, this.transform.position, this.transform.rotation) as GameObject;
		myRelics.Add(currentRelicIcon);
		currentRelicIcon.transform.parent = spawnRelicsAt.transform;
		currentRelicIcon.transform.localScale = new Vector3 (1.9f, 0.7f, 0.4f);
		//currentRelicIcon.transform.localPosition = new Vector3 (XStart + currentXAdd, YStart + currentYAdd, ZStart);
		currentXAdd += XAddAmount;
		if (currentXAdd >= XLimit) {
		//	currentYAdd += YAddAmount;
			//currentXAdd = 0;
		}

		if (currentDisplayingTreasure.myState == TreasureState.pickedUp) {
			currentRelicIcon.GetComponent<Image> ().sprite = spriteState [0];
		}

		if (currentDisplayingTreasure.myState == TreasureState.notPickedUp) {
			currentRelicIcon.GetComponent<Image> ().sprite = spriteState [1];
		}

		if (currentDisplayingTreasure.myState == TreasureState.lost) {
			currentRelicIcon.GetComponent<Image> ().sprite = spriteState [2];
		}

		//Set the sprite 0 = Collected, 1 = Not Collected, 2 = Dropped
	}

	void SpawnRelicIcon(){
		//For each relic in each list
		Debug.Log("Starting SpawnPlayer Relics Icons");
		currentLevelStats = SaveGame.GetGameSaver.GetGameStats.GetStats(currentLevel);

		Debug.Log ("RedTreasureRemaining = " + currentLevelStats.RedTreasuresRemaining.Count);
		foreach (SerializableTreasure currentTreasure in currentLevelStats.RedTreasuresRemaining){
			Debug.Log ("Spawning Red");
			SpawnSprites(currentTreasure, redRelicIcons);
		}

		foreach (SerializableTreasure currentTreasure in currentLevelStats.BlueTreasuresRemaining){
			Debug.Log ("Spawning blue");
			SpawnSprites(currentTreasure, blueRelicIcons);
		}


		foreach (SerializableTreasure currentTreasure in currentLevelStats.GreenTreasuresRemaining){
			Debug.Log ("Spawning green");
			SpawnSprites(currentTreasure, greenRelicIcons);
		}

		foreach (SerializableTreasure currentTreasure in currentLevelStats.YellowTreasuresRemaining){
			Debug.Log ("Spawning yellow");
			SpawnSprites(currentTreasure, yellowRelicIcons);
		}


	}
}
