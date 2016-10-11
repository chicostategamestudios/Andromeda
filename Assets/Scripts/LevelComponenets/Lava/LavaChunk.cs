using UnityEngine;
using System.Collections;

//The lava chunk script will be attached to each piece of the background lava wall
//This will handle things like the activation and deactivation of the background lava stuff 
//right now this is purely visual, but things like damage can be easily attached 

public class LavaChunk : MonoBehaviour {
	Renderer myRenderer;//The renderer of this chunk of the lava wall

	 
	public bool isCheckPoint; //Level designers tick this on if they want this to be a checkpoint for the lava wall
	//when the player dies, the lava wall will reset to the last lava wall that was set as a checkpoint

	TestingLava myLavaWall; //this is the lava wall itself, we keep a refrence to it so when we need to run things on the lava wall
	//we can

	public void DisableRenderer(){ //this is called by the lava wall, on start all of the chunks will be disabled,
									//as the wall moves, chunks are enabled. When the wall is reset 
									//all chunks are disabled, and then re-enabled up to the point it needs to be enabled to
									//Note: it might be a good idea to calculate deltas instead
		if (myRenderer == null) {
			myRenderer = getMyRenderer (); //if we don't have a ref to our renderer yet, get it
		}
		myRenderer.enabled = false;
	}

	public void SetLavaWall(TestingLava myLava){
		myLavaWall = myLava; //when the lava chunk is grabbed by the lava wall controller, give chunks a reference to be used
							//in the snap to me
	}

	public void SnapToMe(){ //this is ran by checkpoints, and will make the lava wall catch up to this location
		myLavaWall.SnapWall (this); 
	}

	public bool CheckPoint{ //we use this to get if we're a checkpoint by the lava wall controller, so it knows to set the checkpoint
		get{ return isCheckPoint; }
	}

	public void EnableRenderer(){ //as started in disable renderer, this is ran to turn the renderers of the lava wall on
									//further functionality may be added later						
		if (myRenderer == null) {
			myRenderer = getMyRenderer ();
		}
		myRenderer.enabled = true;
	}

	Renderer getMyRenderer(){
		return this.gameObject.GetComponent<Renderer> (); //this is ran by enable and disable renderer if they 
														//don't have a ref to the renderer yet
	}


}
