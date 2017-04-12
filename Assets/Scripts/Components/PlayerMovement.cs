//Last editted by James | Modified on Mar 7, 2017

using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Components
{

    public class PlayerMovement : CustomComponentBase
    {

        Components.Jumping _jump;
        Components.Dash _dash;
        Components.WallGrab _wallGrab;



        CharacterController charCont;

        bool waitForJump;
        public static bool applyGravity = true;
        public static bool overrideInput = false;
        public float walled;
        public static float verticleSpeed;
        public static float normalSpeed = 15f;
        public static float boostedSpeed = 30f;
        public static float speed = 15f;
        public static float speedModifier = 1f;
        public float jumpHeight = 20.0f;    //originally 28.5f... changed this to balance the player's jumping potential
        public float hold_jump_force = 45.4f;
        public float dashJump;
        public static Vector2 moveVector;
        public static float playerDir;
        public float gravity = 80f;
        public bool grounded;
        public bool checkforwalls;
        public static bool checkforground = true;
        public float upRayLength = 0.0f;
        public float ceilingHitSpeed = -10f;
        public bool Dashed = false;

        [Tooltip("Time in seconds in which you wish the player to be stunned after taking damage. Can't jump, can't dash, can't move, can't slash.")]
        public float stunnedLength = 1f;
        public static int celingmask = 1 << 8;
        public Vector2 modificationVec = Vector2.zero;
        public static int movingGround = 1 << 9;
        public static int level_mask = 1 << 11; //adding this to check if the player hits the level layer as well
        //int ceilingAndGround = celingmask | movingGround;
        int ceilingAndGround = celingmask | movingGround | level_mask;
        public float maxVertSpeed = -40;
        public bool moving;
        public GameObject upRayPos;
        public bool jumping;
        public bool hitCeiling;
        public bool isStunned = false;
        // Use this for initialization
        void Awake()
        {
            _jump = gameObject.AddComponent<Jumping>();
            _dash = gameObject.AddComponent<Dash>();
            _wallGrab = gameObject.AddComponent<WallGrab>();
            upRayPos = transform.Find("UprayPos").gameObject;

            charCont = this.gameObject.GetComponent<CharacterController>();
        }

        // Update is called once per frame

        private void Update()   //this is to check for ceiling collisions
        {
            if ((charCont.collisionFlags & CollisionFlags.Above) != 0) //if the player has hit anything above them
            {
                verticleSpeed = ceilingHitSpeed; //set the vertical speed to negative so they "bounce" off of it.
            }
        }



        public void MovePlayer(float playerDirection)
        {
            /*i dont know what this does, it doesnt seem to do anything.
             * but im not confident enough to delete this
            if (Input.GetKey(KeyCode.X))
            {
                modificationVec.x = 22f;
            }
            */

            RaycastHit hit;
            //Debug.DrawRay (transform.position/*+new Vector3(0,2,0)*/, -Vector3.up, Color.yellow, 1);
            Ray downRay = new Ray(transform.position + new Vector3(0, 1, 0), -Vector3.up);
            if (Physics.Raycast(downRay, out hit, 1f))
            {
                if (hit.transform.gameObject.layer == 9)
                {
                    this.gameObject.transform.SetParent(hit.transform);

                }
                else
                {
                    this.gameObject.transform.SetParent(null);
                }
            }
            else
            {
                //this.gameObject.transform.SetParent (null);                   //This line was making the player not properly assigned to the platform movement
            }

            if (checkforground)
            {
                grounded = charCont.isGrounded;
            }
            else
            {
                grounded = false;
            }
            playerDir = playerDirection;
            _dash.ManageDashing(grounded, playerDir);

            if (grounded && !waitForJump)
            {
                _jump.resetJumps();
                Dashed = false;   //resetting air dash when the player has landed.

            }

            if (walled != 0)
            {

                _dash.OverrideDash();
            }

            if (!grounded && !checkforwalls)
            {
                //checkforwalls = true;
            }
            if (grounded)
            {
                _wallGrab.notWall = 0f;
                _dash.OverrideDash();
                _dash.ResetDashing();
            }

            if (applyGravity)
            {
                if (grounded)
                {
                    verticleSpeed = -0.1f;
                    checkforwalls = false;
                }
                else if (!grounded)
                {
                    if (verticleSpeed > maxVertSpeed)
                    {
                        print("Max vet speed: " + maxVertSpeed);
                        verticleSpeed -= gravity * Time.deltaTime;
                    }
                }
                if (!waitForJump)
                {
                    checkforwalls = true;

                }
            }

            Upray();
            if (!overrideInput)
            {

                if (isStunned)
                {
                    moveVector = new Vector2(0, verticleSpeed);
                    StartCoroutine("Stun");
                }
                else
                {
                    moveVector = new Vector2(playerDirection * speed * speedModifier, verticleSpeed); //calculate movement in the x and y 
                }

                if (moveVector.x != 0)
                {
                    moving = true;
                }
                else
                {
                    moving = false;
                }

            }
            moveVector += modificationVec;

            charCont.Move(moveVector * Time.deltaTime); //apply movement in the x and y

            if (transform.position.z != 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            }

        }


        public void JumpPlayer(float Direction)
        {
            if (isStunned)
            {
                return;
            }
            if (_wallGrab.notWall != 0)
            {
                return;
            }

            if ((walled != 0) && !grounded)
            {
                _jump.WallJump(Direction, walled);

                return;
            }
            else
            {
                //	if (grounded) {
                StartCoroutine("groundJump");

                return;
                //	}
                //	_jump.BasicJump (jumpHeight);
                //	print ("goddammit");
                //	return;
            }

        }

        public void HoverPlayer()  //This will make the player float for a bit. It is called when the player holds the jump button while in the air.
        {
            verticleSpeed += hold_jump_force * Time.deltaTime;
        }

        public void WallGrab()
        {
            if (checkforwalls && !grounded)
            {

                walled = _wallGrab.WallSlide(playerDir);
                //Debug.Log (walled);
            }

        }

        public void DashPlayer()
        {
            if (walled != 0 || isStunned)
            {
                return;
            }
            if (grounded)
            {  //took out this commented area to check if the player is on the ground then go ahead and do the ground dash.
                StartCoroutine("GroundDash");
                _dash.StartDashing(playerDir);
                return;
            }                //took out this commented area to close the "if grounded" statement.

            if (!grounded && !Dashed)  // this is checking if the player is in the air, and also if they have already dashed before... if they haven't dashed while in the air already then they can dash.
            {
                StartCoroutine("AirDash");
                _dash.StartDashing(playerDir);
                return;
            }
            //_jump.BasicJump (dashJump);
            //	_dash.StartDashing (playerDir);
        }


        public void Upray() //legacy code!!
        {

            //Ray UpRay = new Ray (transform.position , transform.up);
            if (Physics.Raycast(upRayPos.transform.position, transform.up, upRayLength, ceilingAndGround))
            {
                //verticleSpeed = ceilingHitSpeed;
                //hitCeiling = true;
                //StartCoroutine("notHittingCeiling");
            }
        }


        IEnumerator GroundDash()
        {
            applyGravity = false;
            waitForJump = true;
            checkforground = false;
            //_jump.BasicJump (dashJump);

            yield return new WaitForSeconds(0.1f);
            checkforwalls = true;
            checkforground = true;
            waitForJump = false;
            applyGravity = true;
        }

        IEnumerator AirDash() //A second version of GroundDash, but for aerial dashing
        {
            applyGravity = false;
            waitForJump = true;
            checkforground = false;
            Dashed = true; //once dashed is set to true, the player can't dash in the air again.
            yield return new WaitForSeconds(0.1f);
            checkforwalls = true;
            checkforground = true;
            waitForJump = false;
            applyGravity = true;
        }




        IEnumerator groundJump()
        {
            checkforwalls = false;
            waitForJump = true;
            checkforground = false;
            //applyGravity = false;
            _jump.BasicJump(jumpHeight);

            yield return new WaitForSeconds(0.05f);

            checkforground = true;
            //applyGravity = true;
            waitForJump = false;
            //	yield return new WaitForSeconds (0.1f);
            checkforwalls = true;




        }

        IEnumerator Stun()
        {
            yield return new WaitForSeconds(stunnedLength);
            isStunned = false;
        }

        IEnumerator notHittingCeiling()
        {
            yield return new WaitForSeconds(.05f);
            hitCeiling = false;
        }
        //Life is good!
    }
}