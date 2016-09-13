using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	public bool canMoveCursor;
	public float thumbstickCursorThreshold, verticalThumb;

	private GameObject mainMenuCanvas, splashMenu, mainMenu, gameSelectMenu,
	levelSelectMenu, optionsMenu, creditsMenu, statsMenu, exitMenu, 
	mainMenuCursor, gameSelectCursor, exitMenuCursor, currentCursor;

	private bool onSplashScreen; 

	private string  mainMenuCursorLocation, gameSelectCursorLocation,
	exitMenuCursorLocation;

	void Start () {
		onSplashScreen = true;
		canMoveCursor = true;
		GetAllMainMenuObjects ();
	
	}

	void Update () {
		EnterMainMenu ();
		//Seting the current cursors position if not on splash screen
		if (!onSplashScreen) {
			currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex = 
			VerticalCursorMovement (thumbstickCursorThreshold, currentCursor,
				currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex, 
				currentCursor.GetComponent<CursorIndexTracker> ().cursorIndexMax);
			verticalThumb = Input.GetAxis ("Vertical");
			MainMenuSelect ();
		}
	}

	//Finds all of the Main Menu Objects by going
	//through each child object in the Main Menu Canvas
	//and sets ech one to its appropriate object
	void GetAllMainMenuObjects(){
		if (GameObject.Find ("Main_Menu_Canvas")) {
			mainMenuCanvas = GameObject.Find ("Main_Menu_Canvas");
			foreach (RectTransform child in mainMenuCanvas.GetComponent<RectTransform>()) {
				if (child.gameObject.name == ("Splash_Screen_Image")) {
					splashMenu = child.gameObject;
					splashMenu.SetActive (true);
				}
				if (child.gameObject.name == ("Main_Menu_Image")) {
					mainMenu = child.gameObject;
				}
				if (child.gameObject.name == ("Main_Menu_Cursor")) {
					mainMenuCursor = child.gameObject;
				}
				if (child.gameObject.name == ("Game_Select_Menu")) {
					gameSelectMenu = child.gameObject;
					//Going through Game_Select_Menu to find the Game_Select_Cursor
					foreach (RectTransform gameSelectChild in gameSelectMenu.GetComponent<RectTransform>()){
						if (gameSelectChild.gameObject.name == ("Game_Select_Cursor")) {
							gameSelectCursor = gameSelectChild.gameObject;
						}
					}
				}
			}
		} else {
			Debug.Log ("Cannot find Main_Menu_Canvas");
		}
	}

	//Moves from the Splash Screen to the Main Menu
	//and sets the current cursor to mainMenuCursor
	void EnterMainMenu(){
		if (Input.GetButtonDown ("Submit") && splashMenu.activeInHierarchy) {
			onSplashScreen = false;
			splashMenu.SetActive (false);
			mainMenu.SetActive (true);
			mainMenuCursor.SetActive (true);
			currentCursor = mainMenuCursor;
		}

	}

	//Controls Cursor Vertical Movement
	int VerticalCursorMovement(float thumbStickThreshold, GameObject cursor, int cursorPositionIndex, int cursorPositionIndexMax){

		int[] cursorYPositions = cursor.GetComponent<CursorIndexTracker> ().cursorYPositions;
		Vector3 cursorPosition = new Vector3 (cursor.GetComponent<RectTransform> ().localPosition.x, cursor.GetComponent<RectTransform> ().localPosition.y, cursor.GetComponent<RectTransform> ().localPosition.z);

		//If the thumbstick has been pushed far enough
		if (Mathf.Abs (Input.GetAxis ("Vertical")) >= thumbStickThreshold && canMoveCursor) {
			canMoveCursor = false;
			//If the thumbstick has been pushed up and is not at the top of the menu
			//Move the cursor up
			if (Input.GetAxis ("Vertical") > 0 && cursorPositionIndex != 0) {
				cursorPositionIndex--;
				cursor.GetComponent<RectTransform> ().localPosition = new Vector2 (cursorPosition.x, cursorYPositions [cursorPositionIndex]);
				return cursorPositionIndex;
			}
			//If the thumbstick has been pushed down and is not at the bottom of the menu
			//Move the cursor down
			else if (Input.GetAxis ("Vertical") < 0 && cursorPositionIndex < cursorPositionIndexMax) {
				cursorPositionIndex++;
				cursor.GetComponent<RectTransform> ().localPosition = new Vector2 (cursorPosition.x, cursorYPositions [cursorPositionIndex]);
				return cursorPositionIndex;
			} 
			//No movement done (must return something)
			else {
				return cursorPositionIndex;
			}
		} 
		//Thumbstick lower than threshold
		else if (Mathf.Abs(Input.GetAxis ("Vertical")) <= thumbStickThreshold/2) {
			canMoveCursor = true;
			return cursorPositionIndex;
		} 
		//No movement done (must return something)
		else {
			return cursorPositionIndex;
		}
	}

	void MainMenuSelect(){
		if (Input.GetButtonDown ("Submit") && currentCursor == mainMenuCursor) {
			if (currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex == 0) {
				gameSelectMenu.SetActive (true);
				currentCursor = gameSelectCursor;
			}
		}

		if (Input.GetButtonDown ("Cancel") && currentCursor != mainMenuCursor) {
			ReturnToMainMenu ();
		}

	}

	void ReturnToMainMenu(){
		if (gameSelectMenu.activeInHierarchy) {
			ResetCursor ();
			gameSelectMenu.SetActive (false);
			currentCursor = mainMenuCursor;
		}
	}

	void ResetCursor() {
		currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex = 0;
		Vector2 currentCursorStartPos = currentCursor.GetComponent<RectTransform> ().localPosition;
		currentCursor.GetComponent<RectTransform> ().localPosition = 
			new Vector2 (currentCursorStartPos.x, currentCursor.GetComponent<CursorIndexTracker>().cursorYPositions[0]);
	}
}
