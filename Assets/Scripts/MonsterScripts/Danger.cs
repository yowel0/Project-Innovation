using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    [SerializeField]
    private string deathMessage;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DeathManager.GetMainManager().KillPlayer(deathMessage);
        }
    }

}
