using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement2D : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    Transform gyroTransform;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = gyroTransform.up - Vector3.up * Vector3.Dot(gyroTransform.up.normalized, Vector3.up);
        //print(input);
        rb.AddForce(new Vector3(input.x,input.z) * speed);

        audioSource.volume = rb.velocity.magnitude / 3;
    }
}
