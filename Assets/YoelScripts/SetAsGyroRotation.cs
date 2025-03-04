using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsGyroRotation : MonoBehaviour
{
    [SerializeField]
    WS_Client client;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float rotationSpeed = 1;
    Quaternion rotationGoal;

    void OnEnable()
    {
        client.GyroscopeChanged += SetRotationGoal;
    }

    void OnDisable()
    {
        client.GyroscopeChanged -= SetRotationGoal;
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation,rotationGoal,rotationSpeed);
    }

    void SetRotationGoal(Quaternion _rotation){
        rotationGoal = _rotation;
    }
}
