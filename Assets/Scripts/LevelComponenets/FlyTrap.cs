using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;
using Assets.Scripts.Components;

//This is the flytrap. This will start a timer when the player steps on it, and if the player is standing on the platform
//at the end of the timer, it will kill them. if the player steps off, the timer is reset
public class FlyTrap : LevelElement { //T

    public float killDelay = 1f;//Time before the player is killed

    //these variables are so you can have the trigger and kill zone be different zones.
    public float killRadius; //Size of kill zone
    public Transform killCenter; //center of kill zone

  

    void Start()
    {
        LevelReset.AddToLevelElements(this); //add to level reset so we can reset the level.
                                            //if we ever come back to this, making levelReset have 
                                            //a getter would be good
    }

    public override void Reset()
    {
        StopAllCoroutines(); //on reset all we need to do is stop the timer
    }

    void OnDrawGizmosSelected() //this is set up so we can easily visualize the kill center in editor, it doesn't actually "do" anything
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(killCenter.position, killRadius);
    }

    void OnTriggerEnter(Collider col) //if we get it
    {
      
        if(col.gameObject.GetComponent<CharController>() != null) //by the player
        {
            StartCoroutine("Kill"); //start timer
        }
    }

    void OnTriggerExit(Collider col) //if we are left
    {
        if (col.gameObject.GetComponent<CharController>() != null) //by the player
        {
            StopAllCoroutines(); //stop the timer
        }
    }

    IEnumerator Kill() //kill timer. Right now it just waits until a certain amount of time and then snaps.
    {               //you could instead do a counter that counts up a value for easy animations. idk man
     
        yield return new WaitForSeconds(killDelay); //wait for kill delay
        Collider[] hitCol = Physics.OverlapSphere(killCenter.position, killRadius); //get all colliders in the kill radius
        for (int col = 0; col < hitCol.Length; col++) //check each
        {
            
            if (hitCol[col].transform.gameObject.GetComponent<Death>() != null) //if it's the player
            {
               
                hitCol[col].transform.gameObject.GetComponent<Death>().PlayerDeath(); //kill them
            }
        }


    }




}
