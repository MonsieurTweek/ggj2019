using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableLocalAudioSource()
    {
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.volume = 1f;
        }
    }

    public void DisableLocalAudioSource()
    {
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = 0f;
        }
    }
}
