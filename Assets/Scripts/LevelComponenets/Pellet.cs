using UnityEngine;
using System.Collections;

public class Pellet : MonoBehaviour {

    public int moveDirection;

	// Use this for initialization
	void Start () {

        StartCoroutine(destroy());
    }

    // Update is called once per frame
    void Update () {
        
        transform.Translate(new Vector3(moveDirection * Time.deltaTime * 20, 0, 0));

    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);

    }

    void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);
    }

}
