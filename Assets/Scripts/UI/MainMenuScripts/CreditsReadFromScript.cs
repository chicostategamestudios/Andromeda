using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class CreditsReadFromScript : MonoBehaviour {

	public float timeBetweenText, xPos, Ypos;

	private string credits;
	private string textRead = " ";
	private List<string> creditsListStr = new List<string>();
	private List<GameObject> creditsListObj = new List<GameObject>();
	private bool creditsOn = false;

	[SerializeField]
	private GameObject creditText, mainMenuCanvas;

	// Use this for initialization
	void Start () {
		credits = GetComponent<CreditsString> ().gameCredits;
		ParseCredits ();
		StartCoroutine(CreateCreditsText ());
	}

	void OnEnable (){
		StartCoroutine(CreateCreditsText ());
	}

	void Update (){
		if (Input.GetButtonDown ("Submit") && !creditsOn) {
			StartCoroutine(CreateCreditsText ());
		}
	}
		
	void ParseCredits(){
		Debug.Log ("Parsing Credits");
		string parsedString;

	}

	IEnumerator CreateCreditsText(){
		creditsOn = true;
		foreach (string creditString in creditsListStr){
			GameObject createdText = Instantiate (creditText) as GameObject;
			createdText.transform.SetParent (mainMenuCanvas.transform, false);
			creditText.GetComponent<RectTransform> ().position = new Vector3 (xPos, Ypos, 0.0f);
			createdText.GetComponent<Text>().text = creditString;
			creditsListObj.Add (createdText);
			yield return new  WaitForSeconds (timeBetweenText);
		}
		creditsOn = false;
	}
}
