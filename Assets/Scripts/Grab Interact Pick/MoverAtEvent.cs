using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverAtEvent : MonoBehaviour
{
    bool startMoving = false;
    Rigidbody body;
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void StartMoving()
    {
        startMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
        {
            body.AddForce(new Vector3(10, 0, 0), ForceMode.Force);
            //body.Move(new Vector3(1, 0, 0), Quaternion.identity);
        }
    }
}
