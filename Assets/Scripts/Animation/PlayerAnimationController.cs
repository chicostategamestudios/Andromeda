using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;
using Assets.Scripts.Components;

public class PlayerAnimationController : MonoBehaviour {

    Animator animController;
    AnimatorControllerParameter[] animParams;
    CharController charController;
    CharacterController charCont;

    // Use this for initialization
    void Awake()
    {
        animController = GetComponent<Animator>();
        animParams = animController.parameters;
        charController = gameObject.AddComponent<Assets.Scripts.Character.CharController>();
        charCont = this.gameObject.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void FixedUpdate () {
        DetermineAnimatorParams();
	}

    void DetermineAnimatorParams()
    {

        if (charController.dashing == true)
        {
        animController.SetBool("Dash", true);
        SetAllBoolParams("Dash", false);
        }

        else if (charController.jumping == true)
        {
            if (charCont.isGrounded != true)
            {
                animController.SetBool("Jump", true);
                SetAllBoolParams("Jump", false);
            }
        }
        else
        {
            animController.SetBool("Idle", true);
            SetAllBoolParams("Idle", false);
        }

        if (charController.lastDir < 0)
        {
            animController.SetBool("Right", true);
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
