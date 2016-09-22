using UnityEngine;
using System.Collections;

public class MoltenCrab : MonoBehaviour {

	bool grounded;
	public float speed = 3;
	public float randomTimeCheck;
	public float changeDirChance;
	public float damage;
	float forward =1;
	public float wallDist =0.6f;
	float verticleSpeed;
	CharacterController mycont;
	float grav = 10;
    float distToGround;
    public float ledgeDrop = 5f;

	// Use this for initialization
	void Start () {
        //sets distToGround to the distance from the center of the AI to its bottom
        //starts the function to randomly have the AI change directions. 
        distToGround = GetComponent<Collider>().bounds.extents.y;
		if (randomTimeCheck > 0) {
			InvokeRepeating("ChangeDir", randomTimeCheck, randomTimeCheck);
		}

	}
	
	// Update is called once per frame
	void Update () {
        //raycast distance distToGround plus .1 to check if there is floor. If true, there is floor and verticalSpeed is 0
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        {
            verticleSpeed = 0;
            //raycast downward from a short distance in front of the AI, goes a distance specified by distToGround + ledgeDrop. If this returns true there is a ledge and the AI can drop down, if not there is no ledge and the AI turns around. ledgeDrop can be changed to allow the AI to drop down greater distances.
            if (Physics.Raycast(new Vector3(transform.position.x + forward, transform.position.y - .25f, transform.position.z), -Vector3.up, distToGround + ledgeDrop))
            {
                Debug.Log("ledge");
            }
            else
            {
                Debug.Log("no ledge");
                forward *= -1f;
            }
        }
        else
        {
            //if there is no floor under the AI add gravity to it.
            verticleSpeed -= grav * Time.deltaTime;
        }

        //move forward and raycast forward to check for walls while ignoring the player. if raycast hits, turn the ai around
        Vector3 moveVector = new Vector3 (speed * forward, verticleSpeed, 0f);
		transform.Translate (moveVector * Time.deltaTime);
        RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.right * forward, out hit, wallDist) && hit.collider.gameObject.tag != "Player") {
			forward *= -1f;
		}

	}
    //Set the value "changeDirChance" in inspector from 0 - 100 to give determine the chance of it changing direction when this is called.
    void ChangeDir(){
        float chance = Random.Range (0, 100);
		if (chance < changeDirChance) {
			forward *= -1f;
		}

	}

}
