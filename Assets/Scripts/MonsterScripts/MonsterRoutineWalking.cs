using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterRoutineWalking : MonoBehaviour
{
    [SerializeField]
    Tunnel[] destinations;

    int currentDestination = 0;

    [SerializeField]
    bool isDistracted;

    NavMeshAgent mAgent;

    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        ContinueDestination();
    }

    // ugly test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetDistraction(new Vector3(17, 0, -5));
            Debug.Log("Distracted!");
        }
    }



    private void FixedUpdate()
    {

        if (mAgent.remainingDistance < .1f)
        {
            NextDestination();
        }
    }

    public void ContinueDestination()
    {
        mAgent.SetDestination(destinations[currentDestination].GetEntrancePos());
        isDistracted = false;
    }

    public void NextDestination()
    {
        // Don't pick new destination if it was distracted
        if (!isDistracted)
        {
            // Don't teleport if destination wasn't actually reached
            if (mAgent.pathStatus != NavMeshPathStatus.PathPartial)
            {
                Vector3 tunnelExit = destinations[currentDestination].GetExitPos();
                mAgent.Warp(new Vector3(tunnelExit.x, transform.position.y, tunnelExit.z));
            }
            else
            {
                Debug.Log("Couldn't reach destination, attempting next destination");
            }

            currentDestination++;
            currentDestination %= destinations.Length;
        }

        ContinueDestination();
    }

    // Called by monster specific behaviour
    public void SetDistraction(Vector3 dLocation)
    {
        mAgent.SetDestination(dLocation);
        isDistracted = true;
    }

}
