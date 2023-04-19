using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] singleSFXs;
    public Sound[] laughSFXs;
    public Sound[] clockSFXs;
    public Sound[] waterSFXs;
    public AudioSource sfxSource;

    private void Awake()
    {
        if( Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundWithName(string name)
    {
        Sound s = Array.Find(singleSFXs, x => x.name == name);

        if(s == null)
        {
            ExceptionHandler.Throw("Sound not found.");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlaySoundWithName(string name, float volume)
    {
        Sound s = Array.Find(singleSFXs, x => x.name == name);

        if (s == null)
        {
            ExceptionHandler.Throw("Sound not found.");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip, volume);
        }
    }

    public void PlayLaughSound()
    {
        int index = Random.Range(0, laughSFXs.Length);
        sfxSource.PlayOneShot(laughSFXs[index].clip);
    }

    public void PlayClockSound()
    {
        int index = Random.Range(0, clockSFXs.Length);
        sfxSource.PlayOneShot(clockSFXs[index].clip);
    }
    public void PlayWaterSound()
    {
        int index = Random.Range(0, waterSFXs.Length);
        sfxSource.PlayOneShot(waterSFXs[index].clip);
    }
}
