using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Components{
	public class CameraEvent : MonoBehaviour {

		public Transform focusLock;
		public int cameraEvent;
		public float eventSmooth;
		public float distanceAway;
		public float zoomSmooth;

		private float defaultDist;

		//private GameObject cameraMain;

		void Start () {
			//cameraMain = GameObject.FindWithTag ("MainCamera");
			defaultDist = _Camera.distanceAway;
		}

		void OnTriggerEnter (Collider col) {
			if (col.tag == "Player") {
				if (cameraEvent == 0) {
					_Camera.cameraEvent = cameraEvent;
					_Camera.eventSmooth = eventSmooth;
					_Camera.distanceAway = defaultDist;
				}
				if (cameraEvent == 1) {
					_Camera.cameraEvent = cameraEvent;
					_Camera.focusLock = focusLock;
					_Camera.eventSmooth = eventSmooth;
					_Camera.distanceAway = defaultDist;
				}
				if (cameraEvent == 2) {
					_Camera.cameraEvent = cameraEvent;
					_Camera.focusLock = focusLock;
					_Camera.eventSmooth = eventSmooth;
					_Camera.distanceAway = defaultDist;
				}
				if (cameraEvent == 3) {
					_Camera.cameraEvent = cameraEvent;
					_Camera.focusLock = focusLock;
					_Camera.eventSmooth = eventSmooth;
					_Camera.distanceAway = distanceAway;
				}
				if (cameraEvent > 3 || cameraEvent < 0) {
					Debug.LogWarning ("Unknown camera event #. Please input a number between 0 and 3");
				}
			}
		}
	}
}
