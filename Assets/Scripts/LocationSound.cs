using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocationSound : MonoBehaviour
{
    public UnityEvent OnMonsterEnter;


    [SerializeField] AudioClip monsterEnter;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            OnMonsterEnter?.Invoke();
        }
    }


}
