using UnityEngine;
using System.Collections;
//using FMODUnity;

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
    //public StudioEventEmitter target; uncomment this when fmod is back

	private Object[] audioPrefabs;
	private GameObject dragonLaserAudio;

    //[FMODUnity.EventRef]uncomment this when fmod is back
    //private string dragonLaserAudioString = "event:/Lava Level/Structures/Dragon Statues/LaserDragon";uncomment this when fmod is back

    // Use this for initialization
    void Start()
    {
        //Initializing the public varibles of the laser beam based on user input
        beam.GetComponent<FlamePillar>().beamDuration = beamDuration;
        beam.GetComponent<FlamePillar>().dealyOfExplosion = dealyOfExplosion;
        beam.GetComponent<FlamePillar>().explosionDuration = explosionDuration;

		audioPrefabs = Resources.LoadAll ("");
        /*uncomment this when fmod is back
		foreach (Object gameAudio in audioPrefabs) {
			if (gameAudio is GameObject) {
				if ((gameAudio as GameObject).GetComponent<StudioEventEmitter> ()) {
					dragonLaserAudio = (gameAudio as GameObject);
				}
			}
		}
        */
        dragonLaserAudio = GameObject.Instantiate (dragonLaserAudio, this.transform.position, Quaternion.identity) as GameObject;
		dragonLaserAudio.transform.parent = this.transform;
		dragonLaserAudio.transform.localPosition = new Vector3 (-5.78f, 55.12f, -68.88f);
		//Double Check This
		//dragonLaserAudio.GetComponent<StudioEventEmitter> ().Event = dragonLaserAudioString;
		//Debug.Log (dragonLaserAudio.GetComponent<StudioEventEmitter> ().Event);



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
        //uncomment this when fmod is back
		//dragonLaserAudio.GetComponent<StudioEventEmitter> ().Play ();
        //The beam gets activated (It deactivates itself)
        beam.GetComponent<FlamePillar>().isActive = true;
    }
}
