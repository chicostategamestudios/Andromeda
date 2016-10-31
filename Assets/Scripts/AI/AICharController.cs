using System;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.AI {
	
	public class AICharController : AI.AIControllerBase {

		public static CharacterController _aiController;

		AI.AIPlayerMovement _movement;
		AI.AIRelicManager _relics;
		AI.AISlash _slash;
		AI.AIHealth _health;

		public static int aiHorzInput;
		public static bool aiJump;
		public static bool aiDash;
		public static bool aiSlash;
		float playerDirection;
		float lastDir =1f;
		void Awake(){
			_aiController = GameObject.FindGameObjectWithTag ("AIPlayer").GetComponent<CharacterController> ();
			_movement = gameObject.AddComponent<AI.AIPlayerMovement> ();
			_slash = gameObject.AddComponent<AI.AISlash> ();
			_relics = gameObject.AddComponent<AI.AIRelicManager> ();
			_health = gameObject.AddComponent<AI.AIHealth> ();
			_health.TotalStuff ();
		}

		void Update(){
			GetInput ();
			_movement.MovePlayer (playerDirection);
			_movement.WallGrab ();
			//_movement.DashHandler (playerDirection);
			aiJump = false;
			aiSlash = false;
			aiDash = false;

		}
		void FixedUpdate(){
			_relics.AbilityManager ();
		}

		void GetInput() //gets input to be used in the manageInput function, subject to be removed once a input manager is implemeted
		{
			if (Mathf.Abs (aiHorzInput) > 0.15f) {
				if (aiHorzInput > 0) {
					playerDirection = 1f;
					lastDir = 1f;
				}
				if (aiHorzInput < 0) {
					playerDirection = -1f;
					lastDir = -1f;
				}
			} else {
				playerDirection = 0f;
			}


			//if ((Input.GetKeyDown(KeyCode.Space))
			if(aiJump)
			{

				_movement.JumpPlayer(playerDirection);

			}

			//if (Input.GetKeyDown (KeyCode.LeftShift)) {
			if(aiDash){
				_movement.DashPlayer();
			} 

			if (aiSlash) {
				_slash.SlashAttack (lastDir);
			}
		}
	}
}
