using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FancyLevelSelectController : MonoBehaviour {
	public int levelSelectIndex = 0;
	public float timePerRotation = 2.0f;
	public GameObject wheel;
	public GameObject planets;


	private float[] wheelRotValues = new float[6];
	private Vector3[] planetRotValues = new Vector3[6];
	private bool canSwitch = true;
	private bool beginRotation = false;
	private bool switching = false;
	private bool positiveRot = false;
	private bool negativeRot = false;
	private float leftThumbstickX;

	// Use this for initialization
	void Start () {
		InitializePlanetRotValues ();
		InitializeWheelRotValue ();
		Debug.Log (wheelRotValues [0]);
		Debug.Log (planetRotValues [0]);

	}

	public LevelToUnlock GetLevel{
		get {
			LevelToUnlock returnValue = LevelToUnlock.tutorial;

			switch (levelSelectIndex) {
			case 0:
				returnValue = LevelToUnlock.tutorial;
				break;
			//Final Level
			case 1:
				returnValue = LevelToUnlock.finalLevel;
				break;
			case 2:
				returnValue = LevelToUnlock.airLevel;
				break;
			case 3:
				returnValue = LevelToUnlock.earthLevel;
				break;
			case 4:
				returnValue = LevelToUnlock.iceLevel;
				break;
			case 5:
				returnValue = LevelToUnlock.fireLevel;
				break;
			}

			return returnValue;
		}
	}

	// Update is called once per frame
	void Update () {
		leftThumbstickX = Input.GetAxis ("Horizontal");
		UpdateCanSwitch ();
		MoveCursor ();
		EnterLevel ();
	}

	void EnterLevel(){
		if ((Input.GetButton ("Submit") || Input.GetButton ("Start")) && canSwitch) {
			//Enter whatever level
			GetLevelToEnter();
		}
	}

	private void GetLevelToEnter (){

			switch (GetLevel) {
		case LevelToUnlock.tutorial:
				GameManager.LoadLevel (GetLevel);
				SceneManager.LoadScene (SceneRef.getTutorial);
				break;
			//Final Level
			case LevelToUnlock.airLevel:
				GameManager.LoadLevel (GetLevel);
				SceneManager.LoadScene (SceneRef.getWindLevel);
				break;
			case LevelToUnlock.earthLevel:
				GameManager.LoadLevel (GetLevel);
				SceneManager.LoadScene (SceneRef.getEarthLevel);
				break;
			case LevelToUnlock.iceLevel:
				GameManager.LoadLevel (GetLevel);
				SceneManager.LoadScene (SceneRef.getIcelevel);
				break;
			case LevelToUnlock.fireLevel:
				GameManager.LoadLevel (GetLevel);
				SceneManager.LoadScene (SceneRef.getFireLevel);
				break;
			case LevelToUnlock.finalLevel:
				GameManager.LoadLevel(GetLevel);
				SceneManager.LoadScene(SceneRef.getFinalLevel);
				break;
		}
			
	}

	private void InitializeWheelRotValue(){
		wheelRotValues [0] = 60.0f; 
		wheelRotValues [1] = 0.0f; 
		wheelRotValues [2] = 300.0f; 
		wheelRotValues [3] = 240.0f; 
		wheelRotValues [4] = 180.0f; 
		wheelRotValues [5] = 120.0f; 

	}

	private void InitializePlanetRotValues(){
		planetRotValues [0] = new Vector3 (353.6698f, 27.45307f, 331.2433f);
		planetRotValues [1] = new Vector3 (353.6698f, 27.45307f, 331.2433f);
		planetRotValues [2] = new Vector3 (25.15189f, 95.29855f, 344.2794f);
		planetRotValues [3] = new Vector3 (21.23976f, 175.1716f, 20.80001f);
		planetRotValues [4] = new Vector3 (347.7924f, 241.4051f, 26.9397f);
		planetRotValues [5] = new Vector3 (330.7716f, 315.3304f, 356.8074f);
	}

	private void MoveCursor(){
		if (leftThumbstickX > 0.2f && canSwitch) {
			beginRotation = true;
			positiveRot = true;
			canSwitch = false;
		}

		if (leftThumbstickX < -0.2f && canSwitch) {
			beginRotation = true;
			negativeRot = true;
			canSwitch = false;
		}

		if (beginRotation) {
			if (positiveRot) {
				if (levelSelectIndex < 5) {
					Debug.Log ("I'm not 5 so I will rotate towards index+1");
					RotateWheel (wheel.transform.rotation.eulerAngles, wheelRotValues [levelSelectIndex + 1]);
					RotatePlanets (planets.transform.localRotation.eulerAngles, planetRotValues [levelSelectIndex + 1]);

				} else if (levelSelectIndex == 5) {
					Debug.Log ("I am 5 so I will rotate towards 0");
					RotateWheel (wheel.transform.rotation.eulerAngles, wheelRotValues [0]);
					RotatePlanets (planets.transform.localRotation.eulerAngles, planetRotValues [0]);
				}
				switching = true;
			}

			if (negativeRot) {
				if (levelSelectIndex > 0) {
					Debug.Log ("I'm not 5 so I will rotate towards index-1");
					RotateWheel (wheel.transform.rotation.eulerAngles, wheelRotValues [levelSelectIndex - 1]);
					RotatePlanets (planets.transform.localRotation.eulerAngles, planetRotValues [levelSelectIndex - 1]);

				} else if (levelSelectIndex == 0) {
					Debug.Log ("I am 5 so I will rotate towards 5");
					RotateWheel (wheel.transform.rotation.eulerAngles, wheelRotValues [5]);
					RotatePlanets (planets.transform.localRotation.eulerAngles, planetRotValues [5]);
				}
				switching = true;
			}
		}
	}

	private void RotateWheel (Vector3 fromRot, float yRot){
		wheel.transform.rotation = Quaternion.Slerp (Quaternion.Euler (fromRot), Quaternion.Euler (fromRot.x, yRot, fromRot.z), timePerRotation * Time.deltaTime);
	}

	private void RotatePlanets (Vector3 fromRot, Vector3 toRot){
		planets.transform.localRotation = Quaternion.Slerp (Quaternion.Euler (fromRot), Quaternion.Euler (toRot), timePerRotation * Time.deltaTime);
	}

	private void UpdateCanSwitch(){
		if (switching) {
			float wheelDegreeDiff = 0;
			if (positiveRot) {
				if (levelSelectIndex < 5) {
					wheelDegreeDiff = wheel.transform.eulerAngles.y - wheelRotValues [levelSelectIndex + 1];
				} else {
					wheelDegreeDiff = wheel.transform.eulerAngles.y - wheelRotValues [0];
				}

				if (Mathf.Abs(wheelDegreeDiff) <= 1.0f) {
					beginRotation = false;
					canSwitch = true;
					switching = false;
					positiveRot = false;
					Debug.Log ("I've UpdatedCanSwith()");
					if (levelSelectIndex != 5) {
						levelSelectIndex++;
					} else {
						levelSelectIndex = 0;
					}

				}
			}
			if (negativeRot) {
				if (levelSelectIndex > 0) {
					wheelDegreeDiff = wheel.transform.eulerAngles.y - wheelRotValues [levelSelectIndex - 1];
				} else {
					wheelDegreeDiff = wheel.transform.eulerAngles.y - wheelRotValues [5];
				}

				if (Mathf.Abs(wheelDegreeDiff) <= 1.0f) {
					beginRotation = false;
					canSwitch = true;
					switching = false;
					negativeRot = false;
					Debug.Log ("I've UpdatedCanSwith()");
					if (levelSelectIndex != 0) {
						levelSelectIndex--;
					} else {
						levelSelectIndex = 5;
					}

				}
			}
		}
	}
}
