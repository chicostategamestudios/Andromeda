using UnityEngine;
using System.Collections;

public class DrippingDragonStatue : MonoBehaviour {
    [Tooltip("Override for playtesting (or other reasons). Make this true if you want to activate it before the relic is grabbed")]
    public bool isActive;
    [Tooltip("number of seconds in between bursts")]
    public float waitTime;
    [Tooltip("duration of bursts of lava (in seconds)")]
    public float durationOfDrip;
    [Tooltip("intial delay of activation after artifact has been collected (in seconds)")]
    public float initialDelay;

    bool activated = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if((isActive || DragonHeadRelic.isTempleActive) && !activated) {
            activated = true;
            InvokeRepeating("DrippingLava", initialDelay, waitTime + durationOfDrip);
        }
	}

    void DrippingLava() {
        //Spawn a lava burst
        //is lava touching the ground?
            //if it is, despawn the lava
    }
}
