using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoudnessManager : MonoBehaviour
{

    [SerializeField]
    private SoundLoudness[] soundData;


    [SerializeField]
    private HearingMonster monster;



    public void CheckLoudness(AudioClip clip)
    {
        CheckLoudness(clip.name);

    }

    public void CheckLoudness(string soundID)
    {
        float range = 0;
        Debug.Log("Looking for " + soundID);

        foreach (SoundLoudness sound in soundData)
        {
            Debug.Log("Comparing with " + sound.sound.name);
            if (sound.sound.name == soundID)
            {
                Debug.Log("Name found!");
                range = sound.radius;
                break;
            }
        }

        if (range <= 0)
        {
            Debug.Log("Nothing was found, please check if the name is correct");
            return;
        }

        Debug.Log("Moving on");

        CheckMonsterDistance(range);
    }

    private void CheckMonsterDistance(float soundRange)
    {
        Vector3 playerPos = PlayerMovement.GetPlayer().transform.position;

        float monsterDistance = Vector3.Distance(monster.transform.position, playerPos);

        Debug.Log("monsterDistance: " + monsterDistance);
        Debug.Log("soundRange: " + soundRange);


        if (monsterDistance <= soundRange)
        {
            Debug.Log("Approaching player");
            monster.HearPlayer();
        }
        else
        {
            Debug.Log("Distance is too big, not approaching player");
        }
    }

    public static SoundLoudnessManager GetManager()
    {
        return manager;
    }
    static SoundLoudnessManager manager = null;

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;

            // subscribe to events here if needed
        }
        else
        {
            Debug.Log("Second manager destroys itself");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (manager == this)
        {
            manager = null;
        }
    }



}
