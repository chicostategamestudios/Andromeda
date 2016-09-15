using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
public class CreditMenuController : MonoBehaviour {

	public float timeBetweenText;
	private List<string> creditsListStr = new List<string>();
	private List<GameObject> creditsListObj = new List<GameObject>();
	private FileInfo creditsSourceFile;
	private bool creditsOn = false;
	private StreamReader creditsStream = null;
	private string textRead = " ";

	[SerializeField]
	private GameObject creditText, mainMenuCanvas;

	void Start () {
		creditsSourceFile = new FileInfo ("Assets\\Scripts\\UI\\MainMenuScripts\\creditsTextTest.txt");
		creditsStream = creditsSourceFile.OpenText ();
		ReadInCreditsData ();
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

	void ReadInCreditsData(){
		if (creditsSourceFile.Exists) {
			while (textRead != null) {
				textRead = creditsStream.ReadLine ();
				creditsListStr.Add (textRead);
			}
		}
	}

	IEnumerator CreateCreditsText(){
		creditsOn = true;
		foreach (string creditString in creditsListStr){
			GameObject createdText = Instantiate (creditText) as GameObject;
			createdText.transform.SetParent (mainMenuCanvas.transform);
			creditText.transform.position = this.transform.position;
			createdText.GetComponent<Text>().text = creditString;
			creditsListObj.Add (createdText);
			yield return new  WaitForSeconds (timeBetweenText);
		}
		creditsOn = false;
	}
}
