using UnityEngine;
using System.Collections;

public class Crusher : MonoBehaviour {
    [Tooltip("5 is fast 1 is slow 10 is too fast (unless you have no acceleartion)")]
    public float speedGoingDown;
    [Tooltip("DO NOT PLACE THIS NUMBER ABOVE ONE (unless you wish to break the game. 0.5 is a good middle, .2 is slowish, 1 is fast anything higher will cause the crusher to fly through the ground")]
    public float accelerationGoingDown;
    [Tooltip("How long (in seconds) the crusher will stay on the ground after it has hit the ground")]
    public float durationOnTheGround;
    [Tooltip("How fast the crusher comes back up")]
    public float speedComingBackUp;
    float currentSpeed;
    bool grounded;
    bool falling;
    bool activated;
    Vector3 startPosition;
    Vector3 moveVector;

    public Transform crusherMesh;
    public Transform crusherMeshPart2;


	// Use this for initialization
	void Start () {
        startPosition = GetComponent<Transform>().position;    
        speedGoingDown = -Mathf.Abs(speedGoingDown);
        currentSpeed = speedGoingDown;
        print(currentSpeed);
        falling = true;
    }
	
	// Update is called once per frame
	void Update () {
        //Is the crusher grounded?
        grounded = (Physics.Raycast(crusherMeshPart2.position, -Vector3.up, 1f));
        

        //If the crusher isn't falling and it isn't grounded, then have it accelerat towards the ground
        if(!grounded && falling) {
            currentSpeed -= accelerationGoingDown;
           // print(currentSpeed);
            moveVector = new Vector3(0, currentSpeed, 0);
            transform.Translate(moveVector * Time.deltaTime);
            //If it is grounded and has not started moving back up yet, then wait for the user defiened amount of time then move back up
        } else if (grounded && falling) {
            currentSpeed = 0;
            if(!activated) {
                activated = true;
                StartCoroutine("setFallingToFalse");
            }
            //Else just move back up
        } else {
            activated = false;
            currentSpeed = speedComingBackUp;
           moveVector = new Vector3(0, currentSpeed, 0);
            transform.Translate(moveVector * Time.deltaTime);
        }

        //If the starting y position is greater that the current y position, then it should start falling again
        if (!falling && GetComponent<Transform>().position.y > startPosition.y) {
            falling = true;
            currentSpeed = speedGoingDown;
        }

    }

    //Sets falling to false so the crusher can climb back up.
    IEnumerator setFallingToFalse() {
        yield return new WaitForSeconds(durationOnTheGround);
        falling = false;
    }
}

