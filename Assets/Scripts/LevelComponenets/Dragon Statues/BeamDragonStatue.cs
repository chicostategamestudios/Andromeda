using UnityEngine;
using System.Collections;
using FMODUnity;

public class BeamDragonStatue : MonoBehaviour {
    [Tooltip("Override for playtesting (or other reasons). Make this true if you want to activate the statue before the relic is grabbed")]
    public bool isActive;
    [Tooltip("number of seconds in between beam firing")]
    public float waitTime;
    [Tooltip("intial delay of activation after artifact has been collected (in seconds)")]
    public float initialDelay;
    [Tooltip("duration of the beam when it's firing (in seconds)")]
    public float beamDuration;
    [Tooltip("Time in between the beam firing and the explosion")]
    public float dealyOfExplosion;
    [Tooltip("Duration of the explosion")]
    public float explosionDuration;
    bool activated = false;
    [Tooltip("Here's a tip, leave this guy alone. He's introverted.")]
    public GameObject beam;
    public StudioEventEmitter target;

    // Use this for initialization
    void Start()
    {
        //Initializing the public varibles of the laser beam based on user input
        beam.GetComponent<FlamePillar>().beamDuration = beamDuration;
        beam.GetComponent<FlamePillar>().dealyOfExplosion = dealyOfExplosion;
        beam.GetComponent<FlamePillar>().explosionDuration = explosionDuration;
    }

    // Update is called once per frame
    void Update()
    {
        //If the temple has been activated (or manually activated) then start firing the beam
        if ((isActive || DragonHeadRelic.isTempleActive) && !activated)
        {
            activated = true;
            isActive = true;
            InvokeRepeating("FireBeam", initialDelay, waitTime + beamDuration + dealyOfExplosion + explosionDuration);
        }
    }

    void FireBeam()
    {
        target.Play();
        //The beam gets activated (It deactivates itself)
        Debug.Log("I got here");
        beam.GetComponent<FlamePillar>().isActive = true;
    }
}
