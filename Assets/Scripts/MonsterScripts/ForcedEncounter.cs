using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedEncounter : MonoBehaviour
{
    [SerializeField]
    private Transform destination;

    private void Start()
    {
        destination = GetComponentInChildren<Transform>();
    }
    public Vector3 GetWarpPos()
    {
        return transform.position;
    }
    public Vector3 GetDestination()
    {
        return destination.position;
    }

}
