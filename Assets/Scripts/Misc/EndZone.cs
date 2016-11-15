using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

public class EndZone : MonoBehaviour {


	void OnTriggerEnter(Collider col){
		if (col.gameObject.GetComponent<CharController> () != null) {
			EndZoneTrigger ();		
		}
	}

	void EndZoneTrigger(){
		LevelData myData = new LevelData ();
		myData.GetData ();
		GameManager.UpdateSave (myData);
		DisplayEndLevelStats.getEndStats.FeedEndLevelStats (myData);
	}


	void Update(){
		if (Input.GetKeyDown (KeyCode.P)) {
			Debug.Log (GameManager.GetGameStats.FireLevelStats.completionTime);
		}



	}




}
