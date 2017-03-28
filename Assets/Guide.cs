//This script was written by Josh | Last edited by Josh | Modified on 17 February 2017
using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;
using Assets.Scripts.Character;

    public enum GuideType{
        DoubleJump,
        WallJump,
        Dash,
        Slash
    }

public class Guide : MonoBehaviour
{
    [Tooltip("Set to Relic type needed to guide Player")]
    public GuideType guideType;
    private Light lightObject;
    private bool checkForTrue;

    // Instantiate variables
    void Start()
    {
        lightObject = gameObject.GetComponent<Light>();
        lightObject.range = 0;
    }

    void OnTriggerEnter(Collider col)
    {
        // check to see if the object colliding is the Player
        if (col.tag == "Player")
        {
            // If the GuideType is DoubleJump the same and the Player has unlocked jumpRelic
            if (guideType == GuideType.DoubleJump && col.GetComponent<RelicManager>().jumpRelic == true)
            {
                lightObject.color = Color.yellow; // Light up object with this color
                lightObject.range = 3; // Set the range of the light (i.e. the radius of visible light)
            }

            // If the GuideType is WallJump the same and the Player has unlocked wallJumpRelic
            if (guideType == GuideType.WallJump && col.GetComponent<RelicManager>().wallJumpRelic == true)
            {
                lightObject.color = Color.green; // Light up object with this color
                lightObject.range = 3; // Set the range of the light (i.e. the radius of visible light)
            }

            // If the GuideType is Dash the same and the Player has unlocked dashRelic
            if (guideType == GuideType.Dash && col.GetComponent<RelicManager>().dashRelic == true)
            {
                lightObject.color = Color.red; // Light up object with this color
                lightObject.range = 3; // Set the range of the light (i.e. the radius of visible light)
            }

            // If the GuideType is Slash the same and the Player has unlocked slashRelic
            if (guideType == GuideType.Slash && col.GetComponent<RelicManager>().slashRelic == true)
            {
                lightObject.color = Color.cyan; // Light up object with this color
                lightObject.range = 3; // Set the range of the light (i.e. the radius of visible light)
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        // checks to see if the player is leaving the collider
        if (col.tag == "Player")
        {
            lightObject.range = 0; // sets Light's radius so it is not visible
        }
    }
}