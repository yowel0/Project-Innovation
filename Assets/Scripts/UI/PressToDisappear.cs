using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressToDisappear : MonoBehaviour
{
    [SerializeField] KeyCode disappearKey;

    private void Update()
    {
        if (Input.GetKeyDown(disappearKey))
        {
            gameObject.SetActive(false);
        }
    }
}
