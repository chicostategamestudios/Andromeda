using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class SpawnPlayer : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Awake () {
		//Transform spawnPoint = GameObject.FindGameObjectWithTag ("StartPoint").transform;

		Instantiate (player, transform.position, Quaternion.Euler(0, 120, 0));
		if (player.gameObject.GetComponent<RelicManager> () != null) {

		} else {
		//	Debug.LogError("Player 
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
