using System;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.AI {
	public class ContAIInput : MonoBehaviour {
		public bool jump;
		public int aiHorzInput;
		public bool dash;
		public bool slash;

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
		}
	}
}
