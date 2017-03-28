using UnityEngine;
using System.Collections;

public class LevelElement : MonoBehaviour {


    //This is the parent object LevelElement. We should inheint all level objects from this object

	public virtual void Reset() //this will be called on player death, to rest objects to start stuff
    {                   
        //we don't do anything here. each object should overload individually to 
        //how they need to reset
    }

    

    public virtual void Activate()
    {
        //this can be overloaded to "start" something, ie something is triggered by another object
    }


}
