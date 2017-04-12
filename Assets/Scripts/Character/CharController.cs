//Last editted by James | Modified on Feb 9, 2017
using System;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Character
{

    class CharController : Components.ControllerBase
    {
        [Tooltip("Timer value must be more than 0. This is the timer that prevents the player from spamming jump")]
        public float jump_spam_timer = 0.0f; //This is the amount of time before a player can jump again.

        [Tooltip("Timer value must be more than 0. This is the timer that prevents the player from spamming dash")]
        public float dash_spam_timer = 0.5f; //This is the amount of time before a player can dash again.

        private bool can_jump_again = true; //This will be set to true whenever the player can dash again after the timer runs off after a jump.

        private bool can_dash_again = true; //This will be set to true whenever the player can dash again after the timer runs off after a dash.

        [Tooltip("Timer value must be more than 0. This is the timer that prevents the player from holding the jump button to float really high")]
        public float hold_jump_max_time = 0.5f; //This is the max amount of time a player can hold the jump button to float a bit higher.

        [Tooltip("Timer value must be 0. This is the timer that keeps track of the player's float time.")]
        public float hold_jump_time = 0.0f; //This is to keep track of how much time the player has held the jump button to make sure it hasn't exceeded the max time.

        private static CharController _instance;
        public static CharController Instance
        {
            get
            {
                _instance = _instance ?? GameObject.FindObjectOfType<CharController>();
                if (_instance == null)
                {
                    Debug.LogWarning("No CharController in scene but an object is attempted to access it");
                }
                return _instance;
            }

            set
            {
                //	_instance = value;
            }
        }
        Components.PlayerMovement _movement;
        Components.RelicManager _relics;
        Components.Slash _slash;
        Components.Health _health;
        Components.Death _death;

        float playerDirection;
        public float lastDir = 1f;
        public bool jumping = false;
        public float horzInput;


        IEnumerator CanJumpAgain()  //coroutine starts by setting can_jump_again to false so they can't jump again. Then delays the jump by jump_spam_timer
        {                           //afterwards, it sets can_jump_again back to true so they can jump again.
            can_jump_again = false;
            yield return new WaitForSeconds(jump_spam_timer);
            can_jump_again = true;
        }

        IEnumerator CanDashAgain()  //coroutine starts by setting can_dash_again to false so they can't dash again. Then delays the dash by dash_spam_timer
        {                           //afterwards, it sets can_dash_again back to true so they can dash again.
            can_dash_again = false;
            yield return new WaitForSeconds(dash_spam_timer);
            can_dash_again = true;
        }



        public void endScene()
        {

            //	_instance = null;
            //	Instance = null;
        }

        public void Init()
        {
            _instance = this.gameObject.GetComponent<CharController>();
            return;
        }


        void Awake()
        {
            _movement = gameObject.AddComponent<Components.PlayerMovement>();
            _relics = gameObject.AddComponent<Components.RelicManager>();
            _slash = gameObject.AddComponent<Components.Slash>();
            _health = gameObject.AddComponent<Components.Health>();
            _death = gameObject.AddComponent<Components.Death>();
            _health.TotalStuff();
        }

        void Update()
        {
            if (_death.dying)
            {
                return;
            }

            //_movement.DashHandler (playerDirection);
            GetInput();

        }
        void FixedUpdate()
        {
            if (_movement.grounded) //if the player is on the ground, this resets the hold_jump_time back to 0 so they can hold the jump button again.
            {
                hold_jump_time = 0.0f;
            }

            
            _movement.MovePlayer(playerDirection);
            _movement.WallGrab();
            _relics.AbilityManager();
        }

        void GetInput() //gets input to be used in the manageInput function, subject to be removed once a input manager is implemeted
        {

            horzInput = Input.GetAxis("Horizontal");


            if (Mathf.Abs(horzInput) > 0.15f)
            {
                if (horzInput > 0)
                {
                    playerDirection = 1f;
                    if (lastDir != 1f)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y * lastDir, 0));
                    }
                    lastDir = 1f;

                }
                if (horzInput < 0)
                {
                    playerDirection = -1f;
                    if (lastDir != -1f)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y * -lastDir, 0));
                    }
                    lastDir = -1f;
                }
            }
            else
            {
                playerDirection = 0f;
            }


            //if ((Input.GetKeyDown(KeyCode.Space))
            if (Input.GetButtonDown("Fire1") && can_jump_again == true) //added can_Jump_again to check if the player has finished their cooldown of jump.
            {
                _movement.JumpPlayer(playerDirection);
                jumping = true;
                StartCoroutine(CanJumpAgain());
            }
            else
            {

                jumping = false;
            }

            if (Input.GetButton("Fire1") && _movement.grounded == false) // This is to check if the player is holding the jump button and is in the air.
            {                                                           //If the player is holding the jump button, then they hover so long as the hold_jump_time is less than the max time.
                if (hold_jump_time < hold_jump_max_time)
                {
                    _movement.HoverPlayer();
                    hold_jump_time += Time.deltaTime;
                }
            }

            //if (Input.GetKeyDown (KeyCode.LeftShift)) {
            if (Input.GetButtonDown("Fire2") && can_dash_again == true && _relics.dashRelic == true) //added can_dash_again to check if the player isn't spamming dash... 
            {                                                                                               //also checks if the player has actually collected the dash relic
                StartCoroutine(CanDashAgain());
                _movement.DashPlayer();
            }

            if (Input.GetButtonDown("Fire3"))
            {
                _slash.SlashAttack(lastDir);
                Debug.Log("Fire3 pressed");
            }
        }



    }
}