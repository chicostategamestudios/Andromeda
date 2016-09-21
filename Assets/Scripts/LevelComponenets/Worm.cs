using UnityEngine;
using System.Collections;

public class Worm : MonoBehaviour {
    /*
    attached to the head of the worm
    head moves while the rest of the body follows.
    Should be done through modeling but its possible to setup via code.
    */

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	/*
    move forward
        speed is slow when in ground
        faster when moving towards the player?
    if player is in range then start rotating towards them
        rotation set to maybe 1 degree per update
        rotates while moving forward
        other limbs should follow the head
    when worm is above ground cut all controlled movement
        launches and falls in a parabola free fall




    */
	}
}
