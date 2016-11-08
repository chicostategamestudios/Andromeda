using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour {
	public bool canMoveCursor, canPressSubmit, canPressCancel;
	public float thumbstickCursorThreshold, verticalThumb;

	public List<GameObject> previousMenusStored = new List<GameObject>();
	public List<GameObject> previousCursorsStored = new List<GameObject>();

	private GameObject mainMenuCanvas, splashMenuStudio, splashMenuTitle, mainMenu, 
	gameSelectMenu, levelSelectMenu, optionsMenu, windowedDropMenu, resolutionDropMenu, 
	qualityDropMenu, creditsMenu, statsMenu, exitMenu, mainMenuCursor, gameSelectCursor, 
	optionsMenuCursor, windowedDropCursor, resolutionDropCursor, qualityDropCursor,
	exitMenuCursor, currentCursor, currentMenu, noCursor;

	private bool onSplashScreenStudio, onSplashScreenTitle, onMainMenu, onOptionsMenu, onDropDownMenu;

	private string  mainMenuCursorLocation, gameSelectCursorLocation,
	exitMenuCursorLocation;

	void Start () {
		currentMenu = null;
		canPressSubmit = true;
		canPressCancel = true;
		onSplashScreenStudio = true;
		onSplashScreenTitle = false;
		onMainMenu = false;
		onOptionsMenu = false;
		canMoveCursor = true;
		GetAllMainMenuObjects ();
	
	
	}

	void Update () {
		EnterSplashScreenTitle ();
		if (!onSplashScreenStudio && onSplashScreenTitle) {
			EnterMainMenu ();
		}
		//Seting the current cursors position if not on splash screen
		if (!onSplashScreenTitle && !onSplashScreenStudio && onMainMenu) {
			currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex = 
			VerticalCursorMovement (thumbstickCursorThreshold, currentCursor,
				currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex, 
				currentCursor.GetComponent<CursorIndexTracker> ().cursorIndexMax);
			verticalThumb = Input.GetAxis ("Vertical");
			MainMenuSelect ();
		}
		if (onOptionsMenu) {
			OptionsMenuSelect ();
		}
		if (onDropDownMenu) {
			DropDownMenuSelect ();
		}

		ResetButtonsOnUp ();
	}

	//Finds all of the Main Menu Objects by going
	//through each child object in the Main Menu Canvas
	//and sets ech one to its appropriate object
	void GetAllMainMenuObjects(){
		if (GameObject.Find ("Main_Menu_Canvas")) {
			mainMenuCanvas = GameObject.Find ("Main_Menu_Canvas");
			foreach (RectTransform child in mainMenuCanvas.GetComponent<RectTransform>()) {
				if (child.gameObject.name == ("CSGS_Splash_Screen_Image")) {
					splashMenuStudio = child.gameObject;
				}

				if (child.gameObject.name == ("Andromeda_Splash_Screen_Image")) {
					splashMenuTitle = child.gameObject;
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
				if (child.gameObject.name == ("Options_Menu")) {
					optionsMenu = child.gameObject;
					//Going through Options_Menu to find the drop Menus
					foreach (RectTransform optionsMenuChild in optionsMenu.GetComponent<RectTransform>()){
						if (optionsMenuChild.gameObject.name == ("Windowed_Dropdown_Menu")) {
							GameObject windowedDropContainer = optionsMenuChild.gameObject;
							//Going through full window drop menu container to find the drop menu
							foreach (RectTransform windowedDropContainerChild in windowedDropContainer.GetComponent<RectTransform>()) {
								if (windowedDropContainerChild.gameObject.name == ("Windowed_Drop_Menu")) {
									windowedDropMenu = windowedDropContainerChild.gameObject;
									//Going through the windowed drop menu to find the windowed drop cursor
									foreach (RectTransform windowedDropChild in windowedDropMenu.GetComponent<RectTransform>()) {
										if (windowedDropChild.gameObject.name == "Windowed_Menu_Cursor") {
											windowedDropCursor = windowedDropChild.gameObject;
										}
									}
								}
							}
						}
						if (optionsMenuChild.gameObject.name == ("Resolution_Dropdown_Menu")) {
							GameObject resolutionDropContainer = optionsMenuChild.gameObject;
							//Going through full resolution drop menu container to find the drop menu
							foreach (RectTransform resolutionDropContainerChild in resolutionDropContainer.GetComponent<RectTransform>()) {
								if (resolutionDropContainerChild.gameObject.name == ("Resolution_Drop_Menu")) {
									resolutionDropMenu = resolutionDropContainerChild.gameObject;
									//Going through the resolution drop menu to find the resolution drop cursor
									foreach (RectTransform resolutionDropChild in resolutionDropMenu.GetComponent<RectTransform>()) {
										if (resolutionDropChild.gameObject.name == "Resolution_Menu_Cursor") {
											resolutionDropCursor = resolutionDropChild.gameObject;
										}
									}
								}
							}
						}
						if (optionsMenuChild.gameObject.name == ("Quality_Dropdown_Menu")) {
							GameObject qualityDropContainer = optionsMenuChild.gameObject;
							//Going through full quality drop menu container to find the quality menu
							foreach (RectTransform qualityDropContainerChild in qualityDropContainer.GetComponent<RectTransform>()) {
								if (qualityDropContainerChild.gameObject.name == ("Quality_Drop_Menu")) {
									qualityDropMenu = qualityDropContainerChild.gameObject;
									//Going through the quality drop menu to find the qualitydrop cursor
									foreach (RectTransform qualityDropChild in qualityDropMenu.GetComponent<RectTransform>()) {
										if (qualityDropChild.gameObject.name == "Quality_Menu_Cursor") {
											qualityDropCursor = qualityDropChild.gameObject;
										}
									}
								}
							}
						}
					}
				}
				if (child.gameObject.name == ("Options_Menu_Cursor")) {
					optionsMenuCursor = child.gameObject;
				}
				if (child.gameObject.name == ("Stats_Menu")) {
					statsMenu = child.gameObject;
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

	void EnterSplashScreenTitle(){
		if (Input.GetButtonDown ("Submit") && splashMenuStudio.activeInHierarchy && canPressSubmit) {
			canPressSubmit = false;
			onSplashScreenStudio = false;
			onSplashScreenTitle = true;
			splashMenuTitle.SetActive (true);
			splashMenuStudio.SetActive (false);
		}

		if (!splashMenuStudio.activeInHierarchy && !splashMenuTitle.activeInHierarchy &&!onMainMenu) {
			onSplashScreenStudio = false;
			onSplashScreenTitle = true;
			splashMenuTitle.SetActive (true);
		}

	}

	//Moves from the Splash Screen to the Main Menu
	//and sets the current cursor to mainMenuCursor
	void EnterMainMenu(){
		if (Input.GetButtonDown ("Submit") && splashMenuTitle.activeInHierarchy && canPressSubmit) {
			canPressSubmit = false;
			onSplashScreenTitle = false;
			onMainMenu = true;
			splashMenuTitle.GetComponent<FadeBehavior>().FadeOut();
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
			if (Input.GetAxis ("Vertical") > 0) {
				if (cursorPositionIndex == 0) {
					cursorPositionIndex = cursorPositionIndexMax;
				} else {
					cursorPositionIndex--;
				}
				cursor.GetComponent<RectTransform> ().localPosition = new Vector2 (cursorPosition.x, cursorYPositions [cursorPositionIndex]);
				return cursorPositionIndex;
			}
			//If the thumbstick has been pushed down and is not at the bottom of the menu
			//Move the cursor down
			else if (Input.GetAxis ("Vertical") < 0) {
				if (cursorPositionIndex == cursorPositionIndexMax) {
					cursorPositionIndex = 0;
				} else {
					cursorPositionIndex++;
				}
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
			//If we're selecting Options
			if (currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex == 1) {
				SelectMenu (optionsMenu, optionsMenuCursor, currentMenu, currentCursor);
				onOptionsMenu = true;
			}
			//If we're selecting Stats
			if (currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex == 2) {
				SelectMenu (statsMenu, noCursor, currentMenu, currentCursor);
			}
			//If we're selecting Credits
			if (currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex == 3) {
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

			//Deactivating the cursor and menu being left
			menuTemp.SetActive (false);
			cursorTemp.SetActive (false);

			if (menuTemp == optionsMenu) {
				onOptionsMenu = false;
			}
			if (onDropDownMenu) {
				onDropDownMenu = false;
				onOptionsMenu = true;
			}

			//Reseting the cursor being left back to 0
			ResetCursor ();
			//Reseting current menu back to previous
			currentMenu = previousMenusStored [previousMenusStored.Count - 1];

			//Removing the last stored previous menu
			previousMenusStored.Remove (previousMenusStored [previousMenusStored.Count - 1]);

			//Reseting the current cursor back to previous
			currentCursor = previousCursorsStored [previousCursorsStored.Count - 1];

			//Removing the last stored previous cursor
			previousCursorsStored.Remove (previousCursorsStored [previousCursorsStored.Count - 1]);

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
		previousCursorsStored.Add (lastCursor);
		previousMenusStored.Add (lastMenu);
		Debug.Log ("previousCursorsStored has " + previousCursorsStored.Count + " and the last cursor in the list is " + previousCursorsStored [previousCursorsStored.Count - 1]);
		Debug.Log ("previousMenusStored has " + previousMenusStored.Count + " and the last menu in the list is " + previousMenusStored [previousMenusStored.Count - 1]);
		selectedMenu.SetActive (true);
		selectedMenuCursor.SetActive (true);
		currentCursor = selectedMenuCursor;
		currentMenu = selectedMenu;
	}

	void OptionsMenuSelect (){
		if (Input.GetButtonDown ("Submit") && currentCursor == optionsMenuCursor && canPressSubmit) {
			canPressSubmit = false;
			//If we're selecting Windowed
			if (currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex == 3) {
				SelectMenu (windowedDropMenu, windowedDropCursor, currentMenu, currentCursor);
				onOptionsMenu = false;
				onDropDownMenu = true;
			}
			//If we're selecting Resolution
			if (currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex == 4) {
				SelectMenu (resolutionDropMenu, resolutionDropCursor, currentMenu, currentCursor);
				onOptionsMenu = false;
				onDropDownMenu = true;
			}
			//If we're selecting Quality
			if (currentCursor.GetComponent<CursorIndexTracker> ().currentCursorIndex == 5) {
				SelectMenu (qualityDropMenu, qualityDropCursor, currentMenu, currentCursor);
				onOptionsMenu = false;
				onDropDownMenu = true;
			}
		}
	}

	void DropDownMenuSelect(){
		if (Input.GetButtonDown ("Submit") && canPressSubmit) {
			currentCursor.GetComponent<DropDownMenuCursorBehavior> ().PassSelectedDropDownData ();
			ReturnToPreviousMenu ();
		}
	}
		
}
