using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

namespace Assets.Scripts.Components {
	public class _Camera: MonoBehaviour {

		static public Transform focusLock;
		static public int cameraEvent;
		static public float eventSmooth;
		static public float distanceAway = 35f;
		public float lookAheadDistanceY;
		public float lookSmoothTimeY;

		[HideInInspector]public Vector2 focusAreaSize = new Vector2(1,2);

		[HideInInspector]
		public float verticalOffset = -1;
		public float lookAheadDistanceX;
		public float lookSmoothTimeX = .8f;
		public float verticalSmoothTime = .02f;
		public float zoomDistance = 20;

		private float currentLookAheadX;
		private float targetLookAheadX;
		private float lookAheadDirectionX;
		private float smoothLookVelocityX;
		private float currentLookAheadY;
		private float targetLookAheadY;
		private float lookAheadDirectionY;
		private float smoothVelocityY;
		private float smoothLookVelocityY;
		private float checkDistance;
		private float currentDistanceAway;
		private bool lookAheadStopped;
		private bool targetFocused;
		private bool playerFocused;
		private Vector3 targetPosition;

		FocusArea focusArea;

		// Use this for initialization
		void Awake () {
			focusArea = new FocusArea (CharController.Instance.transform.GetComponent<CharacterController>().bounds, focusAreaSize);
			cameraEvent = 0;
			playerFocused = true;
			targetFocused = false;
			//distanceAway = zoomDistance;
		}

		void LateUpdate () {
			CameraEvents ();
			//Debug.Log (cameraEvent);
			//Debug.Log (targetFocused);
		}

		//Draws a visible box of the Area for camera movement
		void OnDrawGizmos() {
			Gizmos.color = new Color (1, 0, 0, .5f);
			Gizmos.DrawCube (focusArea.center, focusAreaSize);
		}

		// Update is called once per frame
		void Update () {
		}


		public void CameraEvents () {
			//Unlock/Normal Camera
			if (cameraEvent == 0) {
				focusArea.Update (CharController.Instance.transform.GetComponent<CharacterController>().bounds);
				//sets the vertical offset of the camera relative to player
				Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;
				currentDistanceAway = distanceAway;

				//Checks the look ahead depending on focus velocity
				//if the focusArea is moving in x, continue looking ahead
				if (focusArea.velocity.x != 0) {
					lookAheadDirectionX = Mathf.Sign (focusArea.velocity.x);
					if ((Mathf.Sign (PlayerMovement.moveVector.x) == Mathf.Sign (focusArea.velocity.x)) && PlayerMovement.moveVector.x != 0) {
						lookAheadStopped = false;
						targetLookAheadX = lookAheadDirectionX * lookAheadDistanceX;
					}  
				}
				//if the focusArea is not moving in x, ease into movement
				else {
					if (!lookAheadStopped) {
						lookAheadStopped = true;
						targetLookAheadX = currentLookAheadX + (lookAheadDirectionX * lookAheadDistanceX - currentLookAheadX) / 4;
					}
				}

				//sets the look ahead depending on direction moving and distance set
				currentLookAheadX = Mathf.SmoothDamp (currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

				focusPosition.y = Mathf.SmoothDamp (transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
				focusPosition += Vector2.right * currentLookAheadX; //Executes LookAhead

				if (!playerFocused) {
					targetPosition = (Vector3)focusPosition + Vector3.forward * -distanceAway; //Transforms Camera position
					Vector3 smoothVel = Vector3.zero;
					transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref smoothVel, eventSmooth);
					checkDistance = Vector3.Distance (transform.position, targetPosition);
					//Debug.Log (checkDistance);
					if (checkDistance < 0.1f) {
						playerFocused = true;
						targetFocused = false;
					}
				}  
				else {
					transform.position = (Vector3)focusPosition + Vector3.forward * -distanceAway;
				}
			}

			//Vertical Lock
			if (cameraEvent == 1) {
				playerFocused = false;
				focusArea.Update (CharController.Instance.transform.GetComponent<CharacterController> ().bounds);
				//sets the vertical offset of the camera relative to player
				Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;


				//Checks the look ahead depending on focus velocity
				//if the focusArea is moving in x, continue looking ahead
				if (focusArea.velocity.x != 0) {
					lookAheadDirectionX = Mathf.Sign (focusArea.velocity.x);
					if ((Mathf.Sign (PlayerMovement.moveVector.x) == Mathf.Sign (focusArea.velocity.x)) && PlayerMovement.moveVector.x != 0) {
						lookAheadStopped = false;
						targetLookAheadX = lookAheadDirectionX * lookAheadDistanceX;
					}  
				}
				//if the focusArea is not moving in x, ease into movement
				else {
					if (!lookAheadStopped) {
						lookAheadStopped = true;
						targetLookAheadX = currentLookAheadX + (lookAheadDirectionX * lookAheadDistanceX - currentLookAheadX) / 4;
					}
				}

				//sets the look ahead depending on direction moving and distance set
				currentLookAheadX = Mathf.SmoothDamp (currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);
				focusPosition.y = Mathf.SmoothDamp (transform.position.y, focusLock.position.y, ref smoothVelocityY, verticalSmoothTime);
				focusPosition += Vector2.right * currentLookAheadX; //Executes LookAhead
				if (!targetFocused) {
					targetPosition = (Vector3)focusPosition + Vector3.forward * -distanceAway; //Transforms Camera position
					Vector3 smoothVel = Vector3.zero;
					transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref smoothVel, eventSmooth);
					Vector3 difference = transform.position - targetPosition;
					float checkDistancePoint = Mathf.Abs (difference.y);
					if (checkDistancePoint < .01f) {
						targetFocused = true;
					}
				}  
				else {
					transform.position = (Vector3)focusPosition + Vector3.forward * -distanceAway;
				}
			}

			//Horizontal Lock
			if (cameraEvent == 2) {
				playerFocused = false;
				focusArea.Update (CharController.Instance.transform.GetComponent<CharacterController>().bounds);
				//sets the vertical offset of the camera relative to player
				Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;


				//Checks the look ahead depending on focus velocity
				//if the focusArea is moving in x, continue looking ahead
				if (focusArea.velocity.y != 0) {
					lookAheadDirectionY = Mathf.Sign (focusArea.velocity.y);
					if ((Mathf.Sign (PlayerMovement.moveVector.y) == Mathf.Sign (focusArea.velocity.y)) && PlayerMovement.moveVector.y != 0) {
						lookAheadStopped = false;
						targetLookAheadY = lookAheadDirectionY * lookAheadDistanceY;
					}  
				}
				//if the focusArea is not moving in x, ease into movement
				else {
					if (!lookAheadStopped) {
						lookAheadStopped = true;
						targetLookAheadY = currentLookAheadY + (lookAheadDirectionY * lookAheadDistanceY - currentLookAheadY) / 4;
					}
				}

				//sets the look ahead depending on direction moving and distance set
				currentLookAheadY = Mathf.SmoothDamp (currentLookAheadY, targetLookAheadY, ref smoothLookVelocityY, lookSmoothTimeY);				
				focusPosition.x = Mathf.SmoothDamp (transform.position.x, focusLock.position.x, ref smoothVelocityY, verticalSmoothTime);
				focusPosition += Vector2.up * currentLookAheadY; //Executes LookAhead
				if (!targetFocused) {
					targetPosition = (Vector3)focusPosition + Vector3.forward * -distanceAway; //Transforms Camera position
					Vector3 smoothVel = Vector3.zero;
					transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref smoothVel, eventSmooth);
					Vector3 difference = transform.position - targetPosition;
					float checkDistancePoint = Mathf.Abs (difference.x);
					if (checkDistancePoint < .01f) {
						targetFocused = true;
					}
				}   
				else {
					transform.position = (Vector3)focusPosition + Vector3.forward * -distanceAway;
				}
			}

			//Room Lock
			if (cameraEvent == 3) {
				playerFocused = false;
				focusArea.Update (CharController.Instance.transform.GetComponent<CharacterController>().bounds);
				//sets the vertical offset of the camera relative to player
				Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;


				//Checks the look ahead depending on focus velocity
				//if the focusArea is moving in x, continue looking ahead
				if (focusArea.velocity.x != 0) {
					lookAheadDirectionX = Mathf.Sign (focusArea.velocity.x);
					if ((Mathf.Sign (PlayerMovement.moveVector.x) == Mathf.Sign (focusArea.velocity.x)) && PlayerMovement.moveVector.x != 0) {
						lookAheadStopped = false;
						targetLookAheadX = lookAheadDirectionX * lookAheadDistanceX;
					}  
				}
				//if the focusArea is not moving in x, ease into movement
				else {
					if (!lookAheadStopped) {
						lookAheadStopped = true;
						targetLookAheadX = currentLookAheadX + (lookAheadDirectionX * lookAheadDistanceX - currentLookAheadX) / 4;
					}
				}

				//sets the look ahead depending on direction moving and distance set
				currentLookAheadX = Mathf.SmoothDamp (currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

				focusPosition.y = Mathf.SmoothDamp (transform.position.y, focusLock.position.y, ref smoothVelocityY, verticalSmoothTime);
				focusPosition.x = Mathf.SmoothDamp (transform.position.x, focusLock.position.x, ref smoothVelocityY, verticalSmoothTime);
				targetPosition = (Vector3)focusPosition + Vector3.forward * -distanceAway; //Transforms Camera position
				Vector3 smoothVel = Vector3.zero;
				transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref smoothVel, eventSmooth);
			}
		}

		//This is the area around the target that the camera is focusing on
		public struct FocusArea {
			public Vector2 center;
			public Vector2 velocity;
			public float left, right;
			public float top, bottom;

			//Area of the focus around target
			public FocusArea(Bounds targetBounds, Vector2 size) {
				left = targetBounds.center.x - size.x/2;
				right = targetBounds.center.x + size.x/2;
				bottom = targetBounds.min.y;
				top = targetBounds.min.y + size.y;

				velocity = Vector2.zero;
				center = new Vector2((left + right)/2, (top + bottom)/2);
			}

			//Moves the box as the target comes in contact with it
			public void Update(Bounds targetBounds) {
				//calculates change in position when contacted in X axis
				float shiftX = 0;
				if (targetBounds.min.x < left) {
					shiftX = targetBounds.min.x - left;
				}
				else if (targetBounds.max.x > right) {
					shiftX = targetBounds.max.x - right;
				}
				left += shiftX;
				right += shiftX;

				//calculates change in position when contacted in Y axis
				float shiftY = 0;
				if (targetBounds.min.y < bottom) {
					shiftY = targetBounds.min.y - bottom;
				}
				else if (targetBounds.max.y > top) {
					shiftY = targetBounds.max.y - top;
				}
				top += shiftY;
				bottom += shiftY;

				//sets new position of center after collision with target has occured
				center = new Vector2 ((left + right) / 2, (top + bottom) / 2);
				velocity = new Vector2 (shiftX, shiftY);
			}
		}
	}
}
