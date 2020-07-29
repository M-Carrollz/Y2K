using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor1 : MonoBehaviour
{
    private int doorUnlockAttempts;
    public GameObject uIController;

    //Runs when the Player moves into this trigger
    void OnTriggerEnter(Collider other)
    {
        //Checks if the tag of the object that enters is "Player"
        if (other.tag == "Player")
        {
            if ( doorUnlockAttempts < 1) // If this is the first door unlock attempt
            {
                GameObject.FindObjectOfType<UIController>().Dialogue("Great!... My Keypass doesn't work. \nI'll have to take the emergency stairs.", 4); //Show Dialogue saying the door is locked.
                doorUnlockAttempts = 1;
            }

            else if (doorUnlockAttempts >= 1) //if this is not the first door unlock attempt
            {
                GameObject.FindObjectOfType<UIController>().ShowMessage("The Door is Locked", 2); //Show Message saying the door is locked.
                doorUnlockAttempts =+ 1;
            }

        }
    }
}
