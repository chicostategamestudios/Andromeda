using UnityEngine;
using System.Collections;

public class PlanetIndividualRotation : MonoBehaviour {

	[SerializeField]
	private float planetRotationSpeed = 4.0f;
	[SerializeField]
	private bool useLocal = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (useLocal) {
			transform.localRotation *= Quaternion.Euler (new Vector3 (0, planetRotationSpeed * Time.deltaTime, 0));
		} else {
			transform.eulerAngles += new Vector3 (0, planetRotationSpeed * Time.deltaTime, 0);
		}
	
	}
}
