using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

    public float bulletSpd = 1f;
    public float lifetime = 2f;

    void Awake()
    {
        Destroy(gameObject, lifetime);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(bulletSpd, 0, 0);
	}
}
