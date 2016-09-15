using UnityEngine;
using System.Collections;

public class DragonHeadRelic : MonoBehaviour {
     
    public static bool isTempleActive; //Global Varible to tell the rest of the game that the temple is active. If you need to call it in anouther script, use 'DragonHeadRelic.isTempleActive
    [Tooltip("Use this varible to make the temple 'Activate' the rest of the level. If you need to call it in anouther script, use 'DragonHeadRelic.isTempleActive")]
    public bool isTempleActive_; // used to be able to manipulate the global varible while playtesting game (local varible)
    SphereCollider myTrigger;
    public GameObject relic;

	// Use this for initialization
	void Start () {
        isTempleActive = false;
        isTempleActive_ = false;
        myTrigger = this.gameObject.GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
    // makes the global variable true if the local varible is set as true and vice versa
	void Update () {
        isTempleActive = isTempleActive_;
	}

    // if the player collides with the relic, then the temple activates
    // also destroys the relic and the trigger
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == ("Player")) {
            isTempleActive_ = true;
            Destroy(relic);
            Destroy(myTrigger);
        }
    }
}
