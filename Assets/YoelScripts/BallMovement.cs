using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    Transform gyroTransform;
    [SerializeField]
    float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = gyroTransform.up - Vector3.up * Vector3.Dot(gyroTransform.up.normalized, Vector3.up);
        print(input);
        rb.AddForce(input * speed);
    }
}
