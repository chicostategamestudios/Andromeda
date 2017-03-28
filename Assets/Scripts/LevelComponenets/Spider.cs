using UnityEngine;
using System.Collections;

public class Spider : LevelElement {
    //This is the spider. It goes up and down, and when it's vine is slashed it dies.

    [SerializeField]
    private float speed = 1f; //speed the spider moves at
    [SerializeField]
    private float moveTime; //how long it moves for
    [SerializeField]
    private float delay = 1f; //how long it waits at the top and bottom

    private Vector3 startPos = Vector3.zero; //start position for resetting

    
    public Renderer myRenderer; //my meshrender so we can hide ourselves when we die

    void Awake() 
    {
        startPos = this.transform.position; //get our start position so we can reset properly
        LevelReset.AddToLevelElements(this); //add ourselves to levelReset so we can reset when the player dies
        StartCoroutine("Move"); //start moving up and down

    }

    public override void Reset() //this is called by LevelReset 
    {
        StopAllCoroutines(); //stop moving
        if (myRenderer != null)
        { 
            myRenderer.enabled = true; //show ourselves
        }
        if (this.gameObject.GetComponent<Collider>() != null)
        {
            this.gameObject.GetComponent<Collider>().enabled = true; //re enable collision (for killing)
        }
        this.transform.position = startPos; //reset our position
        StartCoroutine("Move"); //start moving again

    }

    public override void Activate() //this is called by the vine, when it is slashed
    {
        StopAllCoroutines(); //stop moving
        //play animation?
        if(myRenderer != null)
        {
            myRenderer.enabled = false; //hide ourselves
        }
        if(this.gameObject.GetComponent<Collider>() != null)
        {
            this.gameObject.GetComponent<Collider>().enabled = false; //disable our renderer
        }
        //If we want to have any kind of death animation, this is where we should put it. I would advise having
        //a seperate object that plays and animation, and we just call it from here on death

    }

    IEnumerator Move() //this moves the spider up and down
    {
        float curMove = 0;
        bool move = true;
        bool moveUp = true;

        while (true) //infinite loop
        {
            if (move) //if we're moving (not in a wait delay)
            {
                if (curMove < moveTime) //if we have not been moving for too long
                {

                    if (moveUp) //if we're moving uo
                    {
                        this.transform.Translate(Vector3.up * speed); //move up
                    }
                    else
                    {
                        this.transform.Translate(Vector3.up * -speed); //else move down

                    }
                    yield return new WaitForFixedUpdate(); //move every fixed update
                    curMove += Time.fixedDeltaTime; //add our curmove with fixedupdate, which is reset after our wait delay
                }
                else //if we've been moving for more time than we wanted to
                {
                    moveUp = !moveUp; //move the opposite direction next
                    move = false; //delay before moving again
                    curMove = 0; //reset our move time
                }
            }
            else
            {
                yield return new WaitForSeconds(delay); //wait time before moving again
                move = true; //move 
            }
        }
    }





}
