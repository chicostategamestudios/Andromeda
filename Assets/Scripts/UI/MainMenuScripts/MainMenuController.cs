using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	public bool canMoveCursor, canPressSubmit, canPressCancel;
	public float thumbstickCursorThreshold, verticalThumb;

	public GameObject currentCursor;

	private GameObject mainMenuCanvas, splashMenu, mainMenu, gameSelectMenu,
	levelSelectMenu, optionsMenu, creditsMenu, statsMenu, exitMenu, 
	mainMenuCursor, gameSelectCursor, exitMenuCursor, /*currentCursor,*/
	currentMenu, previousMenu, previousCursor, noCursor;

	private bool onSplashScreen; 

	private string  mainMenuCursorLocation, gameSelectCursorLocation,
	exitMenuCursorLocation;

	void Start () {
		previousMenu = null;
		currentMenu = null;
		canPressSubmit = true;
		canPressCancel = true;
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
			ResetButtonsOnUp ();
		}
	}

	//Finds all of the Main Menu Objects by going
	//through each child object in the Main Menu Canvas
	//and sets ech one to its appropriate object
	void GetAllMainMenuObjects(){
		if (GameObject.Find ("Main_Menu_Canvas")) {
			mainMenuCanvas = GameObject.Find ("Main_Menu_Canvas");
			foreach (RectTransform child in mainMenuCanvas.GetComponent<RectTransform>()) {
				if (child.gameObject.name == ("Andromeda_Splash_Screen_Image")) {
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
				if (child.gameObject.name == ("Credits_Text_Controller_Obj")) {
					creditsMenu = child.gameObject;
				}
				if (child.gameObject.name == ("No_Cursor")) {
					noCursor = child.gameObject;
				}
				if (child.gameObject.name == ("Exit_Menu")) {
					exitMenu = child.gameObject;
				}
				if (child.gameObject.name == ("Exit_Menu_Cursor")) {
					exitMenuCursor = child.gameObject;
				}
			}
		} else {
			Debug.Log ("Cannot find Main_Menu_Canvas");
		}
	}

	//Moves from the Splash Screen to the Main Menu
	//and sets the current cursor to mainMenuCursor

	//For every getbutton down dont allow another unitl the button has been released
	void EnterMainMenu(){
		if (Input.GetButtonDown ("Submit") && splashMenu.activeInHierarchy && canPressSubmit) {
			canPressSubmit = false;
			onSplashScreen = false;
			splashMenu.SetActive (false);
			mainMenu.SetActive (true);
			mainMenuCursor.SetActive (true);
			currentCursor = mainMenuCursor;
			currentMenu = mainMenu;
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
		if (Input.GetButtonDown ("Submit") && currentCursor == mainMenuCursor && canPressSubmit) {
			canPressSubmit = false;
			//If we're selecting Play
			if (currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex == 0) {
				SelectMenu (gameSelectMenu, gameSelectCursor, currentMenu, currentCursor);
			}
			//If we're selecting Credits
			if (currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex == 2) {
				SelectMenu (creditsMenu, noCursor, currentMenu, currentCursor);
			}
			//If we're selecting Exit
			if (currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex == 4) {
				SelectMenu (exitMenu, exitMenuCursor, currentMenu, currentCursor);
			}
		}

		if (Input.GetButtonDown ("Cancel") && currentCursor != mainMenuCursor && canPressCancel) {
			canPressCancel = false;
			ReturnToPreviousMenu ();
		}

	}

	public void ReturnToPreviousMenu(){
		GameObject menuTemp, cursorTemp;
		if (currentMenu.activeInHierarchy) {
			menuTemp = currentMenu;
			cursorTemp = currentCursor;
			menuTemp.SetActive (false);
			cursorTemp.SetActive (false);
			ResetCursor ();
			currentMenu = previousMenu;
			currentCursor = previousCursor;
			currentMenu.SetActive (true);
			currentCursor.SetActive (true);


		}
	}

	void ResetCursor() {
		currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex = 0;
		Vector2 currentCursorStartPos = currentCursor.GetComponent<RectTransform> ().localPosition;
		currentCursor.GetComponent<RectTransform> ().localPosition = 
			new Vector2 (currentCursorStartPos.x, currentCursor.GetComponent<CursorIndexTracker>().cursorYPositions[0]);
	}

	void ResetButtonsOnUp(){
		if (Input.GetButtonUp ("Submit")) {
			canPressSubmit = true;
		}
		if (Input.GetButtonUp ("Cancel")) {
			canPressCancel = true;
		}

	}

	void SelectMenu (GameObject selectedMenu, GameObject selectedMenuCursor, GameObject lastMenu, GameObject lastCursor)
	{
		previousCursor = lastCursor;
		previousMenu = lastMenu;
		selectedMenu.SetActive (true);
		selectedMenuCursor.SetActive (true);
		currentCursor = selectedMenuCursor;
		currentMenu = selectedMenu;
	}
		
}
