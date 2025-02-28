using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(Input.gyro.enabled);
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Input.gyro.attitude;
        print(Input.gyro.attitude);
    }
}
