using UnityEngine;
using System.Collections;

public class SomeCrapLikeThat : MonoBehaviour {
    private void FixedUpdate()
    {
        if(Random.Range(0, 10000) < 1)
            Debug.LogError("Fuck This Shit I'm OUT!!!!");
    }
    
}
