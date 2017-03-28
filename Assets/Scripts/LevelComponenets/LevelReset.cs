using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelReset : MonoBehaviour {
	public static List<MonoBehaviour> myLevelElements = new List<MonoBehaviour>();

    private static List<LevelElement> LevelElements = new List<LevelElement>();

	public bool reset = false;

	public static void Reset(){

        //Debug.Log("reset");

        for (int ele = 0; ele < LevelElements.Count; ele++)
        {
            Debug.Log("resetting: " + LevelElements[ele]);
            LevelElements[ele].Reset();
        }

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
			
			}if (plat is TestingLava && ((plat as TestingLava).move)) {
				(plat as TestingLava).Reset ();
			}
            if(plat is WheelPlatform_Audio)
            {
                (plat as WheelPlatform_Audio).Reset();
            }

		}

       



	}

    public static void AddToLevelElements(LevelElement newElement)
    {
        LevelElements.Add(newElement);
    }

	void Update(){
		if (reset) {
			Reset ();
			reset = false;
		}

	}



}
