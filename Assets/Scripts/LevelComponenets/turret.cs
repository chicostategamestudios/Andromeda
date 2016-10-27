using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {

    public GameObject Bullet_Emitter;
    public GameObject Bullet;

    public GameObject turretGun;
    public bool activate;

    // Use this for initialization
    void Start() {

        activate = false;
        InvokeRepeating("FireBullets", 0, 2.0f);
        
    }

        void FireBullets()
    {
        if (activate == true)
            {
            Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation);
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
