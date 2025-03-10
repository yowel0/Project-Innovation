using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsGyroRotation : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float rotationSpeed = 1;
    Quaternion rotationGoal;

    void OnEnable()
    {
        WS_Client.GyroscopeChanged += SetRotationGoal;
    }

    void OnDisable()
    {
        WS_Client.GyroscopeChanged -= SetRotationGoal;
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation,rotationGoal,rotationSpeed);
    }

    void SetRotationGoal(Quaternion _rotation){
        rotationGoal = _rotation;
    }
}
