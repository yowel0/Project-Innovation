using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    [SerializeField]
    GameObject exitObj;

    [SerializeField]
    private int identifier = -1;

    Vector3 entrancePos;
    Vector3 exitPos;

    private void Awake()
    {
        entrancePos = transform.position;
        exitPos = exitObj.transform.position;
        
    }

    public void SetIdentifier(int id)
    {
        if (identifier < 0) identifier = id;
    }

    public int GetIdentifier() 
    { 
        return identifier; 
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
