using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class Lava : MonoBehaviour {

	public float LavaDamage = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			col.GetComponent<Health> ().TakeDamage (LavaDamage);
            Debug.Log("begining to die (on the inside)");
            col.GetComponent<Death> ().PlayerDeath () ;
		

		}
	}

}
