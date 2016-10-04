using UnityEngine;
using System.Collections;

public class RollingPillar : MonoBehaviour {
	public Transform StartPoint, endpoint;
	public RollingPillarBehavior[] myChildren;
	public float speed;

	private Vector3 startPosition;
	private Quaternion startRotation;

	void Start(){
		myChildren = transform.gameObject.GetComponentsInChildren<RollingPillarBehavior> ();
		StoreStartPosition (ref startPosition, ref startRotation);
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			for (int childCol = 0; childCol < myChildren.Length; childCol++) {
			//	Debug.Log ("fuck " + childCol);
			//	myChildren [childCol].isKinematic = false;
				myChildren[childCol].StartRolling = true;


			}

		}

	}

	void StoreStartPosition(ref Vector3 startPos, ref Quaternion startRot){

		startPos = this.transform.position;
		startRot = this.transform.rotation;
		
	}

	public void Reset(){

		this.transform.position = startPosition;
		this.transform.rotation = startRotation;
		
	}

}
