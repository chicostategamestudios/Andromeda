using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;


//this is our hella basic spring. it makes the player go up
public class Spring : MonoBehaviour {

    public float springForce; //force we go up with

	void OnTriggerEnter(Collider col) //if we're hit
    {
        if (col.gameObject.GetComponent<PlayerMovement>() != null) //by the player
        {
            PlayerMovement.verticleSpeed = springForce; //take them up
        }

    }
    //On a note with this. It does not work reliably if the player is already on the ground. This is because the player
    //is "grounded" which sets their verticalspeed back to grounded speed. If we need to rework this, we should either make
    //a special function in the character controller to "jump" the player from here, or we should snap the player to the spring
    //and then boost them up. 

}
