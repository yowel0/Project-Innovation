using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    [SerializeField]
    GameObject exitObj;

    Vector3 entrancePos;
    Vector3 exitPos;

    private void Start()
    {
        entrancePos = transform.position;
        exitPos = exitObj.transform.position;
    }

    public Vector3 GetEntrancePos()
    {
        return entrancePos;
    }
    public Vector3 GetExitPos()
    {
        return exitPos;
    }
}
