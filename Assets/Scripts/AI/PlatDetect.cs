using System;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.AI {
	public class PlatDetect : MonoBehaviour {

		[HideInInspector]
		public bool canJump;

		void Start(){
			canJump = false;
		}

		void Update () {
			//Debug.Log (canJump);
		}

		void OnTriggerStay (Collider col) {
			//Debug.Log ("Is colliding with: " + col.tag);
			if (col.tag == "MovingPlat") {
				canJump = true;
			}
		}
		void OnTriggerExit (Collider col) {
			//Debug.Log ("Is not colliding");
			if (col.tag == "MovingPlat") {
				canJump = false;
			}
		}
	}
}
