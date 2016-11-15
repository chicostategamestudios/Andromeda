using System;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.AI {
	public class ContAIInput : MonoBehaviour {
		public bool jump;
		public int aiHorzInput;
		public bool dash;
		public bool slash;
		public bool waitingForPlatform;
		public GameObject platformCol;

		private bool didDoubleJump;

		void Start () {
			didDoubleJump = false;
		}

		void Update () {
			if (AI.AIPlayerMovement.grounded) {
				didDoubleJump = false;
			}
		}

		void OnTriggerStay (Collider col) {
			if (col.tag == "AIPlayer") {
				if (!waitingForPlatform) {
					AICharController.aiHorzInput = aiHorzInput;
					if (jump && !didDoubleJump) {
						AIPlayerMovement.verticleSpeed = AI.AIPlayerMovement.jumpHeight;
						didDoubleJump = true;
					}
					if (dash) {
						AICharController.aiDash = true;
					}
					if (slash) {
						AICharController.aiSlash = true;
					}
				}
				if (waitingForPlatform) {
					AICharController.aiHorzInput = 0;
					Debug.Log ("Waiting for Platform");
					if (platformCol == null) {
						Debug.LogError ("No Platform Found!");
					} 
					else {
						if (platformCol.GetComponent<PlatDetect> ().canJump) {
							Debug.Log ("Jumping");
							AICharController.aiHorzInput = aiHorzInput;
							if (jump) {
								AICharController.aiJump = true;
							}
						}
					}
				}
			}
		}
	}
}
