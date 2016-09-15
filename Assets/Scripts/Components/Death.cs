using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Components
{
	public class Death :CustomComponentBase {
	//private GameObject spawner;
	Vector3 spawnPoint;
	public float deathTime =0.3f;
	public float waitToInput =0.3f;
	public bool dying = false;
	private Vector3 currentLoc;
	// Use this for initialization
	void Start () {
		//spawner = GameObject.FindGameObjectWithTag ("Spawn"); 
		spawnPoint = this.gameObject.transform.position;

		//transform.position = new Vector3 (spawner.transform.position.x, spawner.transform.position.y, this.transform.position.z);

		dying = false;

	}
	
		public void PlayerDeath(){
			PlayerMovement.moveVector = Vector3.zero;
			PlayerMovement.verticleSpeed = 0f;
			StartCoroutine ("Respawn");
		}


	public IEnumerator Respawn(){ //manages respawning 
		currentLoc = new Vector3 (transform.position.x, transform.position.y, transform.position.z); //get the player position
		dying = true; //set death to true, this stops the update loop of the main movement controller
		yield return new WaitForSeconds (deathTime); //wait for however long
		
		transform.position = spawnPoint;
						//move the player to the spawner location
		yield return new WaitForSeconds (waitToInput); //how long after respawner should the player wait before they can input again
														//currently not implimented 
			dying = false; //set death back to false, letting the player control themselves again.
	}

	public void SetCheckPoint(Vector3 newPos){
		spawnPoint = newPos;
	}


}
}