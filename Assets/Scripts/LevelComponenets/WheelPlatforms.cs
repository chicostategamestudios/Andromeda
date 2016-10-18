using UnityEngine;
using System.Collections;

public enum WheelType{
	Still,
	FreeSpinning,
	momentum,


}
//this is the wheel platform holder
public class WheelPlatforms : MonoBehaviour {
	public WheelType WheelBehavior;

	public Transform wheel;
	public Transform[] platforms;
	public Transform[] platformPoints;
	public float rotationSpeed;
	public float JumpSpeedAdded;
	public float WheelBreakpoint;
	public float deceleration;
	public WheelPlatform[] platformComp;
	public float maxSpeed = 0.8f;
	Quaternion StartingRot;
	// Use this for initialization
	void Start () {
		LevelReset.myLevelElements.Add (this);

		StartingRot = wheel.rotation;

        if (WheelBehavior == WheelType.Still)
        {
            for (int plat = 0; plat < platforms.Length; plat++)
            {
                platforms[plat].position = platformPoints[plat].position;
            }
            return;
        }
        else if (WheelBehavior == WheelType.FreeSpinning)
        {
            InvokeRepeating("RotateWheel", 0.01f, 0.01f);
            return;
        }
        else if (WheelBehavior == WheelType.momentum)
        {
            InvokeRepeating("RotateWheel", 0.01f, 0.01f);
            rotationSpeed = 0;
        }
        
        

		for (int plat = 0; plat < platforms.Length; plat++) {
			//Debug.Log (plat);
			if (platforms[plat].GetComponent<WheelPlatform>() == null)
			{
				platforms[plat].gameObject.AddComponent<WheelPlatform>();
			}
			platformComp [plat] = platforms [plat].GetComponent<WheelPlatform> ();

		}

		//
	}
	
	public void Reset(){
		wheel.rotation = StartingRot;
		rotationSpeed = 0;
	}


	void RotateWheel () {
		//platforms [plat].position = platformPoints [plat].position;
		wheel.Rotate (Vector3.up * rotationSpeed);
		ChangePlatType ();
	
		if (rotationSpeed > 0) {
			rotationSpeed *= deceleration;
		}
		if (rotationSpeed < 0) {
			rotationSpeed *= deceleration;
		}

		if (Mathf.Abs (rotationSpeed) < 0.011) {
			rotationSpeed = 0;
		}

		if (Mathf.Abs (rotationSpeed) > maxSpeed) {
			if (rotationSpeed < 0) {
				rotationSpeed =  (maxSpeed * -1);
			} else {
				rotationSpeed = maxSpeed;
			}
		}

	}

	void ChangePlatType(){
		for (int plat = 0; plat < platforms.Length; plat++) {
			platforms [plat].position = platformPoints [plat].position;
		}

	}


}
