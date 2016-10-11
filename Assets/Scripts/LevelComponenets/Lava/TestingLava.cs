using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This is the testing Lava script, it is used to make the lava wall move along a path, filling with lava chunks behind it

public class TestingLava : MonoBehaviour {
	
	List<LavaChunk> myTargetPoints = new List<LavaChunk> (); //this list will be filled by all of my children that have
															//lava chunks attached to them
	LavaChunk checkPoint; //this holds the last checkPoint. It is set when we pass by 
	//a lava chunk that is set as a checkpoint, or when we do a "Catch Up" to a certain chunk

	public float speed = 0.1f;
	public Transform myLavaWall;
	int currentPos = 0;
	float rotSpeedModifier = 0.1f;


	public Vector3 diffVec = Vector3.zero;

	void Awake () {
		LevelReset.myLevelElements.Add (this);
	
		Transform myTargetParents = this.transform.FindChild ("Targets");

		for (int child = 0; child < myTargetParents.childCount; child++) {
			myTargetPoints.Add (myTargetParents.GetChild (child).GetComponent<LavaChunk>());
			myTargetPoints [child].SetLavaWall (this);
			myTargetPoints [myTargetPoints.Count - 1].DisableRenderer ();
		}

		checkPoint = myTargetPoints [0];
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
			if (currentPos < (myTargetPoints.Count-1)) {
				currentPos++;
			}
		}


	}

	public void Reset(){

		for (int check = 0; check < myTargetPoints.Count; check++) {
			myTargetPoints [check].DisableRenderer (); //re disable all renderers
		}

		int nextPoint = 0;
		while (nextPoint < myTargetPoints.IndexOf (checkPoint)) {
			myTargetPoints [nextPoint].EnableRenderer ();
			nextPoint++;
		}
		currentPos = myTargetPoints.IndexOf (checkPoint);
		myLavaWall.transform.position = checkPoint.transform.position;
	}

	public void SnapWall(LavaChunk CheckToAdd){

		for (int check = 0; check < myTargetPoints.Count; check++) {
			myTargetPoints [check].DisableRenderer (); //re disable all renderers
		}

		int nextPoint = 0;

		while (nextPoint < myTargetPoints.IndexOf (CheckToAdd)) {
			myTargetPoints [nextPoint].EnableRenderer ();
			nextPoint++;
		}
		currentPos = myTargetPoints.IndexOf (CheckToAdd);
		myLavaWall.transform.position = CheckToAdd.transform.position;
		checkPoint = CheckToAdd;
	}





}
