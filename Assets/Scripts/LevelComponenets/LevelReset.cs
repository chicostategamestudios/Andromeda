using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelReset : MonoBehaviour {
	public static List<MonoBehaviour> myLevelElements = new List<MonoBehaviour>();

	public bool reset = false;

	public static void Reset(){

		Debug.Log("reset");

		foreach (var plat in myLevelElements) {
			if (plat is WheelPlatforms) {
				(plat as WheelPlatforms).Reset ();
			}
			if (plat is Pillar)
			{
				(plat as Pillar).Reset();
			}
			if (plat is RollingPillarBehavior)
			{
				(plat as RollingPillarBehavior).Reset();
			}
			if (plat is FragilePillar)
			{
				(plat as FragilePillar).Reset();
			}
            if (plat is Ice_Trigger)
            {
                (plat as Ice_Trigger).Reset();
            }
            if(plat is Autospin_Wheel_Platforms)
            {
                (plat as Autospin_Wheel_Platforms).Reset();
            }


		}



	}

	void Update(){
		if (reset) {
			Reset ();
			reset = false;
		}

	}



}
