using UnityEngine;
using System.Collections;

public class GasPlumePlat : MonoBehaviour {
	Vector3 startPos;
	public float grav;
	public float JumpForce;
	float vertSpeed;
	public float HowOften;
	bool Go = false;
	bool addForce;
	Vector3 moveVec;
	public float maxHeight;


	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (GasPlume.S.isIgnited && transform.position.y < maxHeight) {
			addForce = true;
		}

		if (addForce) {
			vertSpeed = JumpForce;

			addForce = false;
		}
		if (transform.position.y >= startPos.y) {
			transform.Translate(moveVec * Time.deltaTime);
			vertSpeed -= grav*Time.deltaTime;
			moveVec.y = vertSpeed;
		}
		if (transform.position.y < startPos.y) {
			transform.position = startPos;
		}




	}

}
