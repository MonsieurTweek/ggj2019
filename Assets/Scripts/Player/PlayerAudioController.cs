using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{

    public enum KeySFX
    {
        Walk,
        Dash,
        Death
    }

    public AudioClip walkSFX;
    public AudioClip dashSFX;
    public AudioClip deathSFX;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(KeySFX key)
    {

        AudioClip newClip = null;
        bool playOnLoop = false;

        switch(key)
        {
            case KeySFX.Walk:
                newClip = walkSFX;
                playOnLoop = true;
                break;
            case KeySFX.Dash:
                newClip = dashSFX;
                break;
            case KeySFX.Death:
                newClip = deathSFX;
                break;
        }

        if(newClip != null && (playOnLoop == false || audioSource.isPlaying == false))
        {
            audioSource.clip = newClip;
            audioSource.loop = playOnLoop;
            audioSource.Play();
        }

    }

    public void StopSFX()
    {
        audioSource.Stop();
    }
}
