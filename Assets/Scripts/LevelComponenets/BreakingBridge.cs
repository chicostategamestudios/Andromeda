using UnityEngine;
using System.Collections;

public class BreakingBridge : MonoBehaviour {
    [Tooltip("Make this true to activate the bridge breaking before the temple is active")]
    public bool isActive;
    [Tooltip("The left most board is 1, and the right most board is 30, input which board you wish to break. Defaults to 15")]
    public int boardToBreak;
    [Tooltip("The left most board is 1, and the right most board is 30, input which board the player must walk over to break the bridge (it's got a high height to avoid players jumping over it). Defaults to 15")]
    public float boardToTrigger;
    [Tooltip("This bridge breaks a board after it hits a trigger that is above the board. Only breaks if the relic has been taken")]
    public bool hover__for__a__Tooltip;

    public Transform[] planks; //Planks assosiated with this bridge
    BoxCollider myTrigger; // the box collider assosiated with this bridge (Is a trigger)


    // Use this for initialization
    void Start () {
        myTrigger = this.gameObject.GetComponent<BoxCollider>();

        if (boardToBreak > 30 || boardToBreak <= 0) {
            boardToBreak = 15;
        }

        if (boardToTrigger > 30 || boardToTrigger <= 0) {
            boardToTrigger = 15;
        }
        boardToBreak -= 1;
        boardToTrigger -= 1;

        myTrigger.size = new Vector3(2f, 5f, .5f);
        myTrigger.center = new Vector3(0, 0, boardToTrigger * .5f);
    }
	
	// Update is called once per frame
	void Update () {

    }

    /*
     * destroys the plank that the user defines if the player goes over the plank the
     * user defines and the temple is active (can also activate the breaking of the bridge
     * by setting the isActive boolean to true)
     */
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == ("Player")) {
            if (isActive || DragonHeadRelic.isTempleActive) { 
                Destroy(planks[boardToBreak].gameObject);
                if((boardToBreak + 1) < 29) {
                    planks[boardToBreak + 1].Translate(Vector3.up);
                }
            }
        }
    }
}
