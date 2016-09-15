using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LoadGames : MonoBehaviour {

	List<GameStats> myLoadedGames = new List<GameStats>();

	public bool setGameFile;
	public float SetPlayTime;

	public MyLoadedGame gameToSave;

	MyGames _gamesInstance;
	SaveGame GameSaver;

	void Update(){

		if (Input.GetKeyDown (KeyCode.F1)) {

			OpenSavedgame ();


		} else if (Input.GetKeyDown (KeyCode.F2)) {

			UpdateValues ();

		} else if (Input.GetKeyDown (KeyCode.F3)) {

		} else if (Input.GetKeyDown (KeyCode.F4)) {

		} else if (Input.GetKeyDown (KeyCode.Tab)) {
			GetSavedGames ();
		}




	}




	public void GetSavedGames(){
		myLoadedGames.Clear ();
		if (_gamesInstance == null) {
			_gamesInstance = new MyGames ();
		}
		if (GameSaver == null) {
			GameSaver = SaveGame.GetGameSaver;
		}

		_gamesInstance = GameSaver.GetMyGames ();


	

		myLoadedGames.Add (_gamesInstance.GameOne);
		myLoadedGames.Add (_gamesInstance.GameTwo);
		myLoadedGames.Add (_gamesInstance.GameThree);




		for (int game = 0; game < myLoadedGames.Count; game++) {
			Debug.Log(myLoadedGames [game].existingFile);
			Debug.Log(myLoadedGames [game].PlayTime);
		}




	}

	public void OpenSavedgame(){

		GameSaver.SetGame (gameToSave);

	}


	public void UpdateValues(){
		GameStats updateGame = new GameStats ();
		updateGame = GameSaver.GetGameStats;

		updateGame.existingFile = setGameFile;
		updateGame.PlayTime = SetPlayTime;

		Debug.Log ("UPDATE VALUES");
		GameSaver.UpdateSave (updateGame);


	}





}
