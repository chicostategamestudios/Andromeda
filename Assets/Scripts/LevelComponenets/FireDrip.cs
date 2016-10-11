using UnityEngine;
using System.Collections;

public class FireDrip : MonoBehaviour {
    [Tooltip("The rate at which the fireball falls. 20 is a good speed, 10 is slow, 30 is fast.")]
    public float gravityRate;
    float verticleSpeed;
    Vector3 moveVector;
    bool grounded;
    float rayDist = 1.2f;
    float gravdecrease = .9f;
    
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //check if grounded, grouded is equal to raycast
        grounded = (Physics.Raycast(transform.position, -Vector3.up, rayDist));
        //If not grounded then force of gravity pushes down
        if (!grounded)
        {
            verticleSpeed -= gravityRate * Time.deltaTime;

        }
        else //Destroy the drip
        {
            Destroy(gameObject);
        }
        moveVector = new Vector3(0, verticleSpeed, 0);
        //transforming object for movement
        transform.Translate(moveVector * Time.deltaTime);
    }
}
