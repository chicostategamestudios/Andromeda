using UnityEngine;
using System.Collections;

public class BeamDragonStatue : MonoBehaviour {
    [Tooltip("Override for playtesting (or other reasons). Make this true if you want to activate it before the relic is grabbed")]
    public bool isActive;
    [Tooltip("number of seconds in between beam firing")]
    public float waitTime;
    [Tooltip("duration of the beam when it's firing (in seconds)")]
    public float durationOfBeam;
    [Tooltip("intial delay of activation after artifact has been collected (in seconds)")]
    public float initialDelay;

    bool activated = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((isActive || DragonHeadRelic.isTempleActive) && !activated)
        {
            activated = true;
            InvokeRepeating("FireBeam", initialDelay, waitTime + durationOfBeam);
        }
    }

    void FireBeam()
    {
        //Spawn Beam
        //Has beam been activated for the user specified ammount of time?
            //Despawn beam
        
    }
}
