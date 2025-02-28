using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPathHolder : MonoBehaviour
{
    [Header("These are set automatically")]
    [SerializeField]
    private Transform[] pointLocations;

    [SerializeField]
    private int numberOfPoints;

    [SerializeField]
    private int currentPointIndex = 0;
    private void OnEnable()
    {
        pointLocations = GetComponentsInChildren<Transform>(true);
        numberOfPoints = pointLocations.Length;
        /*foreach (Transform t in pointLocations)
        {
            t.gameObject.SetActive(false);
        }*/
    }

    public Transform GetFirstPoint()
    {
        return pointLocations[0];
    }
    public Transform GetNextPoint()
    {
        currentPointIndex++;
        currentPointIndex %= numberOfPoints;
        return pointLocations[currentPointIndex];
    }
}
