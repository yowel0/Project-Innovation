using System.Collections;
using System.Collections.Generic; // Make sure to include this for using List
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private DialogueSystem dialogueSystem; // Reference to the dialogue system

    public float timer = 2f; // Duration to display the dialogue
    public string dialogue = "Dialogue"; // Dialogue text to display

    [SerializeField] private bool isRepetitive = false; // Is the dialogue repetitive?
    [SerializeField] private float repeatInterval = 30f; // Time after which dialogue reappears

    [SerializeField] private List<string> interactionsToEnable = new List<string>(); // List of interactions that enable this dialogue
    [SerializeField] private List<string> interactionsToDisable = new List<string>(); // List of interactions that disable this dialogue

    private bool interactionCompleted = false; // Track whether the required interaction is done

    // Method to set up the dialogue system and text object
    public void SetUp(DialogueSystem _dialogueSystem)
    {
        dialogueSystem = _dialogueSystem;
    }

    // Method called when another collider enters the trigger collider attached to this object
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player and if any required interaction to enable has been completed
        if (other.gameObject.CompareTag("Player") && IsAnyEnableInteractionCompleted())
        {
            // Handle the dialogue text and display duration
            dialogueSystem.HandleText(dialogue, timer);

            // If it's repetitive and the interaction is not completed, set it to repeat
            if (isRepetitive && !interactionCompleted)
            {
                StartCoroutine(RepeatDialogue());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    // Coroutine to repeat the dialogue after a delay
    private IEnumerator RepeatDialogue()
    {
        yield return new WaitForSeconds(repeatInterval);

        if (!interactionCompleted)
        {
            dialogueSystem.HandleText(dialogue, timer);

            // Restart the coroutine if the interaction is still not done
            StartCoroutine(RepeatDialogue());
        }
    }

    // Check if any enable interactions have been completed
    private bool IsAnyEnableInteractionCompleted()
    {
        // If there are no interactions needed to enable, return true
        if (interactionsToEnable.Count == 0) return true;

        // Check if any required interaction is completed
        foreach (string interaction in interactionsToEnable)
        {
            if (GameManager.GetMainManager().IsInteractedWith(interaction))
            {
                return true; // Return true if at least one interaction is completed
            }
        }
        return false; // None of the required interactions are completed
    }

    // This method can be called when the specific interaction is completed
    public void CompleteInteraction()
    {
        // Check if any of the disable interactions have been completed
        foreach (string interaction in interactionsToDisable)
        {
            if (GameManager.GetMainManager().IsInteractedWith(interaction))
            {
                interactionCompleted = true;
                Destroy(gameObject); // Optionally destroy the game object if the dialogue should stop
                return;
            }
        }
    }

    // Public getter for interactionsToDisable
    public List<string> GetInteractionsToDisable()
    {
        return interactionsToDisable;
    }
}
