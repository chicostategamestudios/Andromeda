using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

public class Turtle : LevelElement {

    Vector3 startPos;

    bool started = false;

    public float speed;

    public Transform BirdObj;

    public Transform moveToPosition;



    void Awake()
    {
        LevelReset.AddToLevelElements(this);
        startPos = BirdObj.transform.position;
    }


    void OnDrawGizmosSelected() //this is set up so we can easily visualize the kill center in editor, it doesn't actually "do" anything
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(BirdObj.position, moveToPosition.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.transform.GetComponent<CharController>() != null)
        {
            //  Activate();
            StartCoroutine("Move");
           
        }
    }

    public override void Reset()
    {
        StopAllCoroutines();
        BirdObj.transform.position = startPos;

    }

    public override void Activate()
    {
        StartCoroutine("Move");
    }


    IEnumerator Move()
    {
       
        while (BirdObj.transform.position != moveToPosition.position) 
        {
            yield return new WaitForFixedUpdate();
            BirdObj.transform.position = Vector3.MoveTowards(BirdObj.transform.position, moveToPosition.position, speed);
        }
      
        BirdObj.gameObject.SetActive(false);


    }

}
