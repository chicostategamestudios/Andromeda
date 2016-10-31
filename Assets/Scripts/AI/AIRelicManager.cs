using UnityEngine;
using System.Collections;


namespace Assets.Scripts.AI
{
	public class AIRelicManager: AICustomComponentBase {
		public bool jumpRelic = true;
		public bool wallJumpRelic = true;
		public bool dashRelic = true;
		public bool slashRelic = true;

	

		public void ToggleJump (bool setTo){
			if (setTo) {
				AIJumping.maxJumps = 2;
			} else {
				AIJumping.maxJumps = 1;
			}
		}
		public void ToggleWallJump (bool setTo){
			if (setTo) {
				AIJumping.maxWallJumps = 999f;
			} else {
				AIJumping.maxWallJumps = 1f;
			}
		}
		public void ToggleDash (bool setTo){
			if (setTo) {
				AIDash.DashUnlocked = true;
			} else {
				AIDash.DashUnlocked = false;
			}
		}
		public void ToggleSlash (bool setTo){
			AISlash.canSlash = setTo;
		}

		public void AbilityManager(){
			//temp manipulation of the toggle functions for prototyping
			ToggleJump(jumpRelic);
			ToggleWallJump (wallJumpRelic);
			ToggleDash (dashRelic);
			ToggleSlash (slashRelic);

		}

	}
}