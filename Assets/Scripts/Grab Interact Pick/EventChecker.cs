using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChecker : MonoBehaviour
{
    public string EventMessage = "Event is triggered";
    public int UsesLeft = 1;
    public bool ConsumeUses = true;

    public void TriggerEvent()
    {
        if (UsesLeft <= 0)
        {
            Debug.Log("Did not interact, there are no more uses left");
            return;
        }
        if (ConsumeUses) UsesLeft--;
        string usesLeftMessage = ConsumeUses ? ". Uses left: " + UsesLeft : ". Did not consume a use.";
        Debug.Log(EventMessage + usesLeftMessage);
    }
}
