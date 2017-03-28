using UnityEngine;
using System.Collections;

public class FloatingObjectGeneric : MonoBehaviour {
    public static float globalScaler = 1f;

    public float amplitde = 0.5f;
    public float amplitdeRandomOffset = 0.2f;
    public float speed = 1;
    public float speedRandomOffset = 0.5f;


    public float rotationAplitude = 2f;
    public float rotSpeed = 1;
    public float randomOffset = 0.75f;


    private float offset;
    private float startVal;
    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 startRotEuler;

	// Use this for initialization
	void Start () {
        startVal = transform.position.y;
        startPos = this.transform.position;
        startRot = this.transform.rotation;
        startRotEuler = this.transform.rotation.eulerAngles;
        offset = Random.Range(0, randomOffset) * globalScaler;
        speed += Random.Range(0, speedRandomOffset) * globalScaler;
        rotSpeed += Random.Range(-randomOffset, randomOffset) * globalScaler;
        amplitde += Random.Range(-amplitdeRandomOffset, amplitdeRandomOffset) * globalScaler;
	}
	
	// Update is called once per frame
	void Update () {
        startPos.y = startVal + amplitde * Mathf.Sin(speed * Time.time + randomOffset);
        transform.position = startPos;

        startRotEuler.y = startRotEuler.y + (rotationAplitude * 0.01f) * Mathf.Sin(rotSpeed * Time.time + randomOffset);
        startRot.eulerAngles = startRotEuler;
        transform.rotation = startRot;
	}
}
