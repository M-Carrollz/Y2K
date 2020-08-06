using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivateAudio : MonoBehaviour
{
    public GameObject audioTrack;

    //Runs when the Player moves into this trigger
    void OnTriggerEnter(Collider other)
    {
        //Checks if the tag of the object that enters is "Player"
        if (other.tag == "Player")
        {
            audioTrack.SetActive(true);

        }
    }

    void OnTriggerExit(Collider other)
    {
        //Checks if the tag of the object that enters is "Player"
        if (other.tag == "Player")
        {
            audioTrack.SetActive(false);

        }
    }
}
