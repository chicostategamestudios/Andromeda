using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This is the testing Lava script, it is used to make the lava wall move along a path, filling with lava chunks behind it

public class TestingLava : MonoBehaviour {

	public static TestingLava LavaWall;

	List<LavaChunk> myTargetPoints = new List<LavaChunk> (); //this list will be filled by all of my children that have
															//lava chunks attached to them
	LavaChunk checkPoint; //this holds the last checkPoint. It is set when we pass by 
	//a lava chunk that is set as a checkpoint, or when we do a "Catch Up" to a certain chunk

	public float speed = 0.1f;
	public Transform myLavaWall;
	int currentPos = 0;
	float rotSpeedModifier = 0.1f;
	public float scalingRate = 2f;

	public Vector3 diffVec = Vector3.zero;
	Vector3 startingVec = Vector3.zero;
	//public float curHeight;
	//public float newHeight;
	float ChunkDistance =0f;
	bool move = false;
	void Awake () {
		LavaWall = this;
		LevelReset.myLevelElements.Add (this);
		Transform myTargetParents = this.transform.FindChild ("Targets");

		for (int child = 0; child < myTargetParents.childCount; child++) {
			myTargetPoints.Add (myTargetParents.GetChild (child).GetComponent<LavaChunk>());
			myTargetPoints [child].SetLavaWall (this);
			myTargetPoints [myTargetPoints.Count - 1].DisableRenderer ();
		}


	}

	public void StartLavaWall(){


		checkPoint = myTargetPoints [0];
		myLavaWall.transform.position = myTargetPoints [0].transform.position;
		myLavaWall.LookAt (myTargetPoints [1].transform.position );
		move = true;
	}


	// Update is called once per frame
	void FixedUpdate () {
		if (move) {
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
				if (currentPos < (myTargetPoints.Count - 1)) {
					float curHeight = myTargetPoints [currentPos].transform.GetComponentInChildren<MeshFilter> ().mesh.bounds.max.y * scalingRate;
					//ChunkDistance = Vector3.Distance (myTargetPoints [currentPos].transform.position, myTargetPoints [currentPos + 1].transform.position);

					myLavaWall.localScale = new Vector3 (myLavaWall.localScale.x, curHeight, myLavaWall.localScale.z);
			
					currentPos++;

					//newHeight = myTargetPoints [currentPos].transform.GetComponentInChildren<MeshFilter> ().mesh.bounds.extents.y * 2;


				}
			}
		}
	}
	/*
	void Update(){
		if (currentPos > 0) {
		//	Scale ();
		}
	}
	*/
	public void Scale(){
		if (myLavaWall != null) {
			float curDistance = Vector3.Distance(myLavaWall.transform.position, myTargetPoints[currentPos].transform.position);
				
			//float heightChange = newHeight - curHeight;
			//Debug.Log (heightChange);
			//heightChange *= curDistance / ChunkDistance;

			//Vector3 newScale = new Vector3 (1, newHeight, 1);
		
			//myLavaWall.localScale = Vector3.Lerp (startingVec, newScale, (curDistance/ChunkDistance));
		
		
		
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
