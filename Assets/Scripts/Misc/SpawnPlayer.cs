using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Awake () {
		//Transform spawnPoint = GameObject.FindGameObjectWithTag ("StartPoint").transform;

		Instantiate (player, transform.position, Quaternion.Euler(0, 120, 0));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
