using UnityEngine;
using System.Collections;

public class RubbleFall : MonoBehaviour {

    public bool grounded;
    public float gravityrate;
    // Use this for initialization
    void Start() {
        grounded = false;
    }

    // Update is called once per frame
    void Update() {

        if (grounded == false)
        {

        }

        if (grounded == true)
        {

        }
            
	}


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 8)
        {
            grounded = true;
        }
    }
}