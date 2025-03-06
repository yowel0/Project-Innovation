using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundLoudness", menuName = "Data/SoundLoudness")]
public class SoundLoudness : ScriptableObject
{
    public AudioClip sound;
    public float radius;
    
}
