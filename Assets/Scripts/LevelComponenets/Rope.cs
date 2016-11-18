using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {

	public Transform targetPos;
	
	// Update is called once per frame
	void Update () {

	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.gray;
		Gizmos.DrawLine(gameObject.transform.position, targetPos.position);
	}
}
