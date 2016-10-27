using UnityEngine;
using System.Collections;

public class Pellet : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(destroy());
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime * 20);

    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);

    }
}
