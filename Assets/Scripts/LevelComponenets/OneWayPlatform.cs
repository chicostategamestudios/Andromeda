using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

//This platform will move one direction and not stop unless the player dies.
//we have options to make this platform activated by vines or player jumping on it
//as well as the option to make the platform float or snap to the ground
public class OneWayPlatform : LevelElement {

    public bool hasVines; // If we want this to be locked, and activated by vines, set this to be true
    public bool snapToGround; //this will make the platform look for ground below it, and snap to it as it moves

    public float speed; //how fast we should move

    public Transform myPlatform; //this is the platform we're moving
    public float platformOffset = 1f; //this is how much that platform should be offset from the ground if we're snapping to the ground.
    private Vector3 startPos; //we hold our start position for when we reset
	// Use this for initialization
	void Start () {
        LevelReset.AddToLevelElements(this); //add ourselves to reset for when the player dies
        startPos = myPlatform.transform.position;//save our start position
	}

    public override void Activate() //This will be called by vines. if we don't have any vines 
    {        //attached Activate is instead called when the player jumps on platform
        StopAllCoroutines(); //make sure we don't activate the the platform multiple times somehow
        StartCoroutine("Move"); //start moving
    }

    public override void Reset() //this will be called by LevelReset
    {
        StopAllCoroutines(); //stop moving
        myPlatform.transform.position = startPos; //return us to our start position
    }

    void OnTriggerEnter(Collider col) //when the player lands on the trigger
    {
        if (!hasVines) //if we're not activating with vines
        {
            if(col.gameObject.GetComponent<CharController>()!= null) //and it's the player that landed on us
            {
                Activate(); //start moving
            }
        }

    }

    IEnumerator Move() //this is how we move
    {
        while (true) //infinite LOOP
        {
            yield return new WaitForFixedUpdate(); //wait for fixed update
            myPlatform.transform.Translate(Vector3.right * speed); //move
            if (snapToGround) //if we snap to the ground
            {
                RaycastHit hit; 
                if (Physics.Raycast(myPlatform.transform.position, -myPlatform.transform.up, out hit, 10f))
                { //look for the ground
                    Vector3 newVec = new Vector3(myPlatform.transform.position.x, hit.point.y +platformOffset, myPlatform.transform.position.z); //get our new ypos
                    myPlatform.transform.position = newVec; //snap us there.
                }

            }
        }
    }

}
