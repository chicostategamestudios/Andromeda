using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;
using Assets.Scripts.Components;

public class PlayerAnimationController : MonoBehaviour {

    Animator animController;
    AnimatorControllerParameter[] animParams;
    CharController charController;
    Dash dash;
    PlayerMovement playerMovement;
    Jumping jumping;
    CharacterController _characterController;
    Animator anim;
    WallGrab _wallGrab;
    Slash slash;
    public LayerMask groundLayer;
    private bool canIdle = true;
    private float timeLeft = 8f;
    private bool timerStarted;
    // Use this for initialization
    void Awake()
    {
        animController = GetComponent<Animator>();
        animParams = animController.parameters;
        charController = gameObject.AddComponent<Assets.Scripts.Character.CharController>();
        dash = this.gameObject.GetComponent<Dash>();
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        jumping = this.gameObject.GetComponent<Jumping>();
        _characterController = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
        _wallGrab = gameObject.GetComponent<WallGrab>();
        slash = this.gameObject.GetComponent<Slash>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        DetermineAnimatorParams();
	}

    void DetermineAnimatorParams()
    {
        if (charController.horzInput >= 0.15f || charController.horzInput <= -0.15f)
        {
            animController.SetBool("Moving", true);
            canIdle = false;
            timerStarted = false;
            timeLeft = 3f;
        }
        else
        {
            if (canIdle == true)
            {
                animController.SetBool("Moving", false);
                timeLeft -= Time.deltaTime;
                if (timeLeft < 0)
                {
                    Debug.Log("ya");
                    timerStarted = false;
                    timeLeft = 8f;
                }


            }
            IdleWait();
        }
        if (Physics.Raycast(transform.position + (new Vector3( 0,1,0)), -transform.up, 2.5f, groundLayer))
        {
                animController.SetBool("Grounded", true);
        }
        else
        {
            animController.SetBool("Grounded", false);
        }
        animController.SetBool("Dashing", dash.Dashing);
        if (dash.Dashing)
        {
            animController.SetBool("HasDashed", true);
        }
        else
        {
            animController.SetBool("HasDashed", false);
        }

        if(slash.slashing)
        {
            animController.SetBool("Slash", true);
        }
        else if (!slash.slashing)
        {
            animController.SetBool("Slash", false);
        }

        animController.SetFloat("JumpStage", jumping.jumpStage);
        animController.SetBool("HitCeiling", playerMovement.hitCeiling);

        if (charController.lastDir > 0)
        {
            animController.SetBool("Mirror", true);
        }
        else
        {
            animController.SetBool("Mirror", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Left Jump"))
        {
            animController.SetBool("SingleJump", true);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Left Landing"))
        {
            animController.SetBool("SingleJump", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Left Double Jump"))
        {
            animController.SetBool("DoubleJump", true);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Left Landing"))
        {
            animController.SetBool("DoubleJump", false);
        }
        if (playerMovement.walled != 0)
        {
            animController.SetBool("Walled", true);
            if (Physics.Raycast(transform.position + (new Vector3(0, 1, 0)), -transform.up, .4f, groundLayer))
            {
                animController.SetBool("Grounded", true);
            }
        }
        else
        {
            animController.SetBool("Walled", false);
        }
        if (_wallGrab.wallDir != 0)
        {
            animController.SetBool("CloseWall", true);
        }
        else
        {
            animController.SetBool("CloseWall", false);
        }
        animController.SetFloat("WallJumps", jumping.wallJumps);
    }



    void SetAllBoolParams (string exempt, bool flag)
    {
        foreach (AnimatorControllerParameter param in animParams)
        {
            if( param.type == AnimatorControllerParameterType.Bool && param.name != exempt)
            {
                animController.SetBool(param.name, flag);
                
            }
        }
    }

    void IdleWait()
    {
        canIdle = true;
    }

    void IdleTime()
    {


    }
}
