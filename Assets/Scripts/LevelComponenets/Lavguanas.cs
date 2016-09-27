using UnityEngine;
using System.Collections;
using System;

public class Lavguanas : MonoBehaviour
{
    float speed = 3; //speed at which the AI moves
    float forward = 1; //determines which way is forward. 1 is right, -1 is left
    public float wallDist = 0.6f; //distance to the wall
    float verticleSpeed; //speed at which the AI falls
    float grav = 10; //gravity intensity
    float distToGround; //distance to the ground from the cetner of the AI
    GameObject Player; // referance to the player
    Renderer rend; //referance to the renderer
    private Color lerpedColor = Color.white; //the color that the AI lerps between
    float fireRate = 5; //rate at which the AI fires at
    float lerpTime = 0; //time it takes to lerp
    public GameObject pellet; // ref to the pellet Prefab
    private GameObject clone; //ref to spawned pellet
    int playerDirection; //variable to hold the direction of the player
    public int fireDirection; //variable to check the direction the Ai fires in

    // Use this for initialization
    void Start()
    {
        //sets the renderer, player, and distToGround components
        rend = GetComponent<Renderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        //if there is no ref to player, add ref to player
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        Assets.Scripts.Character.CharController charController = Player.GetComponent<Assets.Scripts.Character.CharController>();
        //checks where the player is in relation to itself. This checks if the player is to the left of itself
        if (Player.transform.position.x < transform.position.x)
        {
            //checks if the players facing right. The previous if statement and this check if the player is looking at the AI from the left
            if (charController.lastDir > 0)
            {
                //turns off the AI's renderer making it invisible, slows it down, and reduces the timer on its fire timer
                rend.enabled = false;
                speed = 1;
                if (lerpTime > 0)
                {
                    lerpTime -= Time.deltaTime / fireRate;
                }

            }
            //if the player is facing away from AI while to the left of the AI
            else
            {
                //turns on the renderer, speed it up, lerps the color of the AI from white to red, sets the direction to fire in, starts timer to fire
                rend.enabled = true;
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
        //if the player is to the right of the AI
        else
        {
            //if the player is facing away while to the right of the AI
            if (charController.lastDir > 0)
            {
                //turns on the renderer, speed it up, lerps the color of the AI from white to red, sets the direction to fire in, starts timer to fire
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
            //if the player is facing the AI while to the right of the AI
            else
            {
                {
                    //turns off the AI's renderer making it invisible, slows it down, and reduces the timer on its fire timer
                    rend.enabled = false;
                    speed = 1;
                    if (lerpTime > 0)
                    {
                        lerpTime -= Time.deltaTime / fireRate;
                    }

                }
            }

        }
        //raycast from the AI to directly underneath it to check if there is ground, if so verticalSpeed is 0 otherwise add gravity to vertical speed
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        {
            verticleSpeed = 0;
        }
        else
        {
            verticleSpeed -= grav * Time.deltaTime;
        }
        //move forward over time
        Vector3 moveVector = new Vector3(speed * forward, verticleSpeed, 0f);
        transform.Translate(moveVector * Time.deltaTime);
        RaycastHit hit;
        //raycast forward to detect a wall while ignoring the player, if true turn around
        if (Physics.Raycast(transform.position, Vector3.right * forward, out hit, wallDist) && hit.collider.gameObject.tag != "Player")
        {
            forward *= -1f;
        }
    }

    //fire a small pellet forward. Restarts the time to fire.
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


