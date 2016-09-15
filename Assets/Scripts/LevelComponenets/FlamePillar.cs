using UnityEngine;
using System.Collections;

public class FlamePillar : MonoBehaviour {

	public bool isActive;
	Renderer beamRenderer;

	// Use this for initialization
	void Start () {
		isActive = false;
		beamRenderer = GetComponentInChildren<MeshRenderer> ();
		beamRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			FireBeam ();
		}
	}

	void FireBeam(){

	}
}
