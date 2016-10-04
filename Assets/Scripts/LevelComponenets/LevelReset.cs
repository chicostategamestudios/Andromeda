using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelReset : MonoBehaviour {
	public static List<MonoBehaviour> myLevelElements = new List<MonoBehaviour>();

	public bool reset = false;

	public void Reset(){

		foreach (var plat in myLevelElements) {
			if (plat is WheelPlatforms) {
				(plat as WheelPlatforms).Reset ();
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
