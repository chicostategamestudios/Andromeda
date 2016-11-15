using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour {

	private static TimerDisplay myTimer;

	public Text MyTimerText;

	private float timer = 0.0f;

	bool UpdateTime = true;

	// Use this for initialization
	void Start () {
		myTimer = this;
		MyTimerText = this.GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (UpdateTime) {
			timer += Time.deltaTime;
			if (MyTimerText != null) {
				MyTimerText.text = TimeToString ();
			}
		}
	}

	public static TimerDisplay getTimer {
		get {
			if (myTimer == null) {
				myTimer = FindObjectOfType<TimerDisplay> ();
			}
			if (myTimer == null) {
				Debug.LogError ("cannot find the TimerDisplay");
				return null;
			} else {
				return myTimer;
			}
		}
	}


	private string TimeToString(){
		int minutes = Mathf.FloorToInt (timer / 60f);
		int seconds = Mathf.FloorToInt (timer % 60);
		float miliseconds = timer * 1000;
		miliseconds = miliseconds % 1000;

		string displayedTimer = string.Format ("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
		return displayedTimer;

	}

	public string GetFinalTime(){
		UpdateTime = false;
		return TimeToString ();
	}


}
