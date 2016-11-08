using UnityEngine;
using System.Collections;

public class FireballDragonStatue : MonoBehaviour {
    [Tooltip("Override for playtesting (or other reasons). Make this true if you want to activate it before the relic is grabbed")]
    public bool isActive;
    [Tooltip("number of seconds in between fireballs")]
    public float waitTime;
    [Tooltip("intial delay of activation after artifact has been collected (in seconds)")]
    public float initialDelay;
    [Tooltip("How long the fireball stays alive (in seconds)")]
    public float lifetimeOfFireball;
    [Tooltip("How fast does the fireball go? 5 is slow, 15 is fast. Anything lower is super slow anything higher is super fast. 10 is a good middle. Negative numbers go left.")]
    public float speedOfFireball;
    Quaternion spawnQ = Quaternion.Euler(0, 0, 0);

	public Transform launchPoint;
	Vector3 launchFrom;

    bool activated = false;
    public GameObject fireball_;
    // Use this for initialization
    void Start()
    {
		if (!launchPoint) {
			launchFrom = this.transform.position;
		}

    }

    // Update is called once per frame
    void Update()
    {
        //If the temple is active (Or activated manually) then start firing the fireballs
        if ((isActive || DragonHeadRelic.isTempleActive) && !activated) {
            isActive = true;
            activated = true;
            InvokeRepeating("SpawnFireball", initialDelay, waitTime);
        }
    }

    void SpawnFireball()
    {
        //Start Instantiating fireballs with the user defined values
		GameObject fireball = (GameObject) Instantiate(fireball_, launchFrom, spawnQ);
		Fireball _fireBall = fireball.GetComponent<Fireball> ();
		_fireBall.lifetime = lifetimeOfFireball;
		_fireBall.speed = speedOfFireball;
		_fireBall.jumpheight = 10;
		_fireBall.gravityRate = 10;
		_fireBall.rotSpeed = .1f;
        fireball.gameObject.SetActive(true);
    }
}
