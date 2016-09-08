using UnityEngine;
using System.Collections;

public class GasPlume : MonoBehaviour {

	ParticleSystem mySystem;
	//add variable for easy adjusting
	public float burstDelay;
	public float plumeHeight;//particle lifetime


	void GasBurst () {
		mySystem = GetComponentInChildren<ParticleSystem> ();
		mySystem.startLifetime = plumeHeight;
		mySystem.Play ();
	}

	void Awake () {
		InvokeRepeating ("GasBurst", 1, burstDelay);

	}

	void OnTriggerStay(Collider other){
		//check if GasBurst == true
		//and if "fire" object is near

		if (other.gameObject.tag == "fire" && mySystem.isPlaying) {
			//ignite
		}
	}
}
