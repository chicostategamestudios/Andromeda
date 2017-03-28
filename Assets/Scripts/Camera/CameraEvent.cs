using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Components{
	public class CameraEvent : MonoBehaviour {

		public Transform focusLock;
		[Tooltip("0 = Unlock/Default, 1 = Vertical Lock, 2 = Horizontal Lock, 3 = Room Lock, 4 = Tilt Camera")]
		public int cameraEvent;
		[Tooltip("Refer to Camera Readme")]
		public float eventSmooth;
		public float distanceAway;
		public float zoomSmooth;
		public float cameraTilt;

		private float defaultDist;
		private float defaultTilt;

		//private GameObject cameraMain;

		void Start () {
			//cameraMain = GameObject.FindWithTag ("MainCamera");
			defaultDist = _Camera.distanceAway;
			defaultTilt = _Camera.cameraTilt;
		}

		void OnTriggerEnter (Collider col) {
			if (col.tag == "Player") {
				if (cameraEvent == 0) {
					_Camera.cameraEvent = cameraEvent;
					_Camera.eventSmooth = eventSmooth;
					_Camera.distanceAway = defaultDist;
					_Camera.cameraTilt = defaultTilt;

				}
				if (cameraEvent == 1) {
					_Camera.cameraEvent = cameraEvent;
					_Camera.focusLock = focusLock;
					_Camera.eventSmooth = eventSmooth;
					_Camera.distanceAway = defaultDist;
					_Camera.cameraTilt = defaultTilt;
				}
				if (cameraEvent == 2) {
					_Camera.cameraEvent = cameraEvent;
					_Camera.focusLock = focusLock;
					_Camera.eventSmooth = eventSmooth;
					_Camera.distanceAway = defaultDist;
					_Camera.cameraTilt = defaultTilt;
				}
				if (cameraEvent == 3) {
					_Camera.cameraEvent = cameraEvent;
					_Camera.focusLock = focusLock;
					_Camera.eventSmooth = eventSmooth;
					_Camera.distanceAway = distanceAway;
					_Camera.cameraTilt = defaultTilt;
				}
				if (cameraEvent == 4) {
					_Camera.cameraEvent = cameraEvent;
					_Camera.cameraTilt = cameraTilt;
				}
				if (cameraEvent > 4 || cameraEvent < 0) {
					Debug.LogWarning ("Unknown camera event #. Please input a number between 0 and 4");
				}
			}
		}
	}
}
