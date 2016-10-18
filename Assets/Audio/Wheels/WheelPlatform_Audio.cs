using UnityEngine;
using System.Collections;
using FMODUnity;


public class WheelPlatform_Audio : MonoBehaviour {

	WheelPlatforms wplat;
	[SerializeField]
	private StudioEventEmitter Target;

	public float platformCurrentSpeed;

	LevelReset levelreset;



	// Use this for initialization
	void Start () {
		wplat = GetComponentInParent<WheelPlatforms> ();
		Target = GetComponent<StudioEventEmitter> ();
        LevelReset.myLevelElements.Add(this);
	
	}
	
	// Update is called once per frame
	void Update () {

		platformCurrentSpeed = Mathf.Abs(wplat.rotationSpeed);

		if( platformCurrentSpeed >= 0.02f){
			Target.SetParameter ("platVel", platformCurrentSpeed);
		//	Debug.Log ("Changing parameter to: " + platformCurrentSpeed);
		//	Debug.Log (wplat.WheelBehavior);
		}



		//make "LevelReset.reset" a static boolean to fix audio bug when player dies

		

	
	}

    public void Reset()
    {
        Target.SetParameter("platVel", 0f);
    }

    
}
