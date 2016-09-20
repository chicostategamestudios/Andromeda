using UnityEngine;
using System.Collections;
using System;

<<<<<<< HEAD
public class Lavguanas : MonoBehaviour
{
=======
public class Lavguanas : MonoBehaviour {
>>>>>>> bd9a9fc704324b4c21d508bd508861e30f516b29

    bool grounded;
    public float speed = 3;
    public float randomTimeCheck;
    public float changeDirChance;
    public float damage;
    float forward = 1;
    public float wallDist = 0.6f;
    float verticleSpeed;
    float grav = 10;
    float distToGround;
    GameObject Player;
<<<<<<< HEAD
    Renderer rend;
    private Color lerpedColor = Color.white;
    float fireRate = 5;
    float lerpTime = 0;
    public GameObject pellet;
    private GameObject clone;
=======
    int playerDirection;
>>>>>>> bd9a9fc704324b4c21d508bd508861e30f516b29


    // Use this for initialization
    void Start()
    {
<<<<<<< HEAD
        rend = GetComponent<Renderer>();
=======
>>>>>>> bd9a9fc704324b4c21d508bd508861e30f516b29
        Player = GameObject.FindGameObjectWithTag("Player");
        distToGround = GetComponent<Collider>().bounds.extents.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
<<<<<<< HEAD
        }
        Movement movement = Player.GetComponent<Movement>();
        Debug.Log(Movement.playerDirection);
        if (Player.transform.position.x < transform.position.x)
        {
            if (Movement.playerDirection > 0)
            {
                Debug.Log("player is facing");
                rend.enabled = false;
                speed = 1;
            }
            else
            {
                Debug.Log("player is facing away 1");
                rend.enabled = true;
                speed = 3;
                lerpedColor = Color.Lerp(Color.white, Color.red, lerpTime);
                if (lerpTime < 1)
                {
                    lerpTime += Time.deltaTime / fireRate;
                }
                else
                {
                    fire();
                }
                rend.material.color = lerpedColor;



=======
            Movement movement = Player.GetComponent<Movement>();
        }
        if (Player.transform.position.x < transform.position.x)
        {
            if (playerDirection > 0)
            {
                Debug.Log("player is facing ig");
>>>>>>> bd9a9fc704324b4c21d508bd508861e30f516b29
            }
        }
        else
        {
<<<<<<< HEAD
            if (Movement.playerDirection > 0)
            {
                Debug.Log("player is facing away");
                rend.enabled = true;
                speed = 3;
                lerpedColor = Color.Lerp(Color.white, Color.red, lerpTime);
                if (lerpTime < 1)
                {
                    lerpTime += Time.deltaTime / fireRate;


                }
                else
                {
                    Debug.Log("player is facing");
                    rend.enabled = false;
                    speed = 1;
                }

            }

            /*
            if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
            {
                verticleSpeed = 0;
            }
            else
            {
                verticleSpeed -= grav * Time.deltaTime;
            }

            Vector3 moveVector = new Vector3(speed * forward, verticleSpeed, 0f);
            transform.Translate(moveVector * Time.deltaTime);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.right * forward, out hit, wallDist) && hit.collider.gameObject.tag != "Player")
            {
                forward *= -1f;
            }
            */
        }
    }

    void fire ()
    {
        clone = Instantiate(pellet, (transform.position + (transform.forward)), transform.rotation) as GameObject;
        clone.transform.Translate(Vector3.forward * Time.deltaTime * 10);
        lerpTime = 0;
    }
}
=======

        }

        /*
        if player is in range
            check for player x and direction
            if players x is less than ai and direction is + 
            if players x is less than ai and direction is - 
            if players x is greater than ai and direction is + 
            if players x is greater than ai and direction is -
                
        */


        /*
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        {
            verticleSpeed = 0;
        }
        else
        {
            verticleSpeed -= grav * Time.deltaTime;
        }

        Vector3 moveVector = new Vector3(speed * forward, verticleSpeed, 0f);
        transform.Translate(moveVector * Time.deltaTime);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.right * forward, out hit, wallDist) && hit.collider.gameObject.tag != "Player")
        {
            forward *= -1f;
        }
        */
    }
}

>>>>>>> bd9a9fc704324b4c21d508bd508861e30f516b29
