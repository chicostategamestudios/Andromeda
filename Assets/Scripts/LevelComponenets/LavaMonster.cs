using UnityEngine;
using System.Collections;

public class LavaMonster : MonoBehaviour {


	public float lavaDuration;          //time the lava spew is active
    public float waitDuration;          //time between lava spews
	Vector3 startPos;                   //where the AI starts before it rises
	Vector3 targetPos;                  //position where the AI currently wants to go
	Vector3 endPos;                     //the ending position of the AI 
	public float speed;                 //speed the AI rises at
	public float moveUpHeight = 3f;     //how far up the AI moves
	bool canStartLava = false;          //bool to check if the AI can spew lava
	public bool spew;                   //bool to check if the Ai is spewing
	ParticleSystem  mySystem;           //ref to the particle system
	public BoxCollider playerBlocker;   //ref to the spew's collider
    GameObject player;
    public bool tracking;
    Vector3 offset = new Vector3(0, 1, 0);
	// Use this for initialization
    //sets variables needed on start.
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerBlocker.enabled = false;
		mySystem = gameObject.transform.GetComponentInChildren<ParticleSystem> ();
		mySystem.Stop ();
		startPos = transform.position;
		endPos = startPos + new Vector3 (0, moveUpHeight, 0);
		targetPos = startPos;
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (tracking == true)
        {
            mySystem.transform.LookAt(player.transform.position + offset);
        }
        //if current position is not targetPos move torwards that pos.
        if (transform.position != targetPos) {
			transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
		}
        //if target pos is end pos, and current position is end positon and canStartLava is true start the coroutine for spewing lava and set canStartLava false
		if (targetPos == endPos && transform.position == endPos && canStartLava) {
			StartCoroutine("SpewLava");
			canStartLava = false;
		}

    }

    //Starts the timer for spweing lava. turns on the collider for particles and plays the particle system. After waitDuration turn off the collider and particle system
    IEnumerator SpewLava(){
		spew = true;
        mySystem.Play();
        mySystem.transform.LookAt(player.transform.position + offset);
        yield return new WaitForSeconds (lavaDuration);
		spew = false;
        mySystem.Stop ();
		yield return new WaitForSeconds (waitDuration);
		if (transform.position == endPos) {
			StartCoroutine("SpewLava");
		}
	}
	
    //have the AI rise up and allow it to spew lava
	void Awaken(){
		targetPos = endPos;
			canStartLava = true;

	}

    //have the AI go back down and stop spewing lava
	void Sleep(){
		targetPos = startPos;
		StopAllCoroutines ();
		playerBlocker.enabled = false;
		mySystem.Stop ();
	}

    //if player enters the collider around the AI awaken
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			Awaken();
		}

	}
    //If player leaves the collider around the AI go to sleep
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			Sleep();
		}

	}
}
