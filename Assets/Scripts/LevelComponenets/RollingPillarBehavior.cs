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


	private Vector3 startPosition;
	private Quaternion startRotation;

	// Use this for initialization
	void Start () {
		StoreStartPosition(ref startPosition, ref startRotation);
		LevelReset.myLevelElements.Add(this);
		rayDir = speed / (Mathf.Abs (speed));
		PillarMesh = this.transform.GetChild (0);
        gameObject.GetComponent<DealDamage>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (!StartRolling) {
			return;
        }else {
            gameObject.GetComponent<DealDamage>().enabled = true;
        }

        grounded = (Physics.Raycast (transform.position, -Vector3.up, rayDist));
		if (!grounded) {
			verticleSpeed -= gravityRate * Time.deltaTime;
		} else {
			verticleSpeed = jumpheight;
		}

		PillarMesh.Rotate (Vector3.up* rotSpeed* rayDir * Time.deltaTime);

		moveVector = new Vector3 (speed * rayDir,0, verticleSpeed);
		transform.Translate (moveVector * Time.deltaTime);

		if (Physics.Raycast (transform.position, Vector3.right * rayDir, rayDist, 1 << 8 | 1 << 0)) {
			
			rayDir *= -1f;
		}


	}

	void OnTriggerEnter(Collider col){
        Debug.Log("hit)");
		if (col.gameObject.name == "CubeDeath") {
	
			this.gameObject.SetActive(false);
		}

	}


	public void Reset()
	{
		StartRolling = false;
		this.gameObject.SetActive(true);
		this.transform.position = startPosition;
		this.transform.rotation = startRotation;

	}

	void StoreStartPosition(ref Vector3 startPos, ref Quaternion startRot)
	{

		startPos = this.transform.position;
		startRot = this.transform.rotation;

	}

}
