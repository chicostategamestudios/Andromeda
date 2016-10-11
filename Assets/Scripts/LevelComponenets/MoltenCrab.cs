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
    float distToEdge;
    public float ledgeDrop = 5f;
    private bool floorCheck;

	// Use this for initialization
	void Start () {
        //sets distToGround to the distance from the center of the AI to its bottom
        //starts the function to randomly have the AI change directions. 
        distToGround = GetComponent<Collider>().bounds.extents.y;
        distToEdge = GetComponent<Collider>().bounds.extents.x;
		if (randomTimeCheck > 0) {
			InvokeRepeating("ChangeDir", randomTimeCheck, randomTimeCheck);
		}

	}
	
	// Update is called once per frame
	void Update () {
        //raycast distance distToGround plus .1 to check if there is floor. If true, there is floor and verticalSpeed is 0
        //Debug.DrawRay(new Vector3(transform.position.x + distToEdge * -forward, transform.position.y, transform.position.z), -this.transform.up, Color.red);
        if (Physics.Raycast(new Vector3(transform.position.x + distToEdge * -forward, transform.position.y, transform.position.z), -this.transform.up, distToGround + 0.2f) && floorCheck == false)
        {
        }
        else
        {
            floorCheck = true;
            StartCoroutine("checkForFloor");
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z - 90));
        }

        //move forward and raycast forward to check for walls while ignoring the player. if raycast hits, turn the ai around
        Vector3 moveVector = new Vector3 (speed * forward, verticleSpeed, 0f);
		transform.Translate (moveVector * Time.deltaTime);
        RaycastHit hit;
        Debug.DrawRay(transform.position, this.transform.right * forward);
        if (Physics.Raycast (transform.position, this.transform.right * forward, out hit, wallDist) && hit.collider.gameObject.tag != "Player") {
			//forward *= -1f;
            if (forward > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + 90));
            }
            else
            {
                //rotate -90 and move up
            }
		}

	}
    //Set the value "changeDirChance" in inspector from 0 - 100 to give determine the chance of it changing direction when this is called.
    void ChangeDir(){
        float chance = Random.Range (0, 100);
		if (chance < changeDirChance) {
			forward *= -1f;
		}

	}

    IEnumerator checkForFloor()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("happens");
        if (Physics.Raycast(transform.position, -this.transform.up, distToGround + 0.1f))
        {
            floorCheck = false;
        }
        else
        {
            Debug.Log("there is no floor");
        }
    }

}
