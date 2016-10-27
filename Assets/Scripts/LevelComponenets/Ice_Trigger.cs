using UnityEngine;
using System.Collections;

public class Ice_Trigger : MonoBehaviour {

    public bool icefalling = false;
    public float fallSpeed = 8.0f;
    public RaycastHit hit;
    public int dist;
    private Vector3 dir;

    public Vector3 startPosition;
    public Quaternion startRotation;

    // Use this for initialization
    void Start () {
        StoreStartPosition(ref startPosition, ref startRotation);
        LevelReset.myLevelElements.Add(this);
        dist = 10;
        dir = new Vector3(0, -1, 0);
    }

    void Update()
    {
        if (icefalling)
        {
            if (Physics.Raycast(transform.position, dir, dist))
            {
                transform.Translate(dir * fallSpeed * Time.deltaTime);
                print("Works");
                Debug.DrawRay(transform.position, dir * dist, Color.green);
            }
        }
    }
      

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            icefalling = true;
        }
    }

    public void Reset()
    {
        icefalling = false;
        this.transform.position = startPosition;
        this.transform.rotation = startRotation;

    }

    void StoreStartPosition(ref Vector3 startPos, ref Quaternion startRot)
    {

        startPos = this.transform.position;
        startRot = this.transform.rotation;

    }
}
