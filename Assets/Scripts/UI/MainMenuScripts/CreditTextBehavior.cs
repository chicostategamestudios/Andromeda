using UnityEngine;
using System.Collections;

public class CreditTextBehavior : MonoBehaviour {


	public float scrollSpeed = 1.0f;

	void Update(){
		this.GetComponent<RectTransform> ().anchoredPosition += new Vector2 (0.0f, scrollSpeed * Time.deltaTime);

		if (Input.GetButtonDown ("Cancel")){
			Destroy (this.gameObject);
		}
	}
}
