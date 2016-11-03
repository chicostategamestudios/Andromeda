using System;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Character
{

	class CharController : Components.ControllerBase
	{
		private static CharController _instance;
		public static CharController Instance{
			get{
				_instance = _instance ?? GameObject.FindGameObjectWithTag("Player").GetComponent<CharController> ();
				if (_instance == null) {
					Debug.LogWarning ("No CharController in scene but an object is attempted to access it");
				}
				return _instance;
			}
		}
		Components.PlayerMovement _movement;
		Components.RelicManager _relics;
		Components.Slash _slash;
		Components.Health _health;
		Components.Death _death;

		float playerDirection;
		public float lastDir =1f;
        public bool dashing = false;
        public bool jumping = false;
		void Awake(){
			_movement = gameObject.AddComponent<Components.PlayerMovement> ();
			_relics = gameObject.AddComponent<Components.RelicManager> ();
			_slash = gameObject.AddComponent<Components.Slash> ();
			_health = gameObject.AddComponent<Components.Health> ();
			_death = gameObject.AddComponent<Components.Death> ();
			_health.TotalStuff ();
		}

		void Update(){
			if (_death.dying) {
				return;
			}

			GetInput ();
			_movement.MovePlayer (playerDirection);
			_movement.WallGrab ();
			//_movement.DashHandler (playerDirection);

		}
		void FixedUpdate(){
			_relics.AbilityManager ();
		}

			void GetInput() //gets input to be used in the manageInput function, subject to be removed once a input manager is implemeted
			{

				float horzInput = Input.GetAxis ("Horizontal");


			if (Mathf.Abs (horzInput) > 0.15f) {
				if (horzInput > 0) {
					playerDirection = 1f;
                    if (lastDir != 1f)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y * -1, 0));
                    }
                    lastDir = 1f;

                }
                if (horzInput < 0) {
					playerDirection = -1f;
                    if (lastDir != -1f)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y * -1, 0));
                    }
                    lastDir = -1f;

                }
            } else {
				playerDirection = 0f;
			}


				//if ((Input.GetKeyDown(KeyCode.Space))
				if(Input.GetButtonDown("Fire1"))
				{
				    _movement.JumpPlayer(playerDirection);
                    jumping = true;
				}
                else
                {
                    jumping = false;
                }
			
				//if (Input.GetKeyDown (KeyCode.LeftShift)) {
			if(Input.GetButtonDown("Fire2")){
                dashing = true;
				_movement.DashPlayer();
				}
            else
            {
                dashing = false;
            }

            if (Input.GetButtonDown ("Fire3")) {
				_slash.SlashAttack (lastDir);
			}
		




			}



}
}