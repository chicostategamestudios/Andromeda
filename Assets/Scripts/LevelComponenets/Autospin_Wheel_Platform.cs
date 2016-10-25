using UnityEngine;
using System.Collections;


public class Autospin_Wheel_Platform : MonoBehaviour
{

    public Autospin_Wheel_Platforms myWheel;
    public platType myType;

    float breakPoint;
    // Use this for initialization
    void Start()
    {
        myWheel = this.gameObject.transform.parent.GetComponent<Autospin_Wheel_Platforms>();
        

    }

    void FixedUpdate()
    {
        if ((Mathf.Abs(this.gameObject.transform.localPosition.x) < breakPoint) &&
            this.gameObject.transform.localPosition.y < 0f)
        {
            myType = platType.none;

        }
        else if (this.gameObject.transform.localPosition.x > breakPoint)
        {
            myType = platType.right;
        }
        else if (this.gameObject.transform.localPosition.x < (breakPoint * -1f))
        {
            myType = platType.left;
        }
        else
        {
            myType = platType.left;
        }


    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            if (myType == platType.none)
            {
                print("none");
                print(gameObject.transform.localPosition.z);
                print("Y:" + gameObject.transform.localPosition.y);
                return;
            }
        }


    }


}
