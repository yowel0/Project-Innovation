using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInEffect : MonoBehaviour
{
    [SerializeField]
    Image image;
    float time;
    [SerializeField]
    float timeMax;
    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time < timeMax){
            time += Time.deltaTime;
        }
        image.color = new Color(1,1,1,time/timeMax);
        print(image.color);
    }
}
