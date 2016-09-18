using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LoadGames : MonoBehaviour {

	public static List<GameStats> myLoadedGames = new List<GameStats>();

	public bool setGameFile;
	public float SetPlayTime;

	public MyLoadedGame gameToSave;

	MyGames _gamesInstance;
	SaveGame GameSaver;

	public LockMode unlockStuff;

	void Awake(){
		GameSaver = this.GetComponent<SaveGame> ();
		GameSaver.LoadGames ();
		GetSavedGames ();

		for (int game = 0; game < myLoadedGames.Count; game++) {
		//	Debug.Log (myLoadedGames [game].LevelOneStats.getLocked());
		}


	}


	void Update(){
		
		if (Input.GetKeyDown (KeyCode.F1)) {

		

			OpenSavedgame ();
		} else if (Input.GetKeyDown (KeyCode.F2)) {

			UpdateValues ();

		} else if (Input.GetKeyDown (KeyCode.F3)) {

		} else if (Input.GetKeyDown (KeyCode.F4)) {

		} else if (Input.GetKeyDown (KeyCode.Tab)) {
	
		}

	


	}




	public void GetSavedGames(){
		myLoadedGames.Clear ();
		if (_gamesInstance == null) {
			//Debug.Log ("I'm creating a new MyGames() as _gamesInstance");
			_gamesInstance = new MyGames ();
		}
		if (GameSaver == null) {
			//Debug.Log ("GameSaver is " + SaveGame.GetGameSaver);
			GameSaver = SaveGame.GetGameSaver;
		}

		//Debug.Log ("_gamesInstance is " + GameSaver.GetMyGames ());
		_gamesInstance = GameSaver.GetMyGames ();


	

		myLoadedGames.Add (_gamesInstance.GameOne);
		myLoadedGames.Add (_gamesInstance.GameTwo);
		myLoadedGames.Add (_gamesInstance.GameThree);




	




	}

	public void OpenSavedgame(){

		GameSaver.SetGame (gameToSave);

	}


	public void UpdateValues(){
		GameStats updateGame = new GameStats ();

		updateGame = GameSaver.GetGameStats;

		LevelStats EarthLevel = new LevelStats ();
		EarthLevel = updateGame.EarthLevelStats;

		LevelStats FireLevel = new LevelStats ();
		FireLevel = updateGame.FireLevelStats;

		LevelStats AirLevel = new LevelStats ();
		AirLevel = updateGame.AirLevelStats;

		LevelStats WaterLevel = new LevelStats ();
		WaterLevel = updateGame.WaterLevelStats;

	



		AirLevel.setLocked (false);



		EarthLevel.setLocked (true);



	


	


		updateGame.existingFile = setGameFile;
		updateGame.PlayTime = SetPlayTime;




	

		GameSaver.UpdateSave (updateGame);


	


	}





}
