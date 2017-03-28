using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;
using Assets.Scripts.Character;

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
    Vector2 modVec;

    public ParticleSystem mySystem;
	void Start()
	{
		move = CharController.Instance.gameObject.GetComponent<PlayerMovement>();
	}

	void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            if (!activated) {
                //Once the player collides with the wind object, 
                activated = true;
                move = col.GetComponent<PlayerMovement>();
                StartCoroutine("Windy");
                
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if(col.tag == "Player" && activated) {
            StopCoroutine("Windy");
            activated = false;
            modVec = Vector2.zero;
        }
    }

    IEnumerator Windy () {
        if(mySystem != null)
        {
            
            mySystem.Play();
        }
        while (activated) {
            yield return new WaitForSeconds(timeGoingLeft);
            modVec = new Vector2 (-Mathf.Abs(xForce), yForce); 
            yield return new WaitForSeconds(timeGoingRight);
            modVec = new Vector2(Mathf.Abs(xForce), yForce);
        }
        if (mySystem != null)
        {

            mySystem.Stop();
        }
    }

    void Update() {
        if (activated) {
            move.modificationVec = modVec;
        } else
        {
            move.modificationVec = Vector2.zero;
        }
        //print(modVec);
    }
}
