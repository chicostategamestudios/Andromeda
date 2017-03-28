//This script was written by Micheal Kennedy | Last edited by Zachary Coon | Modified on Feb 7, 2017

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.Character;
using System.Collections.Generic;

public class DisplayEndLevelStats : MonoBehaviour {

	private static DisplayEndLevelStats EndLevelStats;

	private Text greenTreasureTextEnd, redTreasureTextEnd, blueTreasureTextEnd, yellowTreasureTextEnd, timerEndText, endGradeText;

	private string green = "EndGreen";
	private string yellow = "EndYellow";
	private string red = "EndRed";
	private string blue = "EndBlue";
	private string timer = "Time";
	private string grade = "Grade";

    //public varibles for relic sprites and spawning them on the menu dynamically
    [Tooltip("The sprites for blue relics, set size to 3. the first sprite should be the picked up sprite, second should be not picked up sprite, and last should be the lost sprite")]
    public Sprite[] blue_relic_sprites; // 0 is the picked up sprite, 1 is the not picked up sprite, 2 is the lost sprite for the blue relic 
    [Tooltip("The sprites for red relics, set size to 3. the first sprite should be the picked up sprite, second should be not picked up sprite, and last should be the lost sprite")]
    public Sprite[] red_relic_sprites; // 0 is the picked up sprite, 1 is the not picked up sprite, 2 is the lost sprite for the red relic 
    [Tooltip("The sprites for green relics, set size to 3. the first sprite should be the picked up sprite, second should be not picked up sprite, and last should be the lost sprite")]
    public Sprite[] green_relic_sprites; // 0 is the picked up sprite, 1 is the not picked up sprite, 2 is the lost sprite for the green relic 
    [Tooltip("The sprites for yellow relics, set size to 3. the first sprite should be the picked up sprite, second should be not picked up sprite, and last should be the lost sprite")]
    public Sprite[] yellow_relic_sprites; // 0 is the picked up sprite, 1 is the not picked up sprite, 2 is the lost sprite for the yellow relic 
    [Tooltip("A game object with an image component on it")]
    public GameObject relic_icon_prefab; //An object to clone and set an image to for displaying relic icons

    //private varibles for sprites and spawning them on the menu dynamically
    private GameObject spawn_relics_at; //an object with a gridlayoutgroup
    private List<GameObject> my_relics = new List<GameObject>(); //a list of the icons to display at the end of the level in a grid



    public GameObject FinalScreenCanvas;

	void Awake(){
		EndLevelStats = this.gameObject.GetComponent<DisplayEndLevelStats> ();
        
    }

	// Use this for initialization
	void Start () {
		

		Text[] colorTxt = GetComponentsInChildren<Text> ();
		for (int color = 0; color < colorTxt.Length; color++) {
			if (colorTxt [color].transform.name.Contains (green)) {
				greenTreasureTextEnd = colorTxt [color];
			}
			if (colorTxt [color].transform.name.Contains (red)) {
				redTreasureTextEnd = colorTxt [color];
			}
			if (colorTxt [color].transform.name.Contains (yellow)) {
				yellowTreasureTextEnd = colorTxt [color];
			}
			if (colorTxt [color].transform.name.Contains (blue)) {
				blueTreasureTextEnd = colorTxt [color];
			}
			if (colorTxt [color].transform.name.Contains (timer)) {
				timerEndText = colorTxt [color];
			}
			if (colorTxt [color].transform.name.Contains (grade)) {
				endGradeText = colorTxt [color];
			}
		}

        spawn_relics_at = this.transform.GetComponentInChildren<GridLayoutGroup>().gameObject;

        //EndLevelStats = this;
        FinalScreenCanvas.gameObject.SetActive (false);

	//	FeedEndLevelStats (1, 2, 3, 4, "asdf");
	}

	public static DisplayEndLevelStats getEndStats{
		get {
			if (EndLevelStats == null) {
				EndLevelStats = FindObjectOfType<DisplayEndLevelStats> ();
			}

			return EndLevelStats; }
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("Start")) {
			CharController myCont = CharController.Instance;
			myCont.endScene();

			//CharController.endScene();
			SceneManager.LoadScene (SceneRef.getLevelSelect);
			
		}


	}

    /*
     * @Authour Micheal Kennedy
     *  - Edited by Zachary Coon
     * FeedEndLevelStats(LevelData)
     * Displays end game statistics
     * Relics and their collected status in the level, and time it took to compelete the level
     */
	public void FeedEndLevelStats (LevelData myData) {


       /* foreach (Treasure current_treasure in )
        {
            SpawnTreasureIcon(current_treasure, blue_relic_sprites);
        }*/

        foreach (Treasure current_treasure in TreasureManager.GetManager.getTreasureList(TreasureType.red))
        {
            SpawnTreasureIcon(current_treasure, red_relic_sprites);
        }

        foreach (Treasure current_treasure in TreasureManager.GetManager.getTreasureList(TreasureType.green))
        {
            SpawnTreasureIcon(current_treasure, green_relic_sprites);
        }

        foreach (Treasure current_treasure in TreasureManager.GetManager.getTreasureList(TreasureType.yellow))
        {
            SpawnTreasureIcon(current_treasure, yellow_relic_sprites);
        }




        timerEndText.text = "Time: " + myData.getFinaltime;
		//endGradeText.text = "Grade: A";

		FinalScreenCanvas.gameObject.SetActive (true);
	}

    /*
     * @Author: Zachary Coon
     * SpawnTreasureIcon(Treasure, Sprite[])
     * Adds Treasure to a list to be displayed
     * 
     * @Param Treasure treasure: the treasure object to be added to the list
     * @Param Sprite[] sprites: an array of sprites with 3 different states, [0] = picked up, [1] = not picked up, [2] = lost
     */
    private void SpawnTreasureIcon(Treasure treasure, Sprite[] sprites)
    {
        //clone object and add it to the list, then parent it for the grid layout group to work properly. Scale it to fit the screen nicely
        GameObject current_relic_icon = GameObject.Instantiate(relic_icon_prefab, this.transform.position, this.transform.rotation) as GameObject;
        //my_relics.Add(current_relic_icon);
        current_relic_icon.transform.SetParent(spawn_relics_at.transform, false);
        current_relic_icon.transform.localScale = new Vector3(1.9f, 0.7f, 0.4f);

        //if treasure is picked up, then display the picked up sprite
        //else if it isn't picked up, the display the not picked up sprite
        // else if it is lost, then display the lost sprite
        // else throw error
        if (treasure.myState == TreasureState.pickedUp)
        {
            current_relic_icon.GetComponent<Image>().sprite = sprites[0];
        }
            else if (treasure.myState == TreasureState.notPickedUp)
        {
            current_relic_icon.GetComponent<Image>().sprite = sprites[1];
        }
            else if (treasure.myState == TreasureState.lost)
        {
            current_relic_icon.GetComponent<Image>().sprite = sprites[2];
        }
            else
        {
            Debug.LogError("Spawning Treasure Icons error, unexpected treasure state in Display End Level Stats: Function Spawn Treasure Icon");
        }
    }
}
