using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterRoutineWalking))]
public class HearingMonster : MonoBehaviour
{
    MonsterRoutineWalking mrw;

    private void OnEnable()
    {
        mrw = GetComponent<MonsterRoutineWalking>();
        //PlayerMovement.OnSprint += HearPlayer;
    }

    private void OnDisable()
    {
        //PlayerMovement.OnSprint -= HearPlayer;
    }

    public void HearPlayer()
    {
        mrw.SetDistraction(PlayerMovement.GetPlayer().transform.position);
    }
}
