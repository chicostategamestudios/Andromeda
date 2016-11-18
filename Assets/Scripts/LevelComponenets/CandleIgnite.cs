using UnityEngine;
using System.Collections;

public class CandleIgnite : MonoBehaviour {

	public GameObject candleFlame;
	public Transform candleSpawn;

	// Use this for initialization
	void Start () {
	}
		
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			Instantiate (candleFlame, candleSpawn.position, transform.rotation);
		}
	}
}
