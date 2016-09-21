using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    public float lifetime = 10f;
    public float speed;
    public float jumpheight;
    public float gravityRate;
    float verticleSpeed;
    Vector3 moveVector;
    public bool grounded;
    public float rayDist = 1.2f;
    public float rayDir;
    public float rotSpeed;
    public Transform FireMesh;
    public float gravdecrease = .9f;
    // Use this for initialization

        void Awake()
    {
        if (lifetime != 0) {
            Destroy(gameObject, lifetime);
        }
    }

    void Start()
    {
        //Detect 
        rayDir = speed / (Mathf.Abs(speed));
        FireMesh = this.transform.GetChild(0);
    }

    // Update is called once per frame


    void Update()
    {
        
        //check if grounded, grouded is equal to raycast
        grounded = (Physics.Raycast(transform.position, -Vector3.up, rayDist));

        //If not grounded then force of gravity pushes down
        if (!grounded)
        {
            verticleSpeed -= gravityRate * Time.deltaTime;
        }
        else //If grounded then jump up equal to jump height
        {
            jumpheight = jumpheight * gravdecrease;
            verticleSpeed = jumpheight;

        }

        //Rotate the object connected (the fire) equal the rotation speed * direction * time
        FireMesh.Rotate(Vector3.up * rotSpeed * rayDir * Time.deltaTime);

        //move vector determines speed of the verticle jump
        moveVector = new Vector3(speed * rayDir, 0, verticleSpeed);
        //transforming object for movement
        transform.Translate(moveVector * Time.deltaTime);

        
        if (Physics.Raycast(transform.position, Vector3.right * rayDir, rayDist, 8))
        {

            rayDir *= -1f;
        }


    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.name == "CubeDeath")
        {
            Debug.Log("fuck");
            Destroy(this.gameObject);
        }

    }


}
