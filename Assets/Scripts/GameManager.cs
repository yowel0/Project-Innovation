using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Dictionaries to track object links and interactions by string ID
    private Dictionary<string, bool> objectIDLinks = new Dictionary<string, bool>();
    private Dictionary<string, bool> interactedWithInteractables = new Dictionary<string, bool>();

    public Dictionary<string, string> correctlyPlacedObjectDialogues = new Dictionary<string, string>();

    public string[] objsWithPostItNote = null;


    public static GameManager GetMainManager()
    {
        return mainManager;
    }
    static GameManager mainManager = null;

    private void Awake()
    {
        if (mainManager == null)
        {
            DontDestroyOnLoad(gameObject);
            mainManager = this;

            // subscribe to events here if needed
        }
        else
        {
            Debug.Log("Second game manager destroys itself");
            Destroy(gameObject);
        }
    }

    public bool CheckIDLink(string id)
    {
        // If the ID doesn't exist in the dictionary, return false
        if (objectIDLinks.ContainsKey(id))
            return objectIDLinks[id];
        return false;
    }

    public bool IsInteractedWith(string id)
    {
        // If the ID doesn't exist in the dictionary, return false
        if (interactedWithInteractables.ContainsKey(id))
            return interactedWithInteractables[id];
        return false;
    }

    // Called by GrabbableObjectScript or Interactable
    public void CorrectObjectIDLink(string id)
    {
        if (!objectIDLinks.ContainsKey(id))
        {
            objectIDLinks.Add(id, true);
        }
        else
        {
            objectIDLinks[id] = true;
        }

        Debug.Log("CorrectObjectIDLink");

        if (correctlyPlacedObjectDialogues.ContainsKey(id))
        {
            DialogueSystem.GetMainDialogueSystem().HandleText(correctlyPlacedObjectDialogues[id], 5);
        }
    }

    public void WrongObjectIDLink(string id)
    {
        if (!objectIDLinks.ContainsKey(id))
        {
            objectIDLinks.Add(id, false);
        }
        else
        {
            objectIDLinks[id] = false;
        }

        Debug.Log("WrongObjectIDLink");
    }

    public void InteractedWithInteractable(string id)
    {
        if (!interactedWithInteractables.ContainsKey(id))
        {
            interactedWithInteractables.Add(id, true);
        }
        else
        {
            interactedWithInteractables[id] = true;
        }

        DialogueSystem.GetMainDialogueSystem().InteractionCompleted(id);
    }


    public void LoadScene(string sceneName)
    {
        Debug.Log("Loading scene");
        
        SceneManager.LoadScene(sceneName);
    }



}
