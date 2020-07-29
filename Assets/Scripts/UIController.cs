using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text messageText; //Message text to be displayed
    public GameObject messagePanel; //Reference to parent Object that the Message text is attached to.
    private float displayTimer; //Display Timer
    private float displayLength; //Variable for how long the message is displayed
    private bool isShowingMessage = false; //Variable for is message displayed

    public Text dialogueText; //Dialogue text to be displayed
    public GameObject dialoguePanel;
    private float dialogueDisplayTimer; //Display Timer
    private float dialogueDisplayLength; //Variable for how long the dialogue is displayed
    private bool isShowingDialogue = false; //Variable for is dialogue displayed

    public Text promptText; //Prompt text to be displayed
    public GameObject promptPanel;
    private bool isShowingPrompt = false; //Variable for is prompt displayed

    /* -------------------------------------------------------------------- Functions ------------------------------------------------------------------- */

    //Function that is called from other scripts and is passed a string (message) and a float (How long to display)
    public void ShowMessage(string message, float duration = 3)
    {
        messagePanel.SetActive(true); //Set the Message Panel to Active
        messageText.text = message; //Change the Displayed text
        isShowingMessage = true; //Set to is Showing Message
        //Set a timer to removed displayed message
        displayLength = duration; //Set display duration
        displayTimer = Time.time; //Set timer and how time is calculated
    }

    //Function that is called from other scripts and is passed a string (message) and a float (How long to display)
    public void Dialogue(string dialogue, float duration = 3)
    {
        dialoguePanel.SetActive(true); //Set the Dialogue Panel to Active
        dialogueText.text = dialogue; //Change the Displayed text
        isShowingDialogue = true; //Set to is Showing dialogue
        //Set a timer to removed displayed dialogue
        dialogueDisplayLength = duration; //Set display duration
        dialogueDisplayTimer = Time.time; //Set timer and how time is calculated
    }

    //Function that is called from other scripts and is passed a string (message) and a float (How long to display)
    public void Prompt(string prompt)
    {
        promptPanel.SetActive(true); //Set the Dialogue Panel to Active
        promptText.text = prompt; //Change the prompt text
        isShowingPrompt = true; //Set to is Showing prompt
    }

    public void EndPrompt()
    {
        if (isShowingPrompt) //If the Prompt panel is currently visible
        {
            //Deativate Message Panel
            promptPanel.SetActive(false); //Turn off Prompt Panel
            isShowingPrompt = false; //Set is Showing Prompt Bool to false

        }
    }

    /* ----------------------------------------------------------------- Update  Function ------------------------------------------------------------- */

    // Update is called once per frame
    void Update()
    {
        if (isShowingMessage) //If the Message panel is currently visible
        {
            //If the time passed is MORE THAN the display length (allows it to display inclusive of the frame it hits the display length)
            if (Time.time - displayTimer > displayLength)
            {
                //Deativate Message Panel
                messagePanel.SetActive(false); //Turn off Message Panel
                isShowingMessage = false; //Set is Showing MEssage Bool to false
            }
        }

        if (isShowingDialogue) //If the Message panel is currently visible
        {
            //If the time passed is MORE THAN the display length (allows it to display inclusive of the frame it hits the display length)
            if (Time.time - dialogueDisplayTimer > dialogueDisplayLength)
            {
                //Deativate Message Panel
                dialoguePanel.SetActive(false); //Turn off Dialogue Panel
                isShowingDialogue = false; //Set is Showing Dialogue Bool to false
            }
        }

    }
}
