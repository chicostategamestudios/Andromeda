using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;
//Last edited by James | Modified on Feb 10, 2017
//Changed the script to keep the glow active until the player touches another checkpoint.

public class CheckpointGlow : CustomComponentBase
{
	public GameObject glow;
    public GameObject[] glows; //Game Object Array that will keep track of the glows.



    void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
            glows = GameObject.FindGameObjectsWithTag("Glow"); //finds all of the glows in the level and stores them in an array.
            foreach (GameObject glowy in glows)                 //go through each glow and turns them off.
            {
                glowy.SetActive(false);
            }
            glow.SetActive(true);                               //after turning all of them off... turn the one the player has touched back on.
		}
    }


    //Below is legacy code.
	/*void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
            //activated = false;
			glow.SetActive(false);
		}
	}*/
}
