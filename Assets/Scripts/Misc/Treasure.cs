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

	public TreasureState myState = TreasureState.notPickedUp;


	public int MyIndex;

	void Start(){
		Initi ();
	}

	void Initi(){
		TreasureManager.GetManager.AddTreasure (this);
	}

	public void ChangeState(TreasureState newState){
		switch (newState){
		case TreasureState.pickedUp:
			myState = newState;
			this.gameObject.SetActive (false);
			break;
		case TreasureState.lost:
			myState = newState;
			this.gameObject.SetActive (true);
			break;
		case TreasureState.notPickedUp:
			myState = newState;
			break;
		}


	}


}
