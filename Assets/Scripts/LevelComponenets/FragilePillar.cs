using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class FragilePillar : MonoBehaviour {
	Transform PillarBotHalf;
	BoxCollider CrackTrigger;
	public Jumping playerjumping;
	bool canDestroy;
	Vector3 botHalfPos;
	// Use this for initialization
	void Start () {

		LevelReset.myLevelElements.Add(this);
		PillarBotHalf = this.gameObject.transform.FindChild ("BotHalf");
		CrackTrigger = this.gameObject.GetComponent<BoxCollider> ();
		botHalfPos = PillarBotHalf.transform.position;
	}
	

	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "Player") {
			canDestroy = true;
		
			playerjumping = col.gameObject.GetComponent<Jumping> ();
			}
		}

	void OnTriggerExit(Collider col){
		canDestroy = false;
	}

	void Update(){
		if (canDestroy&& playerjumping.JustwallJumped) {
			PillarBotHalf.GetComponent<Rigidbody> ().isKinematic = false;
			CrackTrigger.enabled = false;
		}

	}

	public void Reset()
	{
		PillarBotHalf.GetComponent<Rigidbody>().isKinematic = true;
		PillarBotHalf.transform.position = botHalfPos;
		CrackTrigger.enabled = true;

	}


}
