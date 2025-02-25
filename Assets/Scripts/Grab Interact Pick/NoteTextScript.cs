using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteTextScript : MonoBehaviour
{
    public KeyCode EditKey;

    string text = "";
    bool isDone = false;
    void Start()
    {
    }

    
    void Update()
    {
        // Stop/start typing if tab is pressed and is held in hand(placeholder)
        if (Input.GetKeyDown(EditKey) && 
            (/*!GetComponentInParent<GrabbableObjectScript>().placedOnPlacable ||*/
            transform.parent.parent.parent.parent.tag != "IsPostItNotePlacement"))
        {
            isDone = !isDone;
            if (isDone) PlayerMovement.GetPlayer().RestoreMoveSpeed();
            else PlayerMovement.GetPlayer().FreezeMovement();
            return;
        }
        if (/*GetComponentInParent<GrabbableObjectScript>().placedOnPlacable*/
            transform.parent.parent.parent.parent.tag == "IsPostItNotePlacement" && !isDone)
        {
            isDone = true;
            PlayerMovement.GetPlayer().RestoreMoveSpeed();
        }
        if (isDone) return;

        TypeText();
        GetComponent<TextMeshProUGUI>().text = text;
    }

    void TypeText()
    {
        // Backspace functionality
        if (Input.GetKeyDown(KeyCode.Backspace) && text.Length > 0)
        {
            text = text.Substring(0, text.Length - 1);
            return;
        }

        // Adding the typed letters to the text
        string input = Input.inputString;
        if (input.Equals("")) return; // If not typing, nothing happens
        text += input;

    }
}
