using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class SpawnPlayer : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Awake () {
		//Transform spawnPoint = GameObject.FindGameObjectWithTag ("StartPoint").transform;

		Instantiate (player, transform.position, Quaternion.Euler(0, 0, 0));
		if (player.gameObject.GetComponent<RelicManager> () != null) {
			if (GameManager.GetGameStats != null) {
				RelicManager newValues = GameManager.GetGameStats.assignAbilities (player.gameObject.GetComponent<RelicManager> ());
				RelicManager playerValues = player.gameObject.GetComponent<RelicManager> ();
				playerValues.dashRelic = newValues.dashRelic;
				playerValues.jumpRelic = newValues.jumpRelic;
				playerValues.slashRelic = newValues.slashRelic;
				playerValues.wallJumpRelic = newValues.wallJumpRelic;
			}
		} else {
		//	Debug.LogError("Player 
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
