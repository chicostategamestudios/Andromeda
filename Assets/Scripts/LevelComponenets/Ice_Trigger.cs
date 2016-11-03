using UnityEngine;
using System.Collections;

public class Ice_Trigger : MonoBehaviour {

    public bool icefalling = false;
    public float fallTime = 1.0f;
    public float fallSpeed = 8.0f;
    public float spinSpeed = 250.0f;

	// Use this for initialization
	void Start () {
        
	}
	
    void Update()
    {

        if (icefalling)
        {
            Invoke("FallTimer", fallTime);
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.down, spinSpeed * Time.deltaTime);
        }else
        {

        }
    }


	// Update is called once per frame
	void OnTriggerEnter (Collider col) {

        if (col.gameObject.tag == "Player")
        {
            icefalling = true;
        }
    }

    void FallTimer()
    {
        Debug.Log("fall_timer_invoked");
        icefalling = false;
        GetComponent<BoxCollider>().enabled = false;
    }
}
