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


    NavMeshAgent mAgent;

    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        ContinueDestination();
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
    }

    public void NextDestination()
    {
        //GetComponent<Rigidbody>().detectCollisions = false;
        //GetComponentInChildren<Collider>().enabled = false;
        Vector3 tunnelExit = destinations[currentDestination].GetExitPos();

        //Debug.Log("Current position: " + transform.position);

        //transform.position = new Vector3(tunnelExit.x, transform.position.y, tunnelExit.z);

        mAgent.Warp(new Vector3(tunnelExit.x, transform.position.y, tunnelExit.z));

        //Debug.Log("New position: " + transform.position);
        //Debug.Log("Should be: " + tunnelExit);

        //GetComponent<Rigidbody>().detectCollisions = true;
        //GetComponentInChildren<Collider>(true).enabled = true;

        currentDestination++;
        currentDestination %= destinations.Length;
        ContinueDestination();
    }

}
