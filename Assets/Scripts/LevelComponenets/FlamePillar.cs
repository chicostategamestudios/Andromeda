using UnityEngine;
using System.Collections;

public class FlamePillar : MonoBehaviour {

	//Still needs to damage player
	//Needs activation from relic grab

	public bool isActive;
	Renderer beamRenderer;
	ParticleSystem mySystem;
	Collider myCollider;

	// Use this for initialization
	void Start () {
		myCollider = GetComponentInChildren<BoxCollider>();
		beamRenderer = GetComponent<MeshRenderer> ();
		mySystem = GetComponentInChildren<ParticleSystem>();
		isActive = false;
		beamRenderer.enabled = false;
		myCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			StartCoroutine(FireBeam ());
		}
	}

	IEnumerator FireBeam(){
		beamRenderer.enabled = true;
		yield return new WaitForSeconds(2);
		beamRenderer.enabled = false;
		isActive = false;
		yield return new WaitForSeconds(1);
		myCollider.enabled = true;
		mySystem.Play();
		yield return new WaitForSeconds(4);
		myCollider.enabled = false;
	}
		
}
