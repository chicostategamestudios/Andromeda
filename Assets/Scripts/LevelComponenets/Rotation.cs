//This script was written by James | Last edited by James and Zachary... kinda| Modified on Feb 9, 2017
using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    [Tooltip("This is the speed in which this object rotates in degrees per second.")]
    public float rotation_speed = 50.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Time.deltaTime * rotation_speed, 0, 0);
    }
}