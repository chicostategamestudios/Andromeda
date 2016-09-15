﻿using UnityEngine;
using System.Collections;

public class FireballDragonStatue : MonoBehaviour {
    [Tooltip("Override for playtesting (or other reasons). Make this true if you want to activate it before the relic is grabbed")]
    public bool isActive;
    [Tooltip("number of seconds in between fireballs")]
    public float waitTime;
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
            InvokeRepeating("SpawnFireball", initialDelay, waitTime);
        }
    }

    void SpawnFireball()
    {
        //Instantainate a new fireball
    }
}