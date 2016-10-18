using UnityEngine;
using System.Collections;
using FMODUnity;


public class HiddenPlatform_Audio : MonoBehaviour {

	Zmovement platformScript;

	[SerializeField]
	StudioEventEmitter target;


	public Transform platformTrans;

	bool playingSound;

	// Use this for initialization
	void Start () {

		playingSound = false;
		platformScript = GetComponentInParent<Zmovement> ();
		target = GetComponent<StudioEventEmitter> ();

		platformTrans = transform.parent;



	
	
	}
	
	// Update is called once per frame
	void Update () {
		platformTrans = transform.parent;

		if (platformTrans.hasChanged) {
			Debug.Log ("Transform changed");
			platformTrans.hasChanged = false;
		}

		if (platformTrans.hasChanged == false && !target.IsPlaying() ) {
			Debug.Log ("playing sound");
			target.Play ();
		}

	
	}
}
