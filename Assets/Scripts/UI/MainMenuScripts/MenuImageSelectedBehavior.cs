//Original Author: Michael Kennedy? || Last Edited: Alexander Stamatis [A.S] | Modified on Feb 19, 2017

using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuImageSelectedBehavior : MonoBehaviour {

	public Sprite[] onOffSprites;
	public int selectionIndex;

	//added this Awake to load stuff, problem was that array size dissapeared in inspector and so did the connection with the sprites [A.S.] 
	void Awake(){
		//If the array is zero, fill it up with two empty ones
		if (onOffSprites.Length == 0) {
			onOffSprites = new Sprite[2];
		}

		//Alternative method than using Resources.Load [A.S]
		if (this.gameObject.name == "Restart") {
			onOffSprites[0] = Resources.Load("Restart (PauseMenu)", typeof(Sprite)) as Sprite;
            Debug.Log(onOffSprites[0]);
			onOffSprites[1] = Resources.Load("Restart (Selected)", typeof(Sprite)) as Sprite;
		}
		if (this.gameObject.name == "Level Select") {
			onOffSprites[0] = Resources.Load("Level Select (PauseMenu)", typeof(Sprite)) as Sprite;
			onOffSprites[1] = Resources.Load("Level Select (Selected)", typeof(Sprite)) as Sprite;
        }
		if (this.gameObject.name == "Save&Quit") {
			onOffSprites[0] = Resources.Load("Save&Quit (PauseMenu)", typeof(Sprite)) as Sprite;
			onOffSprites[1] = Resources.Load("Save&Quit (Selected)", typeof(Sprite)) as Sprite;
		}
		if (this.gameObject.name == "Yes") {
			onOffSprites[0] = Resources.Load("Yes (PauseMenu)", typeof(Sprite)) as Sprite;
			onOffSprites[1] = Resources.Load("Yes (Selected)", typeof(Sprite)) as Sprite;
		}
		if (this.gameObject.name == "No") {
			onOffSprites[0] = Resources.Load("No (PauseMenu)", typeof(Sprite)) as Sprite;
			onOffSprites[1] = Resources.Load("No (Selected)", typeof(Sprite)) as Sprite;
		}
	
	}

	void Update(){
		AssignProperSprite ();
	}

	void AssignProperSprite(){
		if (this.gameObject.GetComponentInParent<CursorIndexTracker> ().currentCursorIndex == selectionIndex) {
			this.GetComponent<Image> ().sprite = onOffSprites [1];
		} else {
			this.GetComponent<Image> ().sprite = onOffSprites [0];
		}
	}
}
