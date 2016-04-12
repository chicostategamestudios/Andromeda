﻿using UnityEngine;
using System.Collections;


namespace Assets.Scripts.Components
{




	public class RelicManager: CustomComponentBase {
		public bool jumpRelic;
		public bool wallJumpRelic;
		public bool dashRelic;
		public bool slashRelic;

	

		public void ToggleJump (bool setTo){
			if (setTo) {
				Jumping.maxJumps = 2;
			} else {
				Jumping.maxJumps = 1;
			}
		}
		public void ToggleWallJump (bool setTo){
			if (setTo) {
				Jumping.maxWallJumps = 999f;
			} else {
				Jumping.maxWallJumps = 1f;
			}
		}
		public void ToggleDash (bool setTo){
			if (setTo) {
				Dash.canDash = true;
			} else {
				Dash.canDash = false;
			}
		}
		public void ToggleSlash (bool setTo){
			slashRelic = setTo;
		}

		public void AbilityManager(){
			//temp manipulation of the toggle functions for prototyping
			ToggleJump(jumpRelic);
			ToggleWallJump (wallJumpRelic);
			ToggleDash (dashRelic);

		}

	}
}