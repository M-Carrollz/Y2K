using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPrinterSting : MonoBehaviour
{
    private bool hasBeenTriggered = false;
    public GameObject uIController;

    //Runs when the Player moves into this trigger
    void OnTriggerEnter(Collider other)
    {
        //Checks if the tag of the object that enters is "Player"
        if (other.tag == "Player" && !hasBeenTriggered)
        {
            GameObject.FindObjectOfType<UIController>().Dialogue("uhhhh... What was that!?", 3); //Show Dialogue saying the door is locked.
            hasBeenTriggered = true;
        }
    }
}
