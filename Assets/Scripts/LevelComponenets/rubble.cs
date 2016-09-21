using UnityEngine;
using System.Collections;

public class rubble : MonoBehaviour {
    
    public GameObject rub1;
    public GameObject rub2;
    public GameObject rub3;
    public GameObject rub4;
    public GameObject rub5;
    public GameObject rub6;
    public GameObject rub7;

    // Use this for initialization
    void Start () {

            Instantiate(rub1, new Vector3(-405, 18, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(rub2, new Vector3(-406, 18, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(rub3, new Vector3(-407, 18, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(rub4, new Vector3(-406, 18, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(rub5, new Vector3(-406, 18, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(rub6, new Vector3(-406, 18, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(rub7, new Vector3(-406, 18, 0), Quaternion.Euler(0, 180, 0));
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

}