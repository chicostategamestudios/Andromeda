using UnityEngine;
using System.Collections;

public class RollingPillarBehavior : MonoBehaviour {

	public bool StartRolling;

	public float speed;
	public float jumpheight;
	public float gravityRate;
	float verticleSpeed;
	Vector3 moveVector;
	public bool grounded;
	public float rayDist = 1.2f;
	public float rayDir;
	public float rotSpeed;
	public Transform PillarMesh;
    // Use this for initialization


    void Start () {
        //Detect 
		rayDir = speed / (Mathf.Abs (speed));
		PillarMesh = this.transform.GetChild (0);
	}

    // Update is called once per frame


    void Update () {
        //If not start rolling return
		if (!StartRolling) {
			return;
		}
        //check if grounded, grouded is equal to raycast
        grounded = (Physics.Raycast (transform.position, -Vector3.up, rayDist));

        //If not grounded then force of gravity pushes down
        if (!grounded) {
			verticleSpeed -= gravityRate * Time.deltaTime;
		} else //If grounded then jump up equal to jump height
        {
			verticleSpeed = jumpheight;
		}

		PillarMesh.Rotate (Vector3.up* rotSpeed* rayDir * Time.deltaTime);

		moveVector = new Vector3 (speed * rayDir,0, verticleSpeed);
		transform.Translate (moveVector * Time.deltaTime);

		if (Physics.Raycast (transform.position, Vector3.right * rayDir, rayDist, 8)) {
			
			rayDir *= -1f;
		}


	}

	void OnTriggerEnter(Collider col){
		
		if (col.gameObject.name == "CubeDeath") {
			Debug.Log ("fuck");
			Destroy (this.gameObject);
		}

	}


}
