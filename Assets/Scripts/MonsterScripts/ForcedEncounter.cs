using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedEncounter : MonoBehaviour
{
    [SerializeField]
    private Tunnel destination;
    

    public Tunnel GetDestination()
    {
        return destination;
    }

}
