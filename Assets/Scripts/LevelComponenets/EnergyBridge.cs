using UnityEngine;
using System.Collections;

public enum BridgeType {
    On,
    Off,
    Malfunctioning
}

public class EnergyBridge : MonoBehaviour {
    [Tooltip("On means the bridge is on, off meand the bridge is off, malfunctioning means it turns on and then off in intervals")]
    public BridgeType bridgeType;
    BridgeType privateBridgeType;
    [Tooltip("How long the bridge stays on (in seconds). This only is useful if you set the bridge type to malfuctioning")]
    public float OnLength;
    [Tooltip("How long the bridge stays off (in seconds). This only is useful if you set the bridge type to malfuctioning")]
    public float OffLength;

    [Tooltip("An excellent tip may be, that this here varible will stay the way that you currently see...")]
    public GameObject myCollider;
    [Tooltip("An excellent tip may be, that this here varible will stay the way that you currently see...")]
    public GameObject bridge;
    // Use this for initialization
    void Start()
    {
        //If bridge is on, leave the collider on
        //If it's off, turn the collider off,
        //If it's malfuctioning, then repeatedly turn it on and off for the user defined ammount of time.
        privateBridgeType = bridgeType;

        if (bridgeType == BridgeType.On)
        {
            BridgeOn();
        }
        else if (bridgeType == BridgeType.Off)
        {
            BridgeOff();
        }
        else if (bridgeType == BridgeType.Malfunctioning)
        {
            BridgeMalfuctioning();
        }
        else
        {
            Debug.Log("Unexpected Error: Bridge Type doesn't seem to have been set properly");
        }
    }


    // Update is called once per frame
    void Update() {
        //If the bridge type changes for whatever reason, then change the behavior of this bridge
        if(bridgeType != privateBridgeType)
        {
            ChangeBehavoir();
        }
    }
    
    void ChangeBehavoir() {
        //Cancels any invokes that could be happening and then calls start again where it will reinstance the bridge type
        CancelInvoke("BridgeOn");
        CancelInvoke("BridgeOff");
        Start();
    }

    void BridgeOn() {
        //Turns the mesh renderer and collider on
        myCollider.GetComponent<MeshCollider>().enabled = true;
        bridge.GetComponent<MeshRenderer>().enabled = true;
    }

    void BridgeOff() {
        //Turns the mesh renderer and collider off
        myCollider.GetComponent<MeshCollider>().enabled = false;
        bridge.GetComponent<MeshRenderer>().enabled = false;
    }

    void BridgeMalfuctioning() {
        //Turns the bridge on and off based off the ammount of time the user wants the bridge to stay on and off
        InvokeRepeating("BridgeOff", OnLength, OnLength + OffLength);
        InvokeRepeating("BridgeOn", 0f, OnLength + OffLength);
    }
}
