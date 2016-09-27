using UnityEngine;
using System.Collections;

public class DrippingDragonStatue : MonoBehaviour {
    [Tooltip("Override for playtesting (or other reasons). Make this true if you want to activate it before the relic is grabbed. This only last for a second and has a 1 second timer inbetween")]
    public bool isActive;
    

    public Lava_Stream lavaStream;

    bool activated = false;
	// Use this for initialization
	void Start () {
        
	}

    void DrippingLava()
    {
        lavaStream.GetComponent<Lava_Stream>().enabled = true;
        lavaStream.timer = 1;
    }

    // Update is called once per frame
    void Update () {
        if ((isActive || DragonHeadRelic.isTempleActive) && !activated) {
            isActive = true;
            activated = true;
            lavaStream.gameObject.SetActive(true);
            DrippingLava();
        }
	}

    
}
