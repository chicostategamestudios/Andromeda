using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Components{
	public class CameraEvent : MonoBehaviour {

		public Transform focusLock;
		public int cameraEvent;
		public float eventSmooth;

		//private GameObject cameraMain;

		void Awake () {
			//cameraMain = GameObject.FindWithTag ("MainCamera");
		}

		void OnTriggerEnter (Collider col) {
			if (col.tag == "Player") {
				if (cameraEvent == 0) {
					_Camera.cameraEvent = cameraEvent;
					_Camera.eventSmooth = eventSmooth;
				}
				if (cameraEvent > 0 && cameraEvent < 4) {
					_Camera.cameraEvent = cameraEvent;
					_Camera.focusLock = focusLock;
					_Camera.eventSmooth = eventSmooth;
				}
				if (cameraEvent > 3 || cameraEvent < 0) {
					Debug.LogWarning ("Unknown camera event #. Please input a number between 0 and 3");
				}
			}
		}
	}
}
