using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {

    public GameObject Bullet_Emitter;
    public GameObject Bullet;

    // Use this for initialization
    void Start() {
        InvokeRepeating("FireBullets", 0, 2.0f);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

        void FireBullets()
    {
        if (turretActivator.activate == true)
            {
            Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation);
            }
        }
    

}
