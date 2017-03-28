//Author Zachary Coon||Last edited by Zachary Coon||Date March 9 2017

//This object will show story images once the player has collided with it in order of which they were
//placed in the public array called 'storyImages'
using UnityEngine;
using System.Collections;

public class StoryCollider : MonoBehaviour {

    [Tooltip("An array of GameObjects that are turned off, put game objects in order of appearance. Place images in GUI")]
    public GameObject[] storyImages;
    bool started = false; //Has this object started showing the images
    bool ended = false; //Has this object finished showing the images
    int image_index = 0; // The index of the image that is currently showing
    GameObject player; // The player. It's set when the object is collided with the player


    //If the player collides with this object, then freeze the game and load the first image  
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !ended)
        {
            other.gameObject.SetActive(false);
            storyImages[0].SetActive(true);
            player = other.gameObject;
            started = true;
        }
    }

    //press enter to go to the next image, if there are no more images then unfreeze the game
    private void FixedUpdate()
    {
        if(Input.GetButtonDown("Submit") && started)
        {
            image_index++;
            print(storyImages.Length);
            //if image number is less than the number of images, then load the next image and unload the previous
            if(image_index < storyImages.Length)
            {
                storyImages[image_index - 1].SetActive(false);
                storyImages[image_index].SetActive(true);
            }
            //if the image number is the same as the number of images, then unload the last image and resume the game
            else if(image_index == storyImages.Length)
            {
                storyImages[image_index - 1].SetActive(false);
                ended = true;
                started = false;
                player.SetActive(true);
            }
            else
            {
                Debug.LogError("Unexpected result in StoryCollider in fixedUpdate");
            }
        }
    }
}
