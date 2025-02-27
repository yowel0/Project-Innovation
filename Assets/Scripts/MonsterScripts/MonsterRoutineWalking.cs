using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterRoutineWalking : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField]
    Tunnel[] destinations;

    // Distance in units
    [SerializeField]
    float distanceFocusedOnDistraction;

    [Header("Set by code, shown for debugging purposes")]
    [SerializeField]
    bool isDistracted;

    [SerializeField]
    float distractionStartingDistance;

    // This points at the current destination in the destinations array
    int currentDestination = 0;

    // 1 second cooldown before checking if distraction is forgotten
    float distractionMinTime = 1;
    float distractionMinTimeLeft;

    NavMeshAgent mAgent;

    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        ContinueDestination();
    }

    // Ugly hardcoded test for distractions
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
        // mAgent needs a few frames to actually calculate a path, which is okay for checking if destination is reached
        float rDistance = RemainingDistance(mAgent.path.corners);
        //Debug.Log("Remaining distance: " + rDistance);

        // If the agent doesn't have a path, the remaining distance is 0. Agent needs time to calculate path
        if (rDistance < .1f && mAgent.hasPath)
        {
            NextDestination();
        }


        if (isDistracted)
        {
            // Timer to give mAgent enough time to calculate its path
            distractionMinTimeLeft -= Time.fixedDeltaTime;

            // If traveled longer than it has the attention span for and has been distracted for at least a second
            float distanceTraveled = distractionStartingDistance - rDistance;
            if (distanceTraveled > distanceFocusedOnDistraction && distractionMinTimeLeft <= 0)
            {
                ContinueDestination();
                Debug.Log("Forgot distraction");
            }

        }

    }

    public void ContinueDestination()
    {
        mAgent.SetDestination(destinations[currentDestination].GetEntrancePos());
        isDistracted = false;
    }

    public void NextDestination()
    {
        // Don't pick new destination if it was distracted and reached its destination
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


        NavMeshPath path = new NavMeshPath();

        if (NavMesh.CalculatePath(transform.position, dLocation, NavMesh.AllAreas, path))
        {
            Debug.Log("The path was calculated correctly");
            distractionStartingDistance = RemainingDistance(path.corners);
        }
        else
        {
            Debug.Log("The distance of the path is probably incorrect");
            distractionStartingDistance = RemainingDistance(mAgent.path.corners);

        }

        // Adding a cooldown to being able to forget distractions to give mAgent time to calculate a path
        distractionMinTimeLeft = distractionMinTime;
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

}
