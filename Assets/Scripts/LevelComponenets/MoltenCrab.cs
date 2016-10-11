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

	// Use this for initialization
	void Start () {
        //mycont = transform.gameObject.AddComponent<CharacterController> ();
        //mycont.height = 1f;
        distToGround = GetComponent<Collider>().bounds.extents.y;
		if (randomTimeCheck > 0) {
			InvokeRepeating("ChangeDir", randomTimeCheck, randomTimeCheck);
		}

	}
	
	// Update is called once per frame
	void Update () {
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        {
            verticleSpeed = 0;
        }
        else
        {
            verticleSpeed -= grav * Time.deltaTime;
        }



        /*grounded = mycont.isGrounded;

		if (grounded) {
			verticleSpeed = 0;
		} else {
			verticleSpeed -= grav * Time.deltaTime;
		}
        */


        Vector3 moveVector = new Vector3 (speed * forward, verticleSpeed, 0f);
		transform.Translate (moveVector * Time.deltaTime);
        RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.right * forward, out hit, wallDist) && hit.collider.gameObject.tag != "Player") {
			forward *= -1f;
		}

	}
	void ChangeDir(){
		float chance = Random.Range (0, 100);
		if (chance < changeDirChance) {
			forward *= -1f;
		}

	}

}
