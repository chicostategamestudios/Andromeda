using UnityEngine;
using System.Collections;

public class LavaChunk : MonoBehaviour {
	Renderer myRenderer;


	public bool isCheckPoint;
	//[Tooltip("Check this if you want this to be a 'Catch Up' piece, meaning that the lava wall will snap to this location when the player 
	public bool SnapToMe;

	public void DisableRenderer(){
		if (myRenderer == null) {
			myRenderer = getMyRenderer ();
		}
		myRenderer.enabled = false;
	}

	public bool CheckPoint{
		get{ return isCheckPoint; }
	}

	public void EnableRenderer(){
		if (myRenderer == null) {
			myRenderer = getMyRenderer ();
		}
		myRenderer.enabled = true;
	}

	Renderer getMyRenderer(){
		return this.gameObject.GetComponent<Renderer> ();
	}


}
