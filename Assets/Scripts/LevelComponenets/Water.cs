//Author: Zachary Coon | Last Modified by Zachary Coon | Feb. 9 2017
using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

public class Water : LevelElement {

    [Tooltip("How fast the water moves upwards. 1 is slow 10 is really fast")]
    public float speed = 1f;
    
    [Tooltip("An object with a transform component on it. Move this to the top of the water map or to where you'd like the water to end up")]
    public Transform moveToPosition;

 
    private Vector3 startPos; //The starting location of the water
    private bool started = false; //has this thing 


    




    void Awake()
    {
        //Adding 
        LevelReset.AddToLevelElements(this);
        startPos = transform.transform.position;
        speed /= 100f;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.GetComponent<CharController>() != null)
        {
            //  Activate();
            StartCoroutine("Move");

        }
    }

    public override void Reset()
    {
        StopAllCoroutines();
        transform.transform.position = startPos;

    }

    public override void Activate()
    {
        StartCoroutine("Move");
    }


    IEnumerator Move()
    {

        while (transform.transform.position != moveToPosition.position)
        {
            yield return new WaitForFixedUpdate();
            transform.transform.position = Vector3.MoveTowards(transform.transform.position, moveToPosition.position, speed);
        }

        transform.gameObject.SetActive(false);


    }

}

