using UnityEngine;
using System.Collections;

public class RotatingWall : MonoBehaviour {
	[Tooltip("rotation speed, negative value rotates the other way")]
	public float rotate_speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate( new Vector3(0,0,1) * rotate_speed * Time.deltaTime);
	}
}
