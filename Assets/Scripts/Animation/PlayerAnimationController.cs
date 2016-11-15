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
    }

    // Update is called once per frame
    void FixedUpdate () {
        DetermineAnimatorParams();
	}

    void DetermineAnimatorParams()
    {
        animController.SetBool("Moving", playerMovement.moving);
        Debug.DrawRay(transform.position + (new Vector3(0, 1, 0)), -transform.up * 1f);
        if (Physics.Raycast(transform.position + (new Vector3( 0,1,0)), -transform.up, 1f))
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

    //check Dash script for the enum. when enum is in startingLock



}
