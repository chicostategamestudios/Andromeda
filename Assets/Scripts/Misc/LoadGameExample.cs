using UnityEngine;
using System.Collections;

public class LoadGameExample : MonoBehaviour {

    public bool LoadGame;



    public bool UpdateGameSave;


    public LevelToUnlock whichLevel;
    public LockMode lockMode;

    GameStats myStats;
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.L))
        {
          //    SaveGame.GameSaver.LoadGames();
           
        }

        if (UpdateGameSave)
        {

            //SaveGame.GameSaver.UpdateSave(whichLevel, lockMode);

            UpdateGameSave = false;

        //   myStats = SaveGame.GameSaver.loadStats;
            Debug.Log(myStats.LevelOneStats.thisLevel + " : "+ myStats.LevelOneStats.locked );

            Debug.Log(myStats.LevelTwoStats.thisLevel + " : " + myStats.LevelTwoStats.locked);

            Debug.Log(myStats.LevelThreeStats.thisLevel + " : " + myStats.LevelThreeStats.locked);

            Debug.Log(myStats.LevelFourStats.thisLevel + " : " + myStats.LevelFourStats.locked);





        }
        
        





	}
}
