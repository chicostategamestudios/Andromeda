using UnityEngine;
using System.Collections;

public class BreakingBridge : MonoBehaviour {
    [Tooltip("Has the relic been taken and the bridge ready to break?")]
    public bool isActive;
    [Tooltip("The left most board is 1, and the right most board is 30, input which board you wish to break. Defaults to 15")]
    public int boardToBreak;
    [Tooltip("The left most board is 1, and the right most board is 30, input which board the player must walk over to break the bridge (it's got a high height to avoid players jumping over it). Defaults to 15")]
    public int boardToTrigger;
    [Tooltip("This bridge breaks a board after it hits a trigger that is above the board. Only breaks if the relic has been taken")]
    public bool hover__for__a__Tooltip;

    GameObject[] planks;
    // Use this for initialization
    void Start () {
        boardToBreak = 15;
        boardToTrigger = 15;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
