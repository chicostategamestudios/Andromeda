using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class DisplayGameFiles : MonoBehaviour {

	public Text[] myTreasureTexts;
	public Text[] myNameTexts;
	public Sprite[] fireLevelIconSpr, waterLevelIconSpr, earthLevelIconSpr, airLevelIconSpr;
	public Image[] fireLevelIconImage, waterLevelIconImage, earthLevelIconImage, airLevelIconImage;

	string GamePrefix = "Game Number: ";
	string newGame= "NEW GAME";

	public Color activated;
	public Color deActivated;

	// Use this for initialization
	void Start () {
		
		List<GameStats> mygames = LoadGames.myLoadedGames;
	
		for (int game = 0; game < mygames.Count; game++) {

			//Assigning treasure percent
			myTreasureTexts [game].text =mygames [game].treasureCollected.ToString() + "%";

			//Debug.Log (mygames [game].PlayTime);

			//New game or loaded game
			if (mygames [game].PlayTime > 0) {
				myNameTexts [game].text = GamePrefix + ((game + 1).ToString ());
			
			} else {
				myNameTexts [game].text = newGame;
			
			}

			//Assigning level completion
			//Fire Level
			if (mygames [game].FireLevelStats.getLocked()) {
				fireLevelIconImage [game].sprite = fireLevelIconSpr [1];
				//Debug.Log (mygames [game].FireLevelStats.getLocked());
			} else {
				fireLevelIconImage [game].sprite = fireLevelIconSpr [0];
			}

			if (mygames [game].WaterLevelStats.getLocked()) {
				//Debug.Log (mygames [game].FireLevelStats.getLocked());
				waterLevelIconImage [game].sprite = waterLevelIconSpr [1];
			} else {
				waterLevelIconImage [game].sprite = waterLevelIconSpr [0];
			}

			if (mygames [game].EarthLevelStats.getLocked()) {
				earthLevelIconImage [game].sprite = earthLevelIconSpr [1];
				//Debug.Log (mygames [game].FireLevelStats.getLocked());
			} else {
				earthLevelIconImage [game].sprite = earthLevelIconSpr [0];
			}

			if (mygames [game].AirLevelStats.getLocked()) {
				airLevelIconImage [game].sprite = airLevelIconSpr [1];
				//Debug.Log (mygames [game].FireLevelStats.getLocked());
			} else {
				airLevelIconImage [game].sprite = airLevelIconSpr [0];
			}



		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
