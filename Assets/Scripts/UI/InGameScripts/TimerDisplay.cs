//Original Author:  | Last Edited: Ying Xiong | Modified on Feb 09, 2017
//This script's purpose is to display the time run's time
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour {

	private static TimerDisplay myTimer;

	public Text MyTimerText;

	private float timer = 0.0f;

    [HideInInspector]
    public bool UpdateTime;

	// Use this for initialization
	void Start () {
        UpdateTime = false;
		myTimer = this;
		MyTimerText = this.GetComponent<Text>();
        MyTimerText.text = TimeToString();

    }
	
	// Update is called once per frame
	void Update () {
        
        // Returns the function until the player hits the teleport
        if(!UpdateTime)
            return;

        // Time will start running once Phase 2 has started
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
