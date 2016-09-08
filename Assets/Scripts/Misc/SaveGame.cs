using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//This is our save game tool. The save game tool holds the level stats class (the stats that each level can have) as well as the game stats class (the stats that
//the game can have). This class can be used to load the game stats from a file, update gamestats from a file, as well as save those game stats back to the 
//original file. 

public enum LevelToUnlock //these will be used to decide what level we are unlocking, expand this if we need to unlock more stuff
{
    tutorial,
    levelOne,
    levelTwo,
    levelThree,
    levelFour
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



public class SaveGame : MonoBehaviour { //This class will save the game.
   string saveDataFile = "/SaveData.dat";
   public GameStats loadStats;
   public static SaveGame GameSaver;

    void Awake()
    {
   
        if(GameSaver == null)
        {
            GameSaver = this.gameObject.GetComponent<SaveGame>();
           ;
            
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);


    }


    public void UpdateSave(LevelToUnlock level, LockMode lockType) //this function will be called to update the save file. Just pass in what level you want to update
        //and how you want to update it. This will need to be expanded later on (and potentially overloaded) when we start recording more stats
    {
        if (GameSaver == null)
        {
            this.gameObject.GetComponent<SaveGame>();

        }


       // if (loadStats == null) //make sure we load the game at least once before saving, so we're not setting null values
       // {
       //     LoadGame(); //LoadGame should be ran during the opening of the game, this is just a failsafe
       // }

        LevelStats newStats = new LevelStats(); //we create a new stats object.
        newStats.thisLevel = level; //give the new object the level that we passed in
        newStats.locked = lockType; //as well as how we want this level to be locked

        //later we will set more stats here

        switch (level) //this switch is a little dirty, but not too bad. It could be switched later with a more dynamic system but given the current scope of the game this works fine
        {
            case LevelToUnlock.tutorial:
                loadStats.TutorialLevelStats = newStats; //set the stats of whichever level we're working on
                break;
            case LevelToUnlock.levelOne:
                loadStats.LevelOneStats = newStats;
                break;
            case LevelToUnlock.levelTwo:
                loadStats.LevelTwoStats = newStats;
                break;
            case LevelToUnlock.levelThree:
                loadStats.LevelThreeStats = newStats;
                break;
            case LevelToUnlock.levelFour:
                loadStats.LevelFourStats = newStats;
                break;

            default:
                Debug.LogError("Trying to save an invalid value");
                break;

        }

        Save(); //update the save file after we have update the gamestats

    }

    public void LoadGame() //this should be ran in the beginning of the game
    {

        GameStats curStats = new GameStats(); //create a new stats object, which contains all of the stats of our game
        if (File.Exists(Application.persistentDataPath + saveDataFile)) //check if we have a file to open
        {
            BinaryFormatter loadBf = new BinaryFormatter();
            FileStream loadFS = File.Open(Application.persistentDataPath + saveDataFile, FileMode.Open);
            curStats = (GameStats)loadBf.Deserialize(loadFS);
        }
        else
        {
            curStats = CreateNewSave();
            loadStats = curStats;
            Save();

        }
       
        loadStats = curStats; 
    }

   


    public void Save() //this should be ran by the UpdateSave function. After we have update some data we will write it to our save file
    {
       

        BinaryFormatter saveBf = new BinaryFormatter(); //open a formatter
        FileStream saveFile = File.Create(Application.persistentDataPath + saveDataFile); //open the file      
        saveBf.Serialize(saveFile, loadStats); //save the object that we loaded previously. This should have stats updated before we run save saves.
        saveFile.Close();                                   
    }

    public GameStats CreateNewSave()
    {
        GameStats newGame = new GameStats();
        LevelStats newGameStats = new LevelStats();

        newGameStats.thisLevel = LevelToUnlock.tutorial;
        newGameStats.locked = LockMode.locked;
        newGame.TutorialLevelStats = newGameStats;

        newGameStats.thisLevel = LevelToUnlock.levelOne;
        newGameStats.locked = LockMode.locked;
        newGame.LevelOneStats = newGameStats;

        newGameStats.thisLevel = LevelToUnlock.levelTwo;
        newGameStats.locked = LockMode.locked;
        newGame.LevelTwoStats = newGameStats;

        newGameStats.thisLevel = LevelToUnlock.levelThree;
        newGameStats.locked = LockMode.locked;
        newGame.LevelThreeStats = newGameStats;

        newGameStats.thisLevel = LevelToUnlock.levelFour;
        newGameStats.locked = LockMode.locked;
        newGame.LevelFourStats = newGameStats;

        return newGame;
    }


}

[Serializable]
public class GameStats //this object holds all of our game stats. we can store things like level stats or other misc achievements here
{
    public LevelStats TutorialLevelStats;
    public LevelStats LevelOneStats;
    public LevelStats LevelTwoStats;
    public LevelStats LevelThreeStats;
    public LevelStats LevelFourStats;
}

[Serializable]
public class LevelStats //level stats is used to save attriubtes of levels. right now we are just saving the lockmode, but later it will be easy to expand to saving
                   //things like player scores and times and things as well.
{
    public LevelToUnlock thisLevel;
    public LockMode locked;
	public float relicCompletion;
	public LevelGrade grade;
}



