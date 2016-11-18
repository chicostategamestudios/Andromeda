using UnityEngine;
using System.Collections;

public class CheckpointGlow : MonoBehaviour {
	public GameObject glow;
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			glow.SetActive(true);
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			glow.SetActive(false);
		}
	}
}
