using UnityEngine;
using System.Collections;

//This is the new treasure object, it will store the treasure type, state, and index.

public enum TreasureType{ //different types
	red,
	blue,
	green,
	yellow
}

public enum TreasureState{ //different states
	pickedUp,
	lost,
	notPickedUp
}

public class Treasure : MonoBehaviour {

	public TreasureType _treasureType; //this is my type (color)

	[SerializeField]
	private TreasureState myState = TreasureState.notPickedUp; //this is my state

	[SerializeField]
	public int MyIndex; //this is my index, we will be saving treasures by index so we can not load in collected treasures

	void Start(){
		Initi (); //initilize the treasure
	}

	void Initi(){
		TreasureManager.GetManager.AddTreasure (this); //add treasure to the list, it will sort the treasues based off of treasure type
	}

	public bool IsCollected{
		get {if (myState == TreasureState.pickedUp) {
				return true;
			} else {
				return false;
			}
		}
	}

	public void ChangeState(TreasureState newState){ //this will be ran to change the state
		switch (newState){
		case TreasureState.pickedUp: //picked up is ran by the player when they collect it
			myState = newState;
			this.gameObject.SetActive (false);
			break;
		case TreasureState.lost: //lost is ran if the player takes damage after picking it up
			myState = newState;
			this.gameObject.SetActive (true);
			break;
		case TreasureState.notPickedUp: //not picked up is the default treasure state if the player has not picked it up
			myState = newState;
			break;
		}


	}


}
