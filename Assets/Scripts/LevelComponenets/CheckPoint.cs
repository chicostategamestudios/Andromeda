using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class CheckPoint : MonoBehaviour {
	Transform myChild;
	void Start(){
		if (this.gameObject.transform.childCount > 1) {
			Debug.LogError ("For some reason the checkpoint: " + this.transform.name + "has way too many children");
		}

		myChild = this.gameObject.transform.GetChild (0);

	}

	void OnTriggerEnter(Collider col){
		if ((col.gameObject.tag == "Player") && (col.gameObject.GetComponent<Death> () != null)) {
			col.gameObject.GetComponent<Death> ().SetCheckPoint (myChild.transform.position);
		
		} else {
			
		}

	}



}
