using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoxCollapse : MonoBehaviour
{
    public bool hasTriggerbeenTriggered = false;

    //Runs when the Player moves into this trigger
    void OnTriggerEnter(Collider other)
    {
        //Checks if the tag of the object that enters is "Player"
        if (other.tag == "Player" && !hasTriggerbeenTriggered)
        {
            //Play Box Falling Annimation
            hasTriggerbeenTriggered = true;

        }
    }

}
