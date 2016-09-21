using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Lava_Stream : MonoBehaviour {

    private List<GameObject> LavaChildren = new List<GameObject>();
    public bool LavaOn;
    public float timer = 1f;


    // Use this for initialization
    void Start()
    {

        //fills list with the children
        foreach (Transform MyChild in transform) { LavaChildren.Add(MyChild.gameObject); }
        LavaOn = true;


    }

    // Update is called once per frame
    void Update()
    {


        if (GetComponent<ParticleSystem>().isPlaying)
        {
            LavaOn = true;
        }
        else
        {
            LavaOn = false;
            StartCoroutine(TurnLavaOn());
        }

        //If lavaon is true then the particle system is on and the children collider is on
        if (LavaOn == true)
        {
            foreach (GameObject LavaChild in LavaChildren) { LavaChild.GetComponent<Collider>().enabled = true; }
        }
        //If lavaon is false then the particle system is off and the children collider is off
        if (LavaOn == false)
        {
            foreach (GameObject LavaChild in LavaChildren) { LavaChild.GetComponent<Collider>().enabled = false; }
        }
    }

    IEnumerator TurnLavaOn() { yield return new WaitForSeconds(timer); if (LavaOn == false) { LavaOn = true; GetComponent<ParticleSystem>().Play(); } }
}