using UnityEngine;
using System.Collections;

public class turretActivator : MonoBehaviour {

    public GameObject turretGun;
    public static bool activate;

    // Use this for initialization
    void Start () {

        activate = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (activate == true)
        {
            turretGun.GetComponent<turret>().enabled = true;
        }else
        {
            turretGun.GetComponent<turret>().enabled = false;
        }

    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            activate = true;
        }

    }

    void OnTriggerExit(Collider col)
    {
        activate = false;
    }
}