using UnityEngine;
using System.Collections;

public class Ice_Fall : MonoBehaviour {

    public float fallSpeed;
    public float spinSpeed;
    public bool activated;
    public GameObject Ice_Trig;

    void Start()
    {
        activated = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (activated)
        {
            fallSpeed = 8f;
            spinSpeed = 250f;
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.down, spinSpeed * Time.deltaTime);
        }
        if (!activated)
        {
            transform.Translate(Vector3.zero);
            transform.Rotate(Vector3.zero);
        }
    }

    void OnCollisionEnter(Collision col)
    {
            Debug.Log("stopped");
            Ice_Trig.GetComponent<Ice_Trigger>().icefalling = false;
        
    }
}
