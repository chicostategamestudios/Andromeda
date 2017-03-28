using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	void OnTriggerStay(Collider col){
		if (col.tag == "Player") {
			col.transform.parent = this.gameObject.transform;
			//object1.transform.parent = object2.transform 
		}
	
	}
	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			col.transform.parent = null;
			//object1.transform.parent = object2.transform 
		}

	}
}
