using UnityEngine;
using System.Collections;

public class LevelStateManager : MonoBehaviour {

	public enum levelState{
		decending,
		escapingFireLevel,
		escapingIceLevel,
		escapingWindLevel,
		escapingEarthLevel
	};

	public static levelState currentLevelState;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
