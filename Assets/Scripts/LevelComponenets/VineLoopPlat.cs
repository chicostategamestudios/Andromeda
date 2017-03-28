using UnityEngine;
using System.Collections;

public class VineLoopPlat : LevelElement {

    //This is a platform that will move around once it has been activated by something
    
    Vector3 startPos; //save our start position for resetting

    public Vector2 moveSpeed; //our move speed, we can move in the x, y, or both
    public float moveDur; //how long we move for before we turn around
    public Transform Platform; //the platform we're moving

	void Start () {
        startPos = Platform.transform.position; //save our start position
        LevelReset.AddToLevelElements(this); //add ourselves to levelReset so we can be reset
	}

    public override void Activate()
    {
        StartCoroutine("Move"); //this is currently called when the vine is cut
    }

    public override void Reset()
    {
        StopAllCoroutines(); //stop moving
        Platform.transform.position = startPos; //reset our position
    }


    IEnumerator Move() //start moving
    {
        float curMove = 0; 
        bool moveForward = true;

        while (true) //loop forever
        {
            yield return new WaitForFixedUpdate(); //every fixed udpate
            if (moveForward)
            {
                curMove += Time.fixedDeltaTime; //add our time
                Platform.transform.Translate(new Vector3(moveSpeed.x, moveSpeed.y, 0)); //move
            }
            else
            {
                curMove += Time.fixedDeltaTime;
                Platform.transform.Translate(new Vector3(-moveSpeed.x, -moveSpeed.y, 0));
            }
            if(curMove >= moveDur) //if we've been moving for too long
            {
                moveForward = !moveForward; //change directions
                curMove = 0; //reset our current move
            }


        }

    }

}
