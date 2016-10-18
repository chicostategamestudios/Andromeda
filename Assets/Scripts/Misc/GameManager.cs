using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static GameManager _gameManager; //static instance of our Game Manager

	private static GameStats loadedGame; //static instance of the currently loaded Game File

	void Awake(){ //set our manager as a singleton/static/multi-scene object
		if (_gameManager == null) {
			_gameManager = this;
		} else {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (this);
	}

	public static GameManager GetGameManager{ //getter for game manager
		get{ 
			if (_gameManager == null) { //failsafe in case we try to access a gamemanager and it wasn't set during awake
				_gameManager = FindObjectOfType<GameManager> ();
			}
			if (_gameManager == null) { //failsafe if there is no gamemanager
				Debug.LogError ("A class is trying to get an instance of the Game Manager but there is not one in the scene");
				return null;
			} else {
				return _gameManager;
			}
		}
	}

	public static GameStats GetGameStats{ //getter for our game file to ref for stats
		get { 
			if (loadedGame == null) { //failsafe if we try to load a game that is not there
				Debug.LogError ("A script is trying to load a game file from the Game Manager before we chose a game file to load, make sure you call Load Game first");
				return null;
			} else {
				return loadedGame;
			}
		}
	}

	public static void LoadGame(int gameToLoad){ //this will go to my gamesaver and set my currently loaded game file
		switch (gameToLoad) { //this is ran by the UI
		case 0:
			SaveGame.GetGameSaver.SetGame (MyLoadedGame.GameOne);
		break;
		case 1:
			SaveGame.GetGameSaver.SetGame (MyLoadedGame.GameTwo);
			break;
		case 2:
			SaveGame.GetGameSaver.SetGame (MyLoadedGame.GameThree);
		break;
		default:
			Debug.LogError ("Load Game is being ran in the GameManager trying to call a game file that is out of range");
			break;
		}
		loadedGame = SaveGame.GetGameSaver.GetGameStats; //set loaded game
	}

	public static void UpdateSave(){ //call this when we need to update the save of a level or something
		SaveGame.GetGameSaver.UpdateSave (loadedGame); //pass the loaded game to the game saver, which saves it
	}





}
