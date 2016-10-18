using UnityEngine;
using System.Collections;

public enum TreasureType{
	red,
	blue,
	green,
	yellow
}

public enum TreasureState{
	pickedUp,
	lost,
	notPickedUp
}

public class Treasure : MonoBehaviour {

	public TreasureType _treasureType;

	public TreasureState myState;

	public int MyIndex;

	void Start(){
		Initi ();
	}

	void Initi(){
		TreasureManager.GetManager.AddTreasure (this);
	}

	void ChangeState(TreasureState newState){
		switch (newState){
		case TreasureState.pickedUp:
			myState = newState;

			break;
		case TreasureState.lost:
			myState = newState;
			break;
		case TreasureState.notPickedUp:
			myState = newState;
			break;
		}


	}


}
