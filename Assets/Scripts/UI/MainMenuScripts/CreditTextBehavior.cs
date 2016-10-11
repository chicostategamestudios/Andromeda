using UnityEngine;
using System.Collections;

public class CreditTextBehavior : MonoBehaviour {


	public float scrollSpeed = 1.0f;
	/*
	public float textDisplayTime = 3.0f;
	public float timeToFade = 1.0f;

	private CanvasGroup myUIGroup;
	private float rateOfChange;
	private float currentTime;
	private float beginAlpha;
	private float endAlpha;
	private bool fading;
	private bool fadingOut;

	void Start(){
		myUIGroup = GetComponent<CanvasGroup> ();
		rateOfChange = 0.0f;
		currentTime = 0.0f;
		beginAlpha = 0.0f;
		endAlpha = 1.0f;
		fading = false;
		fadingOut = false;

		if (myUIGroup == null) {
			Debug.Log ("No canvas group for the credits text!");
			this.enabled = false;
		} else {
			FadeIn ();
			StartCoroutine (BeginFadeOut ());
		}
	}
*/
	void Update(){
		transform.position += new Vector3 (0.0f, scrollSpeed * Time.deltaTime, 0.0f);
		/*if (fadingOut) {
			FadeOut ();
		}*/
		if (Input.GetButtonDown ("Cancel")){
			Destroy (this.gameObject);
		}
	}
	/*
	void FadeIn(){
		beginAlpha = 0.0f;
		endAlpha = 1.0f;
		fading = true;
		StartCoroutine (FadingBehavior ());
	}

	void FadeOut(){
		beginAlpha = 1.0f;
		endAlpha = 0.0f;
		fading = true;
		fadingOut = true;
		StartCoroutine (FadingBehavior ());
	}

	IEnumerator BeginFadeOut (){
		yield return new WaitForSeconds (textDisplayTime);
		FadeOut();
	}

	IEnumerator FadingBehavior(){
		currentTime = 0.0f;
		rateOfChange = (endAlpha - beginAlpha) / timeToFade;
		ChangeAlpha (beginAlpha);
		while (fading) {
			
			currentTime += Time.deltaTime;

			if (currentTime > timeToFade) {
				
				fading = false;
				ChangeAlpha (endAlpha);

				if (fadingOut) {
					Destroy (this.gameObject);
				}

				yield break;

			} else {
				
				ChangeAlpha (myUIGroup.alpha + (rateOfChange * Time.deltaTime));
			}

			yield return null;
		}
	}

	public void ChangeAlpha (float alpha){
		myUIGroup.alpha = Mathf.Clamp (alpha, 0, 1);
	}*/
}
