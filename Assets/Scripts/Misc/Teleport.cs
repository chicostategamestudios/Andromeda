using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
    [Tooltip("This transform is attached to this object. Drag this transform to the location you wish this object to teleport the player to when they collide with this object.")]
    public Transform teleportLocation;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.GetComponent<Transform>().position = teleportLocation.position;
        }
    }
}
