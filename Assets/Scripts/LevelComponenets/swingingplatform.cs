using UnityEngine;
using System.Collections;

public class swingingplatform : MonoBehaviour {

    public Transform[] patrolPoints;
    public float moveSpeed;
    public int currentPoint;
    public bool forward;

    // Use this for initialization
    void Start()
    {
        transform.position = patrolPoints[0].position;
        currentPoint = 0;
        forward = true;
    }

    // Update is called once per frame
    void Update()
    {

        
            if (forward == true)
            {
                if (transform.position == patrolPoints[currentPoint].position)
                {
                    currentPoint++;
                }

                if (currentPoint == 5)
                {
                    currentPoint = 4;
                    forward = false;
                }
            }
            if (forward == false)
            {

                if (transform.position == patrolPoints[currentPoint].position)
                {
                    currentPoint--;
    
                }

                if (currentPoint == 0)
                {
                    currentPoint = 0;
                    forward = true;
                }
            }

        if (currentPoint >= patrolPoints.Length)
        {
            currentPoint = 0;
            Debug.Log("ERROR");
        }


        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);

    }
}
