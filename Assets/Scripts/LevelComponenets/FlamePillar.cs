using UnityEngine;
using System.Collections;

public class FlamePillar : MonoBehaviour {

	//Still needs to damage player
	//Needs activation from relic grab

	public bool isActive;
	Renderer beamRenderer;
	ParticleSystem mySystem;
	Collider myCollider;
    CapsuleCollider thisCollider;
    public float beamDuration;
    public float dealyOfExplosion;
    public float explosionDuration;

	// Use this for initialization
	void Start () {
		myCollider = GetComponentInChildren<BoxCollider>();
		beamRenderer = GetComponent<MeshRenderer> ();
		mySystem = GetComponentInChildren<ParticleSystem>();
        thisCollider = GetComponent<CapsuleCollider>();
		isActive = false;
		beamRenderer.enabled = false;
		myCollider.enabled = false;
        thisCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			StartCoroutine(FireBeam ());
		}
	}

	IEnumerator FireBeam(){
		beamRenderer.enabled = true;
        thisCollider.enabled = true;
		yield return new WaitForSeconds(beamDuration);
		beamRenderer.enabled = false;
        thisCollider.enabled = false;   
		isActive = false;
		yield return new WaitForSeconds(dealyOfExplosion);
		myCollider.enabled = true;
		mySystem.Play();
		yield return new WaitForSeconds(explosionDuration);
        mySystem.Stop();
		myCollider.enabled = false;
	}
		
}
