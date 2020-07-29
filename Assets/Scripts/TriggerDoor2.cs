using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor2 : MonoBehaviour
{
    private int doorUnlockAttempts;
    public GameObject uIController;
    public bool doorTriggered = false;

    //Runs when the Player moves into this trigger
    void OnTriggerEnter(Collider other)
    {
        //Checks if the tag of the object that enters is "Player"
        if (other.tag == "Player")
        {
            if (doorUnlockAttempts < 1) // If this is the first door unlock attempt
            {
                GameObject.FindObjectOfType<UIController>().Dialogue("Yeah... The door is blocked, seems about right.\nNow how the heck do I get out of here?", 4); //Show Dialogue saying the door is locked.
                doorUnlockAttempts = 1;
                doorTriggered = true;
            }

            else if (doorUnlockAttempts >= 1) //if this is not the first door unlock attempt
            {
                GameObject.FindObjectOfType<UIController>().ShowMessage("The Door is Blocked", 2); //Show Message saying the door is locked.
                doorUnlockAttempts = +1;
            }

        }
    }
}
