using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using Assets.Scripts.Components;


//This is our save game tool. The save game tool holds the level stats class (the stats that each level can have) as well as the game stats class (the stats that
//the game can have). This class can be used to load the game stats from a file, update gamestats from a file, as well as save those game stats back to the 
//original file. 

public enum LevelToUnlock //these will be used to decide what level we are unlocking, expand this if we need to unlock more stuff
{
	tutorial,
	earthLevel,
	fireLevel,
	airLevel,
	iceLevel
}

public enum LockMode //this is just the lockmode of the level. We can use this to relock a level or something like that.
{
	locked,
	unlock
}

public enum LevelGrade{
	A,
	B,
	C,
	S
}

public enum MyLoadedGame
{
	GameOne,
	GameTwo,
	GameThree
}

public class SaveGame : MonoBehaviour { //This class will save the game.
	string saveDataFile = "/SaveData";
	string saveDataFileEnding = ".dat";
	private static MyGames _myGames;
	private static GameStats myGame;
	private static MyLoadedGame curGame;
	private static SaveGame GameSaver;

	void Awake()
	{ //this setup is to ensure a singleton of the object. We only need one gamesaver

		if(GameSaver == null)
		{
			GameSaver = this.gameObject.GetComponent<SaveGame>();       
		}
		else
		{

			   Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);


	}




	public static SaveGame GetGameSaver{ //this is my getter to get this object
		get {
			if (GameSaver == null) {
				GameSaver = FindObjectOfType<SaveGame> ();
			}

			return GameSaver;}
	}

	public MyGames GetMyGames(){ //this will return my games, we SHOULD be loading games before we return myGames....
		if (_myGames != null) {
			return _myGames;
		} else {
			LoadGames (); //....but just in case we forgot to load games first, load games and then return them to the player 
			return _myGames;
		}

	}

	public GameStats GetGameStats{ //this contains the value of our currently selected game. 
		get {
			if (myGame == null) {
				Debug.LogError ("Trying to GetGameStats without setting a game first. make sure you load in and set a game before you run GetGameStats");
			}

			return myGame; }

	}


	public void LoadGames() //this should be ran in the beginning of the game
	{

		MyGames curGames = new MyGames(); //creates a new MyGames object which stores all of the data for each Game

		if (File.Exists(Application.persistentDataPath + saveDataFile + saveDataFileEnding)) //check if we have a file to open (the game has been played before)
		{
			BinaryFormatter loadBf = new BinaryFormatter(); //open a BinaryFormatter
			FileStream loadFS = File.Open(Application.persistentDataPath + saveDataFile + saveDataFileEnding, FileMode.Open); //Open a file in our persistent Data Path location
			curGames = (MyGames)loadBf.Deserialize(loadFS); //Deserialize our saved game dat file and store it into curStats
		}
		else //if we don't have a game save, create a new one
		{

			curGames.GameOne = CreateNewSave ();
			curGames.GameTwo = CreateNewSave ();
			curGames.GameThree = CreateNewSave ();

			_myGames = curGames;

			Save(); //save the new game as a new file

		}

		_myGames = curGames; 
	}

	public void SetGame(MyLoadedGame myNewGame){ //we call this function through the UI to set the game that we are currently playing
		GameStats getGame = new GameStats();

		if (myNewGame == MyLoadedGame.GameOne) {
			myGame = _myGames.GameOne;
			curGame = MyLoadedGame.GameOne;
		} else if (myNewGame == MyLoadedGame.GameTwo) {
			myGame = _myGames.GameTwo;
			curGame = MyLoadedGame.GameTwo;
		} else if (myNewGame == MyLoadedGame.GameThree) {
			myGame = _myGames.GameThree;
			curGame = MyLoadedGame.GameThree;
		}

		//Debug.Log (curGame);

	}

	public void UpdateSave(GameStats UpdateGame) //this function will be called to update the save file. Just pass in what level you want to update
	//and how you want to update it. This will need to be expanded later on (and potentially overloaded) when we start recording more stats
	{

		// LevelStats newStats = new LevelStats(); //we create a new stats object.
		// newStats.thisLevel = level; //give the new object the level that we passed in
		// newStatssetLocked(true) = lockType; //as well as how we want this level to be locked

		//later we will set more stats here

		myGame = UpdateGame;

		if (curGame == MyLoadedGame.GameOne) {
			_myGames.GameOne = UpdateGame;
		} else if (curGame == MyLoadedGame.GameTwo) {
			_myGames.GameTwo = UpdateGame;
		} else if (curGame == MyLoadedGame.GameThree) {
			_myGames.GameThree = UpdateGame;
		}

		//Debug.Log (curGame);

		Save(); //update the save file after we have update the gamestats

	}







	public void Save() //this should be ran by the UpdateSave function. After we have update some data we will write it to our save file
	{

		BinaryFormatter saveBf = new BinaryFormatter(); //open a formatter
		FileStream saveFile = File.Create(Application.persistentDataPath + saveDataFile + saveDataFileEnding); //open the file      
		//Debug.Log(Application.persistentDataPath + saveDataFile + saveDataFileEnding);
		saveBf.Serialize(saveFile, _myGames); //save the object that we loaded previously. This should have stats updated before we run save saves.
		saveFile.Close();                                   
	}





	public GameStats CreateNewSave() //this will create a new save. This creates a new game save object with 
	{								//all values set to default values. There is probably a better way to achieve this
		GameStats newGame = new GameStats();
		newGame.TutorialLevelStats.thisLevel = LevelToUnlock.tutorial;
		newGame.AirLevelStats.thisLevel = LevelToUnlock.airLevel;
		newGame.EarthLevelStats.thisLevel = LevelToUnlock.earthLevel;
		newGame.FireLevelStats.thisLevel = LevelToUnlock.fireLevel;
		newGame.WaterLevelStats.thisLevel = LevelToUnlock.iceLevel;
		//newGame.existingFile = false;
		newGame.PlayTime = 0f;
		newGame.treasureCollected = 0f;







		return newGame; //once our new game is created, return it
	}


}

[Serializable]
public class MyGames //My Games is the parent object that is being saved, it contains all of the the players current games. Create and 
//save an empty one of these to write over the game
{
	public GameStats GameOne;
	public GameStats GameTwo;
	public GameStats GameThree;

	public List<GameStats> GetGames(){
		List<GameStats> mygames = new List<GameStats> ();
		mygames.Add (GameOne);
		mygames.Add (GameTwo);
		mygames.Add (GameThree);
		return mygames;
	}

}

[Serializable]
public class GameStats //this object holds all of our game stats. we can store things like level stats or other misc achievements here
{
	public bool existingFile;

	public float PlayTime;

	public float treasureCollected;

	public LevelStats TutorialLevelStats = new LevelStats();
	public LevelStats EarthLevelStats = new LevelStats();
	public LevelStats FireLevelStats = new LevelStats();
	public LevelStats WaterLevelStats = new LevelStats();
	public LevelStats AirLevelStats = new LevelStats();

	public LevelStats GetStats(LevelToUnlock GetLevel){
		switch (GetLevel) {
		case LevelToUnlock.tutorial:
			return TutorialLevelStats;
			break;
		case LevelToUnlock.earthLevel:
			return EarthLevelStats;
			break;
		case LevelToUnlock.airLevel:
			return AirLevelStats;
			break;
		case LevelToUnlock.fireLevel:
			return FireLevelStats;
			break;
		case LevelToUnlock.iceLevel:
			return WaterLevelStats;
		}
			return null;
		}
	public void SetStats(LevelStats stats){
		switch (stats.thisLevel) {
		case LevelToUnlock.airLevel:
			AirLevelStats = stats;
			break;
		case LevelToUnlock.earthLevel:
			EarthLevelStats = stats;
			break;
		case LevelToUnlock.fireLevel:
			FireLevelStats = stats;
			break;
		case LevelToUnlock.iceLevel:
			WaterLevelStats = stats;
			break;
		case LevelToUnlock.tutorial:
			TutorialLevelStats = stats;
			break;
		}
	}

	public RelicManager assignAbilities(RelicManager inMan){
		RelicManager myMan = inMan;

		myMan.dashRelic = !AirLevelStats.locked;
		myMan.jumpRelic = !FireLevelStats.locked;
		myMan.wallJumpRelic = !WaterLevelStats.locked;
		myMan.slashRelic = !EarthLevelStats.locked;
		return myMan;
	}



}

[Serializable]
public class LevelStats //level stats is used to save attriubtes of levels. right now we are just saving the lockmode, but later it will be easy to expand to saving
//things like player scores and times and things as well.
{
	public LevelToUnlock thisLevel;
	public bool locked = true;
	public float relicsCollected;
	public float totalRelics;

	public LevelGrade grade;

	public string completionTime;

	public List<SerializableTreasure> RedTreasuresRemaining = new List<SerializableTreasure>();
	public List<SerializableTreasure> GreenTreasuresRemaining = new List<SerializableTreasure>();
	public List<SerializableTreasure> BlueTreasuresRemaining = new List<SerializableTreasure>();
	public List<SerializableTreasure> YellowTreasuresRemaining = new List<SerializableTreasure>();


}

