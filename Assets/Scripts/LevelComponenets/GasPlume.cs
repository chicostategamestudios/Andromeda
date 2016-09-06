using UnityEngine;
using System.Collections;

public class GasPlume : MonoBehaviour {

	ParticleSystem mySystem;
	//add variable for easy adjusting


	void GasBurst () {
		mySystem = GetComponentInChildren<ParticleSystem> ();
		mySystem.Play ();
	}

	void Awake () {
		InvokeRepeating ("GasBurst", 1, 5);

	}
}
