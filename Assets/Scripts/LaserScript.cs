using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    private enum ConsistentSpeedDirection
    {
        X,
        Z,
        Foward
    }

    [SerializeField]
    private ConsistentSpeedDirection consistentSpeedDirection;

    [SerializeField]
    private float speed;

    [SerializeField]
    private LaserPathHolder pathHolder;


    [SerializeField]
    private Transform currentDestination;

    private void Start()
    {
        if (pathHolder == null) Debug.LogWarning("Laser doesn't have a path attached");
        currentDestination = pathHolder.GetFirstPoint();
    }

    private void FixedUpdate()
    {
        float secretRealSpeed = speed / 100f;
        Vector3 diff = currentDestination.position - transform.position;
        diff.y = 0;

        switch (consistentSpeedDirection)
        {
            case ConsistentSpeedDirection.X: MoveConsistentX(secretRealSpeed, diff);
                break;
            case ConsistentSpeedDirection.Z: MoveConsistentZ(secretRealSpeed, diff);
                break;
            case ConsistentSpeedDirection.Foward: MoveConsistentForward(secretRealSpeed, diff);
                break;
        }        

    }


    void MoveConsistentX(float secretRealSpeed, Vector3 diff)
    {
        if (Mathf.Abs(diff.x) <= secretRealSpeed)
        {
            DoNextPath(diff);
        }
        else
        {
            int posOrNeg = diff.x > 0 ? 1 : -1;
            transform.Translate(secretRealSpeed * posOrNeg, 
                                0, 
                                diff.normalized.z * secretRealSpeed / Mathf.Abs(diff.normalized.x));
        }
    }

    void MoveConsistentZ(float secretRealSpeed, Vector3 diff)
    {
        if (Mathf.Abs(diff.z) <= secretRealSpeed)
        {
            DoNextPath(diff);
        }
        else
        {
            int posOrNeg = diff.z > 0 ? 1 : -1;
            transform.Translate(diff.normalized.x * secretRealSpeed / Mathf.Abs(diff.normalized.z), 
                                0, 
                                secretRealSpeed * posOrNeg);
        }
    }

    void MoveConsistentForward(float secretRealSpeed, Vector3 diff)
    {
        if (diff.magnitude <= secretRealSpeed)
        {
            DoNextPath(diff);
        }
        else
        {
            transform.Translate(diff.normalized * secretRealSpeed);
        }
    }

    void DoNextPath(Vector3 diff)
    {
        transform.Translate(diff);
        currentDestination = pathHolder.GetNextPoint();
    }
    void DoNextPath()
    {
        Vector3 diff = currentDestination.position - transform.position;
        diff.y = 0;
        DoNextPath(diff);
    }

}
