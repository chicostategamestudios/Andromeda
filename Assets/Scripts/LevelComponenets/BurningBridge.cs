using UnityEngine;
using System.Collections;

public class BurningBridge : MonoBehaviour {
    public Transform bridge;
    BoxCollider myTrigger;

    // Use this for initialization
    void Start () {
        myTrigger = this.gameObject.GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)  {


        if (col.gameObject.tag == ("Player"))
        {
            InvokeRepeating("Fall", 0.01f, 0.01f);
            Destroy(myTrigger);

        }
    }

    void Fall() {
        bridge.Rotate(Vector3.forward);
    }
}
