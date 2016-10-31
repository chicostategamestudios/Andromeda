using System;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.AI {
	public class AIWallJumping : MonoBehaviour {



		// Update is called once per frame
		void OnTriggerStay (Collider col) {
			if (col.tag == "AIPlayer") {
				AICharController.aiJump = true;
			}
		}
	}
}