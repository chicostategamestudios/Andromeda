using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

namespace Assets.Scripts.Components {
	public class _CameraOri: MonoBehaviour {

		static public Transform focusLock;
		static public int cameraEvent;
		static public float eventSmooth;

		public Vector2 focusAreaSize;

		public float verticalOffset;
		public float lookAheadDistanceX;
		public float lookSmoothTimeX;
		public float verticalSmoothTime;

		private float currentLookAheadX;
		private float targetLookAheadX;
		private float lookAheadDirectionX;
		private float smoothLookVelocityX;
		private float smoothVelocityY;
		private bool lookAheadStopped;
		private bool playerFocused;

		FocusArea focusArea;

		// Use this for initialization
		void Start () {
			focusArea = new FocusArea (CharController.Instance.transform.GetComponent<CharacterController>().bounds, focusAreaSize);
			cameraEvent = 0;
			playerFocused = true;
		}

		void LateUpdate () {
			CameraEvents ();
			Debug.Log (cameraEvent);
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
			if (cameraEvent == 0) {
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

				focusPosition.y = Mathf.SmoothDamp (transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
				focusPosition += Vector2.right * currentLookAheadX; //Executes LookAhead
				if (!playerFocused) {
					Vector3 targetPosition = (Vector3)focusPosition + Vector3.forward * -10; //Transforms Camera position
					Vector3 smoothVel = Vector3.zero;
					transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref smoothVel, eventSmooth);
					float checkDistance = Vector3.Distance (transform.position, targetPosition);
					Debug.Log (checkDistance);
					if (checkDistance < 0.2f) {
						playerFocused = true;
					}
				}  
				else {
					transform.position = (Vector3)focusPosition + Vector3.forward * -10;
				}
			}

			if (cameraEvent == 1) {
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
				focusPosition += Vector2.right * currentLookAheadX; //Executes LookAhead
				transform.position = (Vector3)focusPosition + Vector3.forward * -10; //Transforms Camera position
			}

			if (cameraEvent == 2) {
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

				focusPosition.y = Mathf.SmoothDamp (transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
				focusPosition.x = Mathf.SmoothDamp (transform.position.x, focusLock.position.x, ref smoothVelocityY, verticalSmoothTime);
				//focusPosition += Vector2.up * currentLookAheadX/2; //Executes LookAhead
				transform.position = (Vector3)focusPosition + Vector3.forward * -10; //Transforms Camera position
			}

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
				transform.position = (Vector3)focusPosition + Vector3.forward * -10; //Transforms Camera position
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