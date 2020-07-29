using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWindow : MonoBehaviour
{
    public GameObject uIController;

    //Runs when the Player moves into this trigger
    void OnTriggerEnter(Collider other)
    {
        //Checks if the tag of the object that enters is "Player"
        if (other.tag == "Player")
        {
            GameObject.FindObjectOfType<UIController>().Dialogue("Hmmmm... \nI could climb through the window onto those window washing platforms.", 4); //Show Dialogue saying the door is locked.
            GameObject.FindObjectOfType<UIController>().Prompt("Press F to climb through the window");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Checks if the tag of the object that enters is "Player"
        if (other.tag == "Player")
        {
            GameObject.FindObjectOfType<UIController>().Prompt("");

        }
    }


}
