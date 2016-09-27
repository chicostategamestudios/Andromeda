using UnityEngine;
using System.Collections;
using System;

public class Lavguanas : MonoBehaviour
{

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
    Renderer rend;
    private Color lerpedColor = Color.white;
    float fireRate = 5;
    float lerpTime = 0;
    public GameObject pellet;
    private GameObject clone;
    int playerDirection;
    public int fireDirection;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        distToGround = GetComponent<Collider>().bounds.extents.y;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        Assets.Scripts.Character.CharController charController = Player.GetComponent<Assets.Scripts.Character.CharController>();
        if (Player.transform.position.x < transform.position.x)
        {
            if (charController.lastDir > 0)
            {
                Color color = rend.material.color;
                color.a = 0.0f;
                rend.material.color = color;
                speed = 1;
                if (lerpTime > 0)
                {
                    lerpTime -= Time.deltaTime / fireRate;
                }

            }
            else
            {

                speed = 3;
                lerpedColor = Color.Lerp(Color.white, Color.red, lerpTime);
                if (lerpTime < 1)
                {
                    lerpTime += Time.deltaTime / fireRate;
                }
                else
                {
                    fireDirection = -1;
                    fire();
                }
                rend.material.color = lerpedColor;
            }
        }
        else
        {
            if (charController.lastDir > 0)
            {
                rend.enabled = true;
                speed = 3;
                lerpedColor = Color.Lerp(Color.white, Color.red, lerpTime);
                if (lerpTime < 1)
                {
                    lerpTime += Time.deltaTime / fireRate;
                }
                else
                {
                    fireDirection = 1;
                    fire();
                }
                rend.material.color = lerpedColor;

            }
            else
            {
                {
                    Color color = rend.material.color;
                    color.a = 0.0f;
                    rend.material.color = color;
                    speed = 1;
                    if (lerpTime > 0)
                    {
                        lerpTime -= Time.deltaTime / fireRate;
                    }

                }
            }

        }
        
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
    
    }


    void fire()
    {
        if (fireDirection > 0)
        {
            clone = Instantiate(pellet, (transform.position), transform.rotation) as GameObject;

        }
        else
        {
            clone = Instantiate(pellet, (transform.position), transform.rotation) as GameObject;

        }
        Pellet pelletScript = clone.GetComponent<Pellet>();
        pelletScript.moveDirection = fireDirection;
        lerpTime = 0;
    }

}


