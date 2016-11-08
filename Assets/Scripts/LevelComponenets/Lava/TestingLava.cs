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

	public float minHeight = 10f;

	public float startDelay = 1f;

	//public Vector3 diffVec = Vector3.zero;
	Vector3 startingVec = Vector3.zero;
	//public float curHeight;
	//public float newHeight;
	float ChunkDistance =0f;
	[HideInInspector]public bool move = false;
	void Awake () {
		LavaWall = this;
		LevelReset.myLevelElements.Add (this); //I add this to the LevelReset to the monobehavior, this will call the reset
												//function when the player dies. if this code was more organized/better
												//we would instead be inheiting our level elements from a base class
												//and calling a virtual Reset function which could be overriden in each child
												//class.
		Transform myTargetParents = this.transform.FindChild ("Targets"); //This is the object that holds my targets
																			//this isn't the best practice, but was good enough for now

		for (int child = 0; child < myTargetParents.childCount; child++) { //for each child of my child holder
			myTargetPoints.Add (myTargetParents.GetChild (child).GetComponent<LavaChunk>());  //add their lava chunk to my list
			myTargetPoints [child].SetLavaWall (this); //set the lava wall reference in the chunk, so they can call the snap and whatnot
			myTargetPoints [myTargetPoints.Count - 1].DisableRenderer (); //go to each lava chunk and call disable renderer, so they're invisible
		}


	}

	public void StartLavaWall(){

		//This is ran by the relic, it just starts the lava wall
		StartCoroutine("TimedLavaWall");
	}

	IEnumerator TimedLavaWall(){
		yield return new WaitForSeconds (startDelay);
		checkPoint = myTargetPoints [0];  //I set my initial checkpoint to the first lava chunk
		myLavaWall.transform.position = myTargetPoints [0].transform.position; //set my position to the start position
		myLavaWall.LookAt (myTargetPoints [1].transform.position ); //look at my next position
		move = true; //start moving forward

	}


	// Update is called once per frame
	void FixedUpdate () {
		if (move) { //this is set in wall. We could also just make this a coroutine that is ran on fixed udpate, but this works fine
			myLavaWall.transform.position = Vector3.MoveTowards (myLavaWall.transform.position, myTargetPoints [currentPos].transform.position, speed); //move the wall towards the next chunk
			Vector3 dir = myTargetPoints [currentPos].transform.position - myLavaWall.transform.position; //this is the vector that the wall is moving in, we use this to rotate towards this vector
		
			Vector3 newDir = Vector3.RotateTowards (myLavaWall.transform.forward, dir, rotSpeedModifier / (Vector3.Distance (myLavaWall.transform.position, myTargetPoints [currentPos].transform.position)), 0f);
				//this will rotate us towards the vector between our current position and our target position divided by the distance between the two, to make smoother rotations

			myLavaWall.rotation = Quaternion.LookRotation (newDir);
				//look at the vector calculated above


			if (myLavaWall.transform.position == myTargetPoints [currentPos].transform.position) { //if we reach our target Lava Chunk
				myTargetPoints [currentPos].EnableRenderer (); //set the wall to Active
				if (myTargetPoints [currentPos].CheckPoint) { //if this chunk is also a checkpoint, 
					checkPoint = myTargetPoints [currentPos]; //set it as lava wall checkpoint
				}
				if (currentPos < (myTargetPoints.Count - 1)) { //if we are not at the end of our list
					float curHeight = myTargetPoints [currentPos].transform.GetComponentInChildren<MeshFilter> ().mesh.bounds.max.y * scalingRate; //get the height of the mesh of this lava chunk
					//ChunkDistance = Vector3.Distance (myTargetPoints [currentPos].transform.position, myTargetPoints [currentPos + 1].transform.position);
					if (curHeight < minHeight) {
						curHeight = minHeight;
					}

					myLavaWall.localScale = new Vector3 (myLavaWall.localScale.x, curHeight, myLavaWall.localScale.z); //set our lava walls scale to the height of the mesh of the chunk (ideally this would happen over time between the two poitns)		
					currentPos++; //set current pos(this current chunk we're operating on) to one position forward
				}
			}
		}
	}
		
	public void Reset(){ //this is ran by the LevelReset script
						//it goes and disables all meshses, and then moves to wall forward to the last checkpoint,
						//enabling all chunks behind it
		if (!move) {
			return;
		}


		for (int check = 0; check < myTargetPoints.Count; check++) { 
			myTargetPoints [check].DisableRenderer (); //re disable all renderers
		}

		int nextPoint = 0; //go to pos 0
		while (nextPoint < myTargetPoints.IndexOf (checkPoint)) { //move through our list until we reach our checkpoint
			myTargetPoints [nextPoint].EnableRenderer (); //enabling each thing
			nextPoint++; //and itirating forward
		}
		currentPos = myTargetPoints.IndexOf (checkPoint); //once we reach our checkpoint... 
		myLavaWall.transform.position = checkPoint.transform.position;//...set it as our current position, so we start moving towards the next position
	}

	public void SnapWall(LavaChunk CheckToAdd){ //This will snap the wall to a position. This is called by lava chunks if they are told to by a checkpoint.
												
		//when a player reaches a checkpoint 
		for (int check = 0; check < myTargetPoints.Count; check++) { 
			myTargetPoints [check].DisableRenderer (); //re disable all renderers
		}

		int nextPoint = 0;

		while (nextPoint < myTargetPoints.IndexOf (CheckToAdd)) { //move towards our new position
			myTargetPoints [nextPoint].EnableRenderer ();
			nextPoint++;
		}
		currentPos = myTargetPoints.IndexOf (CheckToAdd); //once we get to where we're going set it as current chunk
		myLavaWall.transform.position = CheckToAdd.transform.position; //move to current chunk
		checkPoint = CheckToAdd; //set it as a checkpoint
	}





}
