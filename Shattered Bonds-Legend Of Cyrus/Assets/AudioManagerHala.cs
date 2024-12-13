using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerHala : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;
    public static AudioManagerHala instance = null;
    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;



    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

   /* public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);//generatesrandom pitch between lowPitchRange highPitchRange
        efxSource.pitch = randomPitch; //to apply random pitch to efxSource.
        efxSource.clip = clips[randomIndex]; //assign random clip from audio lists.
        efxSource.Play();//play selected sound effect

    }*/


    // Update is called once per frame
    void Update()
    {
        
    }
}

