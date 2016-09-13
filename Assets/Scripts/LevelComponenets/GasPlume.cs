using UnityEngine;
using System.Collections;

public class GasPlume : MonoBehaviour {

	ParticleSystem mySystem;
	static public GasPlume S;
	public float burstDelay;
	public float plumeHeight;//particle lifetime
	public bool isIgnited;
	private Color myColor = Color.red; 

	void Awake () {
		mySystem = GetComponentInChildren<ParticleSystem> ();
		S = this;
		InvokeRepeating ("GasBurst", 1, burstDelay);

	}

	void GasBurst () {
		mySystem.startColor = Color.white;
		mySystem.startLifetime = plumeHeight;
		mySystem.Play ();
	}

	void Update(){
		if (isIgnited) {
			mySystem.startColor = myColor;
		}

		if (!mySystem.isPlaying) {
			isIgnited = false;
		}
	}

	void OnTriggerStay(Collider other){
		//check if GasBurst == true
		//and if "fire" object is near
		if (other.gameObject.tag == "fire" && mySystem.isPlaying) {
			//ignite
			isIgnited = true;
		}
	}
}
