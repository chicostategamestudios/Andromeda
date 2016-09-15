using UnityEngine;
using System.Collections;
using System;

public class Lavguanas : MonoBehaviour {

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
    int playerDirection;


    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        distToGround = GetComponent<Collider>().bounds.extents.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Movement movement = Player.GetComponent<Movement>();
        }
        if (Player.transform.position.x < transform.position.x)
        {
            if (playerDirection > 0)
            {
                Debug.Log("player is facing ig");
            }
        }
        else
        {

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

