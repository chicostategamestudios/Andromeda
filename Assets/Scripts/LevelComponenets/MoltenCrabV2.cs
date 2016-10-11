using UnityEngine;
using System.Collections;

public class MoltenCrabV2 : MonoBehaviour {

    public float speed = 3f; //speed at which the crab travels
    public bool faceRight;  //bool to check which the crab is facing on runtime. Set by level design in editor and affects the forward float
    float forward; //float that tracks which way the crab is facing. The crabs orientation and rotations are based on whether this 1 is positive or negative. The bool faceRight will change this and keep the editor simple.
    public float wallDist = 0.6f; //Distance to the wall before the crab starts to climb it. Used to know when to rotate.
    Vector3 downCast; //Keeps track of which direction is "down" for the crab. Used to know when to rotate.
    bool floorCheck = false; //used to keep track of whether there is floor beneath the crab or not. 


    // Use this for initialization
    void Start() {
        //checks which way the the crab needs to be facing on start.
        if (faceRight == true)
        {
            forward = 1;
        }
        else
        {
            forward = -1;
        }
        //sets moveVector to the direction the crab needs to move in, moves the crab in that direction, then runs checkDir();
        Vector3 moveVector = new Vector3(speed * forward, 0, 0f);
        transform.Translate(moveVector * Time.deltaTime);
        checkDir();
    }

    // Update is called once per frame
    void Update()
    {
        checkDir();
        Vector3 moveVector = new Vector3(speed * forward, 0, 0f);
        transform.Translate(moveVector * Time.deltaTime);
        RaycastHit hit;
        //raycast in "front" of crab to check for wall. (front being transform.right due to 2D orientation) Raycast direction is modified by forward to be in "front" of the AI based on whatever direction its moving in. 
        if (Physics.Raycast(transform.position, this.transform.right * forward, out hit, wallDist) && hit.collider.gameObject.tag != "Player")
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + 90 * forward));
                checkDir();
            }
            Debug.DrawRay(downCast, -this.transform.up, Color.red);
        if (Physics.Raycast(downCast, -this.transform.up, out hit, wallDist))
        {
            floorCheck = false;
        }
        else if (floorCheck == false)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z - 90 * forward));
            floorCheck = true;
        }
    }

    void checkDir()
    {
        int rotationDir = (int)transform.rotation.eulerAngles.z / 90;
        if (transform.rotation.eulerAngles.y != 0)
        {
            //forward = -1;
        }
        else
        {
            //forward = 1;
        }

        switch (rotationDir)
        {
            case 0:
                downCast = (new Vector3(transform.position.x - .5f * forward, transform.position.y, transform.position.z));
                break;
            case 1:
                downCast = (new Vector3(transform.position.x, transform.position.y - .5f * forward, transform.position.z));
                break;
            case 2:
                downCast = (new Vector3(transform.position.x + .5f * forward, transform.position.y, transform.position.z));
                break;
            case 3:
                downCast = (new Vector3(transform.position.x, transform.position.y + .5f * forward, transform.position.z));
                break;

        }
    }
}
