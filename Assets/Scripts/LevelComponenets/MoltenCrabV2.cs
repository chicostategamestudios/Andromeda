using UnityEngine;
using System.Collections;

public class MoltenCrabV2 : MonoBehaviour
{

    public float speed = 3f; //speed at which the crab travels
    public bool faceRight;  //bool to check which the crab is facing on runtime. Set by level design in editor and affects the forward float
    float forward; //float that tracks which way the crab is facing. The crabs orientation and rotations are based on whether this 1 is positive or negative. The bool faceRight will change this and keep the editor simple.
    public float wallDist = .6f; //Distance to the wall before the crab starts to climb it. Used to know when to rotate.
    Vector3 downCast; //Keeps track of which direction is "down" for the crab. Used to know when to rotate.
    bool floorCheck = false; //used to keep track of whether there is floor beneath the crab or not. 
    float height;
    float width;
    float rotAngleRad;
    float rotAngleDeg;
    bool rotatingForward;
    bool rotatingBack;
    public Transform forwardcastOrigin;
    public Transform backDowncastOrigin;
    public Transform frontDowncastOrigin;
    public GameObject leftWall;
    public GameObject rightWall;

    // Use this for initialization
    void Start()
    {
        //checks which way the the crab needs to be facing on start.
        if (faceRight == true)
        {
            forward = 1;
        }
        else
        {
            forward = -1;

        }
        frontDowncastOrigin.transform.localPosition = new Vector3(frontDowncastOrigin.transform.localPosition.x * forward, frontDowncastOrigin.transform.localPosition.y, frontDowncastOrigin.transform.localPosition.z);
        height = transform.localScale.y;
        //sets moveVector to the direction the crab needs to move in, moves the crab in that direction, then runs checkDir();
        Vector3 moveVector = new Vector3(speed * forward, 0, 0f);
        transform.Translate(moveVector * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotatingForward == true || rotatingBack == true)
        {
            speed = 0f;
            wallDist = 1.5f;
        }
        else
        {
            speed = 3f;
            wallDist = .65f;
        }

        Vector3 moveVector = new Vector3(speed * forward, 0, 0f);
        transform.Translate(moveVector * Time.deltaTime);
        RaycastHit bottomHit;
        //raycast in "front" of crab to check for wall. (front being transform.right due to 2D orientation) Raycast direction is modified by forward to be in "front" of the AI based on whatever direction its moving in. 
        Debug.DrawRay(forwardcastOrigin.position, this.transform.right * forward, Color.black);

        if (rotatingBack == false)
        {
            
            if (Physics.Raycast(forwardcastOrigin.position, this.transform.right * forward, out bottomHit, wallDist) && bottomHit.collider.gameObject.tag != "Player")
            {
                {
                    rotatingForward = true;
                    transform.Rotate(new Vector3(0, 0, 50 * Time.deltaTime * forward));
                }
            }
            else
            {
                rotatingForward = false;
            }

        }
        if (rotatingForward == false)
        {
            Debug.DrawRay(backDowncastOrigin.position, -this.transform.up, Color.red);
            if (Physics.Raycast(backDowncastOrigin.position, -this.transform.up, .3f))
            {
            }
            else
            {
                rotatingBack = true;
            }
            if (rotatingBack == true)
            {
                transform.RotateAround(backDowncastOrigin.position, transform.forward, -50 * Time.deltaTime * forward);
                if (Physics.Raycast(frontDowncastOrigin.position, -this.transform.up, .01f))
                {
                    rotatingBack = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "leftWall" || col.gameObject.name == "rightWall")
        {
            forward = forward * -1;
            frontDowncastOrigin.transform.localPosition = new Vector3(frontDowncastOrigin.transform.localPosition.x * -1, frontDowncastOrigin.transform.localPosition.y, frontDowncastOrigin.transform.localPosition.z);
        }
    }
}

