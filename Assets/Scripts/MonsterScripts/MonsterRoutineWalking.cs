using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterRoutineWalking : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField]
    PathHolder pathHolder;


    Vector3 startPos;

    NavMeshAgent mAgent;

    void Start()
    {
        startPos = transform.position;
        mAgent = GetComponent<NavMeshAgent>();
        NextRandomLocation();

        DeathManager.OnRespawn += ResetPosition;
    }

    private void OnDestroy()
    {
        DeathManager.OnRespawn -= ResetPosition;
    }



    private void FixedUpdate()
    {
        // mAgent needs a few frames to actually calculate a path, which is okay for checking if destination is reached
        float rDistance = RemainingDistance(mAgent.path.corners);
        //Debug.Log("Remaining distance: " + rDistance);


        if (rDistance <= mAgent.stoppingDistance)
        {
            NextRandomLocation();
        }


    }

    void NextRandomLocation()
    {
        Vector3 newDestination = pathHolder.GetRandomPoint().position;
        mAgent.SetDestination(new Vector3(newDestination.x, transform.position.y, newDestination.z));
    }

    // Called by monster specific behaviour
    public void SetDistraction(Vector3 dLocation)
    {
        mAgent.SetDestination(dLocation);
    }


    public void ForcedEncounter(ForcedEncounter location)
    {
        mAgent.Warp(location.GetWarpPos());
        SetDistraction(location.GetDestination());
    }


    // Copied from internet, better calculation of remaining distance for path.
    // mAgent.remainingDistance doesn't like calculating around corners
    public float RemainingDistance(Vector3[] points)
    {
        if (points.Length < 2) return 0;
        float distance = 0;
        for (int i = 0; i < points.Length - 1; i++)
            distance += Vector3.Distance(points[i], points[i + 1]);
        return distance;
    }


    private void ResetPosition()
    {
        mAgent.Warp(startPos);
        NextRandomLocation();
    }



}
