using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class WindyArea : MonoBehaviour {
    [Tooltip("The force the player gets pushed in the x direction")]
    public float xForce = 5f;
    [Tooltip("The force the player gets pushed in the y direction")]
    public float yForce = 5f;
    [Tooltip("How long in seconds the player gets pushed left")]
    public float timeGoingLeft = 2;
    [Tooltip("How long in seconds the player gets pushed right")]
    public float timeGoingRight = 2;
    public bool activated = false;
    PlayerMovement move;
    

	void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            if (!activated) {
                //Once the player collides with the wind object, then
                StartCoroutine("Windy");
                activated = true;
                move = col.GetComponent<PlayerMovement>();
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if(col.tag == "Player" && activated) {
            StopCoroutine("Windy");
            activated = false;
        }
    }

    IEnumerator Windy () {
        while (true) {
            yield return new WaitForSeconds(timeGoingLeft);
            move.modificationVec = new Vector2 (-Mathf.Abs(xForce), yForce); 
            yield return new WaitForSeconds(timeGoingRight);
            move.modificationVec = new Vector2(Mathf.Abs(xForce), yForce);
        }
    }
}
