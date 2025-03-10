using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBlocker : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.SetActive(false);
    }
}
