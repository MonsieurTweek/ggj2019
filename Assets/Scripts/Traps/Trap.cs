using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Trap : MonoBehaviour
{

    public enum KeySFX
    {
        Enable,
        Ambiance,
        Disable,
        Death
    }

    [SerializeField]
    protected bool _isActive = true;
    [SerializeField]
    protected bool _resetActiveOnInit = true;

    public AudioSource audioSource;
    public AudioClip enableSFX;
    public AudioClip ambianceSFX;
    public AudioClip disableSFX;
    public AudioClip deathSFX;

    public bool isActive() {
        return _isActive;
    }

    public abstract void ActiveTrapFromPlayer();

    public abstract void ActiveTrapFromTrigger();

    public virtual void EnableTrap() {
        if(_resetActiveOnInit == true)
        {
            _isActive = true;
        }
        
        transform.root.gameObject.SetActive(true);
    }

    public virtual void DisableTrap() {
        _isActive = false;
        transform.root.gameObject.SetActive(false);
    }

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

        switch (key)
        {
            case KeySFX.Enable:
                newClip = enableSFX;
                break;
            case KeySFX.Ambiance:
                newClip = ambianceSFX;
                break;
            case KeySFX.Disable:
                newClip = disableSFX;
                break;
            case KeySFX.Death:
                newClip = deathSFX;
                break;
        }

        if (newClip != null && (playOnLoop == false || audioSource.isPlaying == false))
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
