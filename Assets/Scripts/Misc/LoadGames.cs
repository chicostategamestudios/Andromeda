using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Components;

public class LoadGames : MonoBehaviour {

	//This script is an example of how to load the game, as well as other load and 
	//save features


	public static List<GameStats> myLoadedGames = new List<GameStats>();
	//This will hold all three loaded games

	public float SetPlayTime;
	//This is just here so we can input fake data into game saves

	MyGames _gamesInstance;
	//_gamesInstance holds all three of our game saves. we use this to grab a 

	//public MyLoadedGame gameToSave; 
	//This enum can be changed to decide which level we want to save to


	SaveGame GameSaver;
	//This is just my instance of my game saver. 

	public LockMode unlockStuff;
	//this is an example enum being used to decide if we should lock or unlock a level

	void Awake(){
		GetSavedGames (); //get saved games is the first thing we should run, particularly during our initial
		//"loading screen" to get all of our game files, once we have ran GetSavedGames, _gamesInstance is 
		//filled and we can start grabbing game files from inside
	}

	public void testingSomething(LevelToUnlock myLevel){
		GameManager.LoadLevel (myLevel);
	}


	void Update(){

		if (Input.GetKeyDown (KeyCode.F1)) {

			OpenSavedgame (MyLoadedGame.GameOne); //open saved game will be ran when we chose the level we want to open
			//we'll probably be running this by UI buttons, just choosing Game File 1-3 with an enum
		} else if (Input.GetKeyDown (KeyCode.F2)) { //Update values will be used at the end of each level
			//to "save" the game. Update values will pull down the current game file, change whatever values we need
			//to change, and save those new values back to the game save file

			UpdateValues ();

		} else if (Input.GetKeyDown (KeyCode.F3)) {

			LoadLevel ();
			//Load Level is an example of actually loading a game level
			//right now im just displaying what loading in the treasure will look like
			//because loading the treasure is kind of weird 


		} 

	}

	public void GetSavedGames(){ //Get saved games is ran during the game select screen, this grabs the base file 
		//of all of our game saves
		myLoadedGames.Clear (); //my loaded games is a list that holds games 1-3, we store those in a static list for easy access
		if (_gamesInstance == null) {
			_gamesInstance = new MyGames (); //if we don't have an instance of _gamesIntance, make a new one
			//just to store data in when we load the file
		}
		if (GameSaver == null) {
			GameSaver = SaveGame.GetGameSaver; //if we don't have a refrence to the GameSaver, get one
			//This will also make GameSaver finding itself it has not yet 
			//found an instance of itself
		}

		_gamesInstance = GameSaver.GetMyGames ();
		//GameSaver.GetMyGames will return our full save file, which are storing in the
		//local variable, _gamesInstance

		myLoadedGames.Add (_gamesInstance.GameOne); //once we have our game file, add each save to the list
		myLoadedGames.Add (_gamesInstance.GameTwo); //we don't technically need to do this, I was just doing it
		myLoadedGames.Add (_gamesInstance.GameThree); //for ease of testing.

	}

	public void OpenSavedgame(MyLoadedGame gameToSave){ //open Saved Game is ran when we choose a game file
		//this should be ran in the Game Select Screen, and will set 
		//our game saver to that game file, so we are only working on 
		//and modifying the values of that one game save
		GameSaver.SetGame (gameToSave);
	}


	public void UpdateValues(){ //Update Values is how we save the game



		GameStats updateGame = new GameStats (); //First we make a new gamestats object

		updateGame = GameSaver.GetGameStats; //and set it to the stats of the game we're currently working on

		//**************This is an example of how we're saving the Early Level
		/*
		LevelStats EarthLevel = new LevelStats (); //first we make a level stats object
		EarthLevel = updateGame.EarthLevelStats; //and fill it with the level stats of our current game file

		EarthLevel.setLocked (true); //we can modify whatever stats we want for that level

		updateGame.PlayTime = SetPlayTime; //as well as for the game file itself
		*/

		GameSaver.UpdateSave (updateGame); //and then we run UpdateSave on our Game Stats, which will
		//update get the game save file, modify it's values to these new values, and then save it back out

	}


	public void SaveLevelStats(){
		//This is an example of saving a game, specifically the treasures. 

		//since we can't save Transforms of treasures that were uncollected, we are instead just going to save the position
		//This will work fine for final versions, but could run into some issues
		//when we are doing itirations with Level Design, particularly if we are saving games, moving treaurses,
		//and then loading games, things won't be consistant. 
		_gamesInstance.GameOne.AirLevelStats.TreasuresRemaining.Clear ();
		for (int treasure = 0; treasure < Health.myLoot.Count; treasure++) {
			_gamesInstance.GameOne.AirLevelStats.TreasuresRemaining.Add (Health.myLoot [treasure].position);
		}


	}


	public void LoadLevel(){ //This is an exmaple of loading the treasures in a level, this function should not actually be 
		//here, and should be ran in some sort of Level or Scene manager 

		for (int relic = 0; relic < Health.myLoot.Count; relic++) { //First we get all of the relics in the level,
			//They add themselves to this list in awake, which means that this
			//function needs to be ran in start 
			if (_gamesInstance.GameOne.AirLevelStats.TreasuresRemaining.Contains (Health.myLoot [relic].position)) { 
				//Treasure remains is a list of positions of the treasures
				//that we're uncollected the last time we went through the level
				//so we check our levels relics against the 
				//treasures that should be active

				Health.myLoot [relic].gameObject.SetActive (true);
				//if they are in the list of relics remaining
				//just leave them active
			} else {
				GameObject newLoot = Health.myLoot [relic].gameObject; //if they are not in our list of remaining treasures 
				Health.myLoot.Remove (Health.myLoot [relic]);			//we remove them from our list
				Destroy (newLoot);										//and destroy it from the scene

			}
		}
	}
}
