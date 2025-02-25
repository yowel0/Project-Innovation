using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseFourObjPlacer : MonoBehaviour
{
    public string objID;
    public GameObject distortedObj;
    public GameObject normalObj;
    public Transform distortedObjLocation;
    public Transform normalObjLocation;

    bool isDistorted = true;

    // Start is called before the first frame update
    void Start()
    {
        //Ask GameManager if objID is in ObjsWithPostItNote
        for (int i = 0; i < GameManager.GetMainManager().objsWithPostItNote.Length; i++)
        {
            if (GameManager.GetMainManager().objsWithPostItNote[i] == objID)
            {
                isDistorted = false;
            }
        }

        if (isDistorted)
        {
            Instantiate(distortedObj, distortedObjLocation.position, distortedObjLocation.rotation);
        }
        else
        {
            Instantiate(normalObj, normalObjLocation.position, normalObjLocation.rotation);
        }

    }


}
