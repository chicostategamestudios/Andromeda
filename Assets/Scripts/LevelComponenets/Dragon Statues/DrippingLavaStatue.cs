using UnityEngine;
using System.Collections;

public class DrippingLavaStatue : MonoBehaviour {
    [Tooltip("Override for playtesting (or other reasons). Make this true if you want to activate it before the relic is grabbed")]
    public bool isActive;
    [Tooltip("number of seconds in between drips")]
    public float waitTime;
    [Tooltip("intial delay of activation after artifact has been collected (in seconds)")]
    public float initialDelay;
    [Tooltip("How fast does the drip go? 10 is slow, 20 is fast. Anything lower is super slow anything higher is super fast. 17 is a good middle.")]
    public float gravityInfluence;
    Quaternion spawnQ = Quaternion.Euler(0, 0, 0);

    bool activated = false;
    public GameObject firedrip_;

    // Use this for initialization
    void Start () {
    
}
	
	// Update is called once per frame
	void Update () {
        if ((isActive || DragonHeadRelic.isTempleActive) && !activated)
        {
            isActive = true;
            activated = true;
            InvokeRepeating("SpawnLava", initialDelay, waitTime);
        }
    }

    void SpawnLava()
    {
        //Start Instantiating drips with the user defined values
        GameObject firedrip = (GameObject)Instantiate(firedrip_, firedrip_.GetComponent<Transform>().position, spawnQ);
        firedrip.GetComponent<FireDrip>().gravityRate = gravityInfluence;
        firedrip.gameObject.SetActive(true);
    }
}
