using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class DeleteGame : MonoBehaviour {

    private static MyGames _myGames;
    private static GameStats myGame;
    private static MyLoadedGame curGame;
    private static SaveGame GameSaver;


    public static List<GameStats> myLoadedGames = new List<GameStats>();
    //This will hold all three loaded games

    public float SetPlayTime;
    //This is just here so we can input fake data into game saves

    MyGames _gamesInstance;
    //_gamesInstance holds all three of our game saves. we use this to grab a 

    //public MyLoadedGame gameToSave; 
    //This enum can be changed to decide which level we want to save to

    public LockMode unlockStuff;
    //this is an example enum being used to decide if we should lock or unlock a level

    void Awake()
    {
        GetSavedGames(); //get saved games is the first thing we should run, particularly during our initial
                         //"loading screen" to get all of our game files, once we have ran GetSavedGames, _gamesInstance is 
                         //filled and we can start grabbing game files from inside
    }

    void Start()
    {

    }


	void FixedUpdate () {
	    if (Input.GetButtonDown("Jump"))
        {
            GameStats curDelete;
            if (curGame == MyLoadedGame.GameOne)
            {
                curDelete = GetComponent<SaveGame>().CreateNewSave();
                _myGames.GameOne = curDelete;
            }
            else if (curGame == MyLoadedGame.GameOne)
            {
                curDelete = GetComponent<SaveGame>().CreateNewSave();
                _myGames.GameTwo = curDelete;
            }
            else if (curGame == MyLoadedGame.GameOne)
            {

            }
            if (curGame == MyLoadedGame.GameOne)
            {
                curDelete = GetComponent<SaveGame>().CreateNewSave();
                _myGames.GameThree = curDelete;
            }
            Debug.Log("Delete Button Pressed");
        }
	}

    public void GetSavedGames()
    { //Get saved games is ran during the game select screen, this grabs the base file 
      //of all of our game saves
        myLoadedGames.Clear(); //my loaded games is a list that holds games 1-3, we store those in a static list for easy access
        if (_gamesInstance == null)
        {
            _gamesInstance = new MyGames(); //if we don't have an instance of _gamesIntance, make a new one
                                            //just to store data in when we load the file
        }
        if (GameSaver == null)
        {
            GameSaver = SaveGame.GetGameSaver; //if we don't have a refrence to the GameSaver, get one
                                               //This will also make GameSaver finding itself it has not yet 
                                               //found an instance of itself
        }

        _gamesInstance = GameSaver.GetMyGames();
        //GameSaver.GetMyGames will return our full save file, which are storing in the
        //local variable, _gamesInstance

        myLoadedGames.Add(_gamesInstance.GameOne); //once we have our game file, add each save to the list
        myLoadedGames.Add(_gamesInstance.GameTwo); //we don't technically need to do this, I was just doing it
        myLoadedGames.Add(_gamesInstance.GameThree); //for ease of testing.

    }

    public void OpenSavedgame(MyLoadedGame gameToDelete)
    { //open Saved Game is ran when we choose a game file
      //this should be ran in the Game Select Screen, and will set 
      //our game saver to that game file, so we are only working on 
      //and modifying the values of that one game save
        GameSaver.SetGame(gameToDelete);
    }
}
