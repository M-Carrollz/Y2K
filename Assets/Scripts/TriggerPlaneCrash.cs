using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlaneCrash : MonoBehaviour
{
    private bool isFirstEnter = true;
    public GameObject uIController;

    void OnTriggerEnter(Collider other)
    {
        //Checks if the tag of the object that enters is "Player"
        if (other.tag == "Player" && isFirstEnter)
        {
            GameObject.FindObjectOfType<UIController>().Dialogue("Ohhh Lord... What Now?.", 4); //Show Dialogue saying the door is locked.
            //Play Camera Shake Animation
            //Dust Particles fall from cieling
            //Player wobble annimation
            isFirstEnter = false;
        }
    }

}
