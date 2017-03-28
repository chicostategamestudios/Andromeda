using UnityEngine;
using System.Collections;
//using FMODUnity;uncomment this when fmod is back

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
    //private StudioEventEmitter target;uncomment this when fmod is back
    bool activated = false;
    public GameObject firedrip_;

	private Object[] audioPrefabs;
	private GameObject dragonDrippingAudio;

    //[FMODUnity.EventRef]uncomment this when fmod is back
    //private string dragonDrippingAudioString = "event:/Lava Level/Structures/Dragon Statues/DrippingDragon";


	void Awake(){
        /*uncomment this when fmod is back
        foreach (Transform child in this.transform) {
			if (child.gameObject.GetComponent<StudioEventEmitter> () != null) {
				target = child.gameObject.GetComponent<StudioEventEmitter> ();
			}
		}
        */
    }

    // Use this for initialization
    void Start () {
		audioPrefabs = Resources.LoadAll ("");

        /*uncomment this when fmod is back
        foreach (Object gameAudio in audioPrefabs) {
			if (gameAudio is GameObject) {
				if ((gameAudio as GameObject).GetComponent<StudioEventEmitter> ()) {
					dragonDrippingAudio = (gameAudio as GameObject);
				}
			}
		}
        */

        dragonDrippingAudio = GameObject.Instantiate (dragonDrippingAudio, this.transform.position, Quaternion.identity) as GameObject;
		dragonDrippingAudio.transform.parent = this.transform;
		dragonDrippingAudio.transform.localPosition = Vector3.zero;
        //Double Check This
        //dragonDrippingAudio.GetComponent<StudioEventEmitter> ().Event = dragonDrippingAudioString;uncomment this when fmod is back
        //Debug.Log (dragonDrippingAudio.GetComponent<StudioEventEmitter> ().Event);


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
        //target.Play();
		//dragonDrippingAudio.GetComponent<StudioEventEmitter> ().Play ();
        GameObject firedrip = (GameObject)Instantiate(firedrip_, this.transform.position, this.transform.rotation);
        firedrip.GetComponent<FireDrip>().gravityRate = gravityInfluence;
        firedrip.gameObject.SetActive(true);
    }
}
