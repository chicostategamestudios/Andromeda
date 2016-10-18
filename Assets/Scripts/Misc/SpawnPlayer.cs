using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class SpawnPlayer : MonoBehaviour {

	public GameObject player;


	// Use this for initialization
	void Start () {
		//Transform spawnPoint = GameObject.FindGameObjectWithTag ("StartPoint").transform;

		GameObject _player = (GameObject)Instantiate (player, transform.position, Quaternion.identity);
		SetStartAbilities (_player);
	}


	void SetStartAbilities(GameObject myPlayer){
		RelicManager playerSkills = myPlayer.GetComponent<RelicManager> ();
		GameStats currentGame = GameManager.GetGameStats;
		if (currentGame.FireLevelStats.locked) {
			playerSkills.jumpRelic = false;
		} else {
			playerSkills.jumpRelic = true;
		}

		if (currentGame.AirLevelStats.locked) {
			playerSkills.dashRelic = false;
		} else {
			playerSkills.dashRelic = true;
		}

		if (currentGame.EarthLevelStats.locked) {
			playerSkills.slashRelic = false;
		} else {
			playerSkills.slashRelic = true;
		}

		if (currentGame.WaterLevelStats.locked) {
			playerSkills.wallJumpRelic = false;
		} else {
			playerSkills.wallJumpRelic = true;
		}




	}



}
