using UnityEngine;
using System.Collections;

public class Vine : CanDestroy {

    //This is the vine object. Right now we're using it to trigger actions on other objects.
    //we do this by calling Activate on different LevelElements, which is then overloaded into different
    //LevelElements

    public LineRenderer myRenderer; //this is our line visually.
    public LevelElement myLevelElement; //This is the object we're activating

    public BoxCollider myCollider; //this is our collider. Since line renderers can't have collision
                                    //we place this object between the start point and end point of the line
                                    //renderer, and scale our box collider in the y, that way we can 
                                    //have "fake" collision on our line renderer

    public Transform StartPos; //Start position of the line vine
    public Transform EndPos; //end position of the vine



    public void Awake()
    {
        LevelReset.AddToLevelElements(this); //add our selves so we can be reset
        myRenderer.useWorldSpace = true; //fix our renderer in case people fucked it up
        myRenderer.SetVertexCount(2); 
        myRenderer.SetPosition(0, StartPos.position); //we just set our start position in awake
                                                      //if we need to update it for moving vines or something
                                                      //changes will need to be made
    }

    void FixedUpdate()
    {
        myRenderer.SetPosition(1, EndPos.position); //update our bottom vine position
        this.transform.position = (StartPos.position + EndPos.position) / 2f; //set our position of this to the center of the start and end point
        myCollider.size = new Vector3(myCollider.size.x, (Mathf.Abs(StartPos.position.y - EndPos.position.y)), myCollider.size.z); //update our box collider to be correct with the vine
    }

    public override void DestroyMe() //this is currently inheiting from destructable object.
    {                                //This will be called when the player calls slash on it
        if (myLevelElement != null) //if we have a level element (like a spider or platform
        {
            myLevelElement.Activate(); //we want to activate it
        }
        if(myRenderer != null) //if we set our line renderer properly
        {
            myRenderer.enabled = false; //turn it off
        }
        if(this.gameObject.GetComponent<Collider>() != null) //if we have collision
        {
            this.gameObject.GetComponent<Collider>().enabled = false; //turn it off
        }

    }

    public override void Reset() //this will reset us
    {
        if (myRenderer != null)
        {
            myRenderer.enabled = true; //turn our visuals on
        }
        if (this.gameObject.GetComponent<Collider>() != null)
        {
            this.gameObject.GetComponent<Collider>().enabled = true; //and collision
        }
    }



}
