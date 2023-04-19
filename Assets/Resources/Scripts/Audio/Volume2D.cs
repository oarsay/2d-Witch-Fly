using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume2D : MonoBehaviour
{
    [HideInInspector] public static Transform listenerTransform;
    public AudioSource[] audioSources;
    public float minDist = 1;
    public float maxDist = 400;

    private void Awake()
    {
        listenerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
    }

    void Update()
    {
        float currentVolume = GetVolumeValueAccordingToDistance(transform, minDist, maxDist);
        
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = currentVolume;
        }   
    }

    // Use transforms for calculations
    public static float GetVolumeValueAccordingToDistance(Transform sourceTransform, float minDistance, float maxDistance)
    {
        float dist = Vector2.Distance(sourceTransform.position, listenerTransform.position);

        if (dist < minDistance)
        {
            return 1;
        }
        else if (dist > maxDistance)
        {
            return 0;
        }
        else
        {
            return 1 - ((dist - minDistance) / (maxDistance - minDistance));
        }
    }

    // Use positions for calculations
    public static float GetVolumeValueAccordingToDistance(Vector3 sourcePosition, float minDistance, float maxDistance)
    {
        float dist = Vector2.Distance(sourcePosition, listenerTransform.position);

        if (dist < minDistance)
        {
            return 1;
        }
        else if (dist > maxDistance)
        {
            return 0;
        }
        else
        {
            return 1 - ((dist - minDistance) / (maxDistance - minDistance));
        }
    }
}