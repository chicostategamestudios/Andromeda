// This script was written by Josh Deal | Last Edited by Josh Deal | Modified on 23 February 2017

using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

namespace Assets.Scripts.Components
{
	public class _Camera : MonoBehaviour
	{

		static public Transform focusLock;
		static public int cameraEvent;
		static public float eventSmooth;
		static public float distanceAway = 35f; // Distance from camera to player in Z-axis
		static public float cameraTilt;
		private float lookAheadDistanceY = 0f; // Distance camera will "look" ahead of player's y
        private float lookSmoothTimeY = 0f; // How fast camera will look up of down if player moves in those directions
        private float maxSnapDist = 20f; // Distance between camera and player before camera snaps to location

		private Vector2 focusAreaSize = new Vector2(2, 2); // size of the focus box

		private float verticalOffset = -1f; // Distance camera is offset from player's y
		private float lookAheadDistanceX = 10f; // Distance camera will "look" ahead of player's x
        private float lookSmoothTimeX = 0.8f; // time it takes camera to transform position in x-axis
		private float verticalSmoothTime = .02f; // time it takes camera to transform position in y-axis

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
		void Start()
		{
			focusArea = new FocusArea(CharController.Instance.transform.GetComponent<CharacterController>().bounds, focusAreaSize);
            cameraEvent = 0;
			playerFocused = true;
			targetFocused = false;
		}

		void LateUpdate()
		{
            Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;
            // This makes it so that if the camera is further than the max snap distance the camera will instantly teleport there
            if (Vector2.Distance(transform.position, focusPosition) > maxSnapDist)
            {
                transform.position = focusPosition;
            }
            // otherwise it does normal camera functions
            else
            {
                CameraEvents();
            }
        }

		//Draws a visible box of the Area for camera movement
		/*void OnDrawGizmos()
		{
			Gizmos.color = new Color(1, 0, 0, .5f);
			Gizmos.DrawCube(focusArea.center, focusAreaSize);
		}*/

		public void CameraEvents()
		{
			//Unlock/Normal Camera
			if (cameraEvent == 0)
			{
			//	Debug.Log(CharController.Instance.transform.GetComponent<CharacterController>().bounds);
				focusArea.Update(CharController.Instance.transform.GetComponent<CharacterController>().bounds);
				//sets the vertical offset of the camera relative to player
				Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;
				currentDistanceAway = distanceAway;

				//Checks the look ahead depending on focus velocity
				//if the focusArea is moving in x, continue looking ahead
				if (focusArea.velocity.x != 0)
				{
					lookAheadDirectionX = Mathf.Sign(focusArea.velocity.x);
					if ((Mathf.Sign(PlayerMovement.moveVector.x) == Mathf.Sign(focusArea.velocity.x)) && PlayerMovement.moveVector.x != 0)
					{
						lookAheadStopped = false;
						targetLookAheadX = lookAheadDirectionX * lookAheadDistanceX;
					}
				}
				//if the focusArea is not moving in x, ease into movement
				else
				{
					if (!lookAheadStopped)
					{
						lookAheadStopped = true;
						targetLookAheadX = currentLookAheadX + (lookAheadDirectionX * lookAheadDistanceX - currentLookAheadX) / 4;
					}
				}

				//sets the look ahead depending on direction moving and distance set
				currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

				focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
				focusPosition += Vector2.right * currentLookAheadX; //Executes LookAhead

				if (!playerFocused)
				{
					targetPosition = (Vector3)focusPosition + Vector3.forward * -distanceAway; //Transforms Camera position
					Vector3 smoothVel = Vector3.zero;
					transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVel, eventSmooth);
					checkDistance = Vector3.Distance(transform.position, targetPosition);
					//Debug.Log (checkDistance);
					if (checkDistance < 0.1f)
					{
						playerFocused = true;
						targetFocused = false;
					}
				}
				else
				{
					transform.position = (Vector3)focusPosition + Vector3.forward * -distanceAway;
					transform.eulerAngles = new Vector3 (cameraTilt, 0, 0);
				}
			}

			//Vertical Lock
			if (cameraEvent == 1)
			{
				playerFocused = false;
				focusArea.Update(CharController.Instance.transform.GetComponent<CharacterController>().bounds);
				//sets the vertical offset of the camera relative to player
				Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;


				//Checks the look ahead depending on focus velocity
				//if the focusArea is moving in x, continue looking ahead
				if (focusArea.velocity.x != 0)
				{
					lookAheadDirectionX = Mathf.Sign(focusArea.velocity.x);
					if ((Mathf.Sign(PlayerMovement.moveVector.x) == Mathf.Sign(focusArea.velocity.x)) && PlayerMovement.moveVector.x != 0)
					{
						lookAheadStopped = false;
						targetLookAheadX = lookAheadDirectionX * lookAheadDistanceX;
					}
				}
				//if the focusArea is not moving in x, ease into movement
				else
				{
					if (!lookAheadStopped)
					{
						lookAheadStopped = true;
						targetLookAheadX = currentLookAheadX + (lookAheadDirectionX * lookAheadDistanceX - currentLookAheadX) / 4;
					}
				}

				//sets the look ahead depending on direction moving and distance set
				currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);
				focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusLock.position.y, ref smoothVelocityY, verticalSmoothTime);
				focusPosition += Vector2.right * currentLookAheadX; //Executes LookAhead
				if (!targetFocused)
				{
					targetPosition = (Vector3)focusPosition + Vector3.forward * -distanceAway; //Transforms Camera position
					Vector3 smoothVel = Vector3.zero;
					transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVel, eventSmooth);
					Vector3 difference = transform.position - targetPosition;
					float checkDistancePoint = Mathf.Abs(difference.y);
					if (checkDistancePoint < .01f)
					{
						targetFocused = true;
					}
				}
				else
				{
					transform.position = (Vector3)focusPosition + Vector3.forward * -distanceAway;
					transform.eulerAngles = new Vector3 (cameraTilt, 0, 0);				}
			}

			//Horizontal Lock
			if (cameraEvent == 2)
			{
				playerFocused = false;
				focusArea.Update(CharController.Instance.transform.GetComponent<CharacterController>().bounds);
				//sets the vertical offset of the camera relative to player
				Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;


				//Checks the look ahead depending on focus velocity
				//if the focusArea is moving in x, continue looking ahead
				if (focusArea.velocity.y != 0)
				{
					lookAheadDirectionY = Mathf.Sign(focusArea.velocity.y);
					if ((Mathf.Sign(PlayerMovement.moveVector.y) == Mathf.Sign(focusArea.velocity.y)) && PlayerMovement.moveVector.y != 0)
					{
						lookAheadStopped = false;
						targetLookAheadY = lookAheadDirectionY * lookAheadDistanceY;
					}
				}
				//if the focusArea is not moving in x, ease into movement
				else
				{
					if (!lookAheadStopped)
					{
						lookAheadStopped = true;
						targetLookAheadY = currentLookAheadY + (lookAheadDirectionY * lookAheadDistanceY - currentLookAheadY) / 4;
					}
				}

				//sets the look ahead depending on direction moving and distance set
				currentLookAheadY = Mathf.SmoothDamp(currentLookAheadY, targetLookAheadY, ref smoothLookVelocityY, lookSmoothTimeY);
				focusPosition.x = Mathf.SmoothDamp(transform.position.x, focusLock.position.x, ref smoothVelocityY, verticalSmoothTime);
				focusPosition += Vector2.up * currentLookAheadY; //Executes LookAhead
				if (!targetFocused)
				{
					targetPosition = (Vector3)focusPosition + Vector3.forward * -distanceAway; //Transforms Camera position
					Vector3 smoothVel = Vector3.zero;
					transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVel, eventSmooth);
					Vector3 difference = transform.position - targetPosition;
					float checkDistancePoint = Mathf.Abs(difference.x);
					if (checkDistancePoint < .01f)
					{
						targetFocused = true;
					}
				}
				else
				{
					transform.position = (Vector3)focusPosition + Vector3.forward * -distanceAway;
					transform.eulerAngles = new Vector3 (cameraTilt, 0, 0);
				}
			}

			//Room Lock
			if (cameraEvent == 3)
			{
				playerFocused = false;
				focusArea.Update(CharController.Instance.transform.GetComponent<CharacterController>().bounds);
				//sets the vertical offset of the camera relative to player
				Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;


				//Checks the look ahead depending on focus velocity
				//if the focusArea is moving in x, continue looking ahead
				if (focusArea.velocity.x != 0)
				{
					lookAheadDirectionX = Mathf.Sign(focusArea.velocity.x);
					if ((Mathf.Sign(PlayerMovement.moveVector.x) == Mathf.Sign(focusArea.velocity.x)) && PlayerMovement.moveVector.x != 0)
					{
						lookAheadStopped = false;
						targetLookAheadX = lookAheadDirectionX * lookAheadDistanceX;
					}
				}
				//if the focusArea is not moving in x, ease into movement
				else
				{
					if (!lookAheadStopped)
					{
						lookAheadStopped = true;
						targetLookAheadX = currentLookAheadX + (lookAheadDirectionX * lookAheadDistanceX - currentLookAheadX) / 4;
					}
				}

				//sets the look ahead depending on direction moving and distance set
				currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

				focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusLock.position.y, ref smoothVelocityY, verticalSmoothTime);
				focusPosition.x = Mathf.SmoothDamp(transform.position.x, focusLock.position.x, ref smoothVelocityY, verticalSmoothTime);
				targetPosition = (Vector3)focusPosition + Vector3.forward * -distanceAway; //Transforms Camera position
				Vector3 smoothVel = Vector3.zero;
				transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVel, eventSmooth);
				transform.eulerAngles = new Vector3 (cameraTilt, 0, 0);			}

			//Tilt Camera
			if (cameraEvent == 4)
			{
				//	Debug.Log(CharController.Instance.transform.GetComponent<CharacterController>().bounds);
				focusArea.Update(CharController.Instance.transform.GetComponent<CharacterController>().bounds);
				//sets the vertical offset of the camera relative to player
				Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;
				currentDistanceAway = distanceAway;

				//Checks the look ahead depending on focus velocity
				//if the focusArea is moving in x, continue looking ahead
				if (focusArea.velocity.x != 0)
				{
					lookAheadDirectionX = Mathf.Sign(focusArea.velocity.x);
					if ((Mathf.Sign(PlayerMovement.moveVector.x) == Mathf.Sign(focusArea.velocity.x)) && PlayerMovement.moveVector.x != 0)
					{
						lookAheadStopped = false;
						targetLookAheadX = lookAheadDirectionX * lookAheadDistanceX;
					}
				}
				//if the focusArea is not moving in x, ease into movement
				else
				{
					if (!lookAheadStopped)
					{
						lookAheadStopped = true;
						targetLookAheadX = currentLookAheadX + (lookAheadDirectionX * lookAheadDistanceX - currentLookAheadX) / 4;
					}
				}

				//sets the look ahead depending on direction moving and distance set
				currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

				focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
				focusPosition += Vector2.right * currentLookAheadX; //Executes LookAhead

				if (!playerFocused)
				{
					targetPosition = (Vector3)focusPosition + Vector3.forward * -distanceAway; //Transforms Camera position
					Vector3 smoothVel = Vector3.zero;
					transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVel, eventSmooth);
					checkDistance = Vector3.Distance(transform.position, targetPosition);
					//Debug.Log (checkDistance);
					if (checkDistance < 0.1f)
					{
						playerFocused = true;
						targetFocused = false;
					}
				}
				else
				{
					transform.position = (Vector3)focusPosition + Vector3.forward * -distanceAway;
					transform.eulerAngles = new Vector3 (cameraTilt, 0, 0);				}
			}
		}

		//This is the area around the target that the camera is focusing on
		public struct FocusArea
		{
			public Vector2 center;
			public Vector2 velocity;
			public float left, right;
			public float top, bottom;

			//Area of the focus around target
			public FocusArea(Bounds targetBounds, Vector2 size)
			{
				left = targetBounds.center.x - size.x / 2;
				right = targetBounds.center.x + size.x / 2;
				bottom = targetBounds.min.y;
				top = targetBounds.min.y + size.y;

				velocity = Vector2.zero;
				center = new Vector2((left + right) / 2, (top + bottom) / 2);
			}

			//Moves the box as the target comes in contact with it
			public void Update(Bounds targetBounds)
			{
				//calculates change in position when contacted in X axis
				float shiftX = 0;
				if (targetBounds.min.x < left)
				{
					shiftX = targetBounds.min.x - left;
				}
				else if (targetBounds.max.x > right)
				{
					shiftX = targetBounds.max.x - right;
				}
				left += shiftX;
				right += shiftX;

				//calculates change in position when contacted in Y axis
				float shiftY = 0;
				if (targetBounds.min.y < bottom)
				{
					shiftY = targetBounds.min.y - bottom;
				}
				else if (targetBounds.max.y > top)
				{
					shiftY = targetBounds.max.y - top;
				}
				top += shiftY;
				bottom += shiftY;

				//sets new position of center after collision with target has occured
				center = new Vector2((left + right) / 2, (top + bottom) / 2);
				velocity = new Vector2(shiftX, shiftY);
			}
		}
	}
}