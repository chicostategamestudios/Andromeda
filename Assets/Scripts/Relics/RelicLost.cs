//Original Author: Ying Xiong | Last Edited: Ying Xiong | Modified on Feb 09, 2017
//This script's purpose is to play a particle effect when a relic is lost

using UnityEngine;
using System.Collections;

public class RelicLost : MonoBehaviour {

    ParticleSystem relicParticle;
    public string[] colors; // An array to hold colors
    public int colorSwitcher; // int to determine which color should be played next on particle FX

    // Use this for initialization
    void Start() {

        colorSwitcher = 0;
        /*
         * colors = new string[4];
        colors[0] = "blue";
        colors[1] = "red";
        colors[2] = "green";
        colors[3] = "yellow";
        */
        relicParticle = GetComponent<ParticleSystem>();
        //relicParticle.Play();
	
	}

    public void PlayParticle()
    {
        SwitchParticleColor();
        relicParticle.Play();
    }

    public void SwitchParticleColor()
    {
        switch(colorSwitcher)
        {
            case 0:
                relicParticle.startColor = Color.blue;
                colorSwitcher += 1;
                break;
            case 1:
                relicParticle.startColor = Color.red;
                colorSwitcher += 1;
                break;
            case 2:
                relicParticle.startColor = Color.green;
                colorSwitcher += 1;
                break;
            case 3:
                relicParticle.startColor = Color.yellow;
                colorSwitcher = 0;
                break;
            default:
                Debug.LogError("A color is trying to be called that does not go with the relics.. Thanks");
                break;


        }
    }
}
