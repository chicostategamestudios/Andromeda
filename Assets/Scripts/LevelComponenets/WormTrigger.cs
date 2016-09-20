using UnityEngine;
using System.Collections;

public class WormTrigger : MonoBehaviour {

    public GameObject worm;
    bool triggered;
    bool up;
    bool down;

    Vector3 originalPos;

    // Use this for initialization
    void Start()
    {
        originalPos = worm.transform.position;
        triggered = false;
        up = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (up == true)
        {
            worm.transform.Translate(0, Time.deltaTime * 10, 0);
        }
        if (down == true)
        {
            worm.transform.Translate(0, -Time.deltaTime * 10, 0);
        }
    }
    void OnTriggerEnter(Collider Col)
    {
        if (Col.tag == "Player" && triggered == false)
        {
            //play alert
            StartCoroutine(UpandDown(2f));
            triggered = true;
        }
    }
    IEnumerator UpandDown(float waittTime)
    {
        up = true;
        yield return new WaitForSeconds(.5f);
        up = false;
        yield return new WaitForSeconds(waittTime);
        down = true;
        yield return new WaitForSeconds(.5f);
        down = false;
    }
}