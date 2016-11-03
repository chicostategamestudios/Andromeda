using System;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.AI {
	public class AIInputs : MonoBehaviour {

		private bool playerProg;
		private bool playerRegr;
		public bool jump;
		public int aiHorzInput;
		public bool dash;
		public bool slash;
		public bool waitingForPlatform;
		public GameObject platformCol;

		void Start () {

		}
		
		// Update is called once per frame
		void Update () {
		
		}

		void OnTriggerEnter (Collider col) {
			if (col.tag == "Player") {
				playerProg = true;
			}
		}

		void OnTriggerStay (Collider col) {
			if (col.tag == "AIPlayer" && !playerProg) {
				if (col.gameObject.transform.position.x <= gameObject.transform.position.x + .5 && col.gameObject.transform.position.x >= gameObject.transform.position.x - .5) {
					AICharController.aiHorzInput = 0;
				}
			}
			if (col.tag == "AIPlayer" && playerProg) {
				if (!waitingForPlatform) {
					AICharController.aiHorzInput = aiHorzInput;
					if (jump) {
						AICharController.aiJump = true;
					}
					if (dash) {
						AICharController.aiDash = true;
					}
					if (slash) {
						AICharController.aiSlash = true;
					}
				}
				if (waitingForPlatform) {
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

		void OnTriggerExit (Collider col) {
			if (col.tag == "Player") {
				playerProg = false;
			}
		}
	}
}
