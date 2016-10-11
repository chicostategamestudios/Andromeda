using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestingLava : MonoBehaviour {
	Transform myTargetParents;
	List<LavaChunk> myTargetPoints = new List<LavaChunk> ();
	LavaChunk checkPoint;

	float speed = 0.2f;
	public Transform myLavaWall;
	int currentPos = 0;
	float rotSpeedModifier = 0.1f;
	// Use this for initialization

	public Vector3 diffVec = Vector3.zero;

	void Start () {
		myTargetParents = this.transform.FindChild ("Targets");

		for (int child = 0; child < myTargetParents.childCount; child++) {
			myTargetPoints.Add (myTargetParents.GetChild (child).GetComponent<LavaChunk>());
			myTargetPoints [myTargetPoints.Count - 1].DisableRenderer ();
		}


		myLavaWall.LookAt (myTargetPoints [1].transform.position );


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		diffVec = (myLavaWall.transform.position - myTargetPoints [currentPos].transform.position);

		myLavaWall.transform.position = Vector3.MoveTowards (myLavaWall.transform.position, myTargetPoints [currentPos].transform.position, speed);
		Vector3 dir = myTargetPoints [currentPos].transform.position - myLavaWall.transform.position;

		Vector3 newDir = Vector3.RotateTowards (myLavaWall.transform.forward, dir, rotSpeedModifier / (Vector3.Distance (myLavaWall.transform.position, myTargetPoints [currentPos].transform.position)), 0f);

		myLavaWall.rotation = Quaternion.LookRotation (newDir);



		if (myLavaWall.transform.position == myTargetPoints [currentPos].transform.position) {
			myTargetPoints [currentPos].EnableRenderer ();
			if (myTargetPoints [currentPos].CheckPoint) {
				checkPoint = myTargetPoints [currentPos];
			}
			currentPos++;
		}


	}
}
