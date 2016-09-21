using UnityEngine;
using System.Collections;

public class DrippingDragonStatue : MonoBehaviour {
    [Tooltip("Override for playtesting (or other reasons). Make this true if you want to activate it before the relic is grabbed")]
    public bool isActive;
    [Tooltip("duration of bursts of lava (in seconds)")]
    public float durationOfDrip;

    public Lava_Stream lavaStream;

    bool activated = false;
	// Use this for initialization
	void Start () {
        
	}

    void DrippingLava()
    {
        lavaStream.GetComponent<Lava_Stream>().enabled = true;
        lavaStream.timer = durationOfDrip;
    }

    // Update is called once per frame
    void Update () {
        if ((isActive || DragonHeadRelic.isTempleActive) && !activated) {
            isActive = true;
            activated = true;
            DrippingLava();
        }
	}

    
}
