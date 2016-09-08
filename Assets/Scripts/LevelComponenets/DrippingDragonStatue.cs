using UnityEngine;
using System.Collections;

public class DrippingDragonStatue : MonoBehaviour {
    public bool isActive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        isActive = DragonHeadRelic.isTempleActive;
        if (isActive) {
            //Repeatidly call a function for a user defined time.
        }
	}
}
