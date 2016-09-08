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
	
			col.GetComponent<Death> ().PlayerDeath () ;
		

		}
	}

}
