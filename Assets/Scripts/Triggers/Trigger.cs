using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public Trap[] targets;
    public Light[] lights;
    public bool needAction = true;
    public bool hideOnTrigger = false;

    [SerializeField]
    protected bool _isActive = true;

    public AudioClip disableLaserSFX;
    public AudioClip enableLightSFX;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        EnableTrigger();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoActiveTriggerFromPlayer() {
        if(_isActive == false)
        {
            return;
        }
        if(targets.Length > 0)
        {
            foreach(Trap target in targets)
            {
                target.ActiveTrapFromPlayer();
            }
        }
        if(hideOnTrigger == true)
        {
            DisableTrigger();
        }
    }

    public virtual void DoActiveTriggerFromAction() {
        if(_isActive == false)
        {
            return;
        }
        if(targets.Length > 0)
        {
            foreach(Trap target in targets)
            {
                target.ActiveTrapFromTrigger();
            }

            // SFX
            _audioSource.clip = disableLaserSFX;
            _audioSource.PlayDelayed(1);

        }
        if(lights.Length > 0)
        {
            foreach(Light light in lights)
            {
                light.enabled = true;
            }

            // SFX
            _audioSource.clip = enableLightSFX;
            _audioSource.PlayDelayed(1);
        }

        if(hideOnTrigger == true)
        {
            DisableTrigger();
        }
    }

    public virtual void EnableTrigger() {
        _isActive = true;

        GetComponent<Renderer>().enabled = true;
        //_isActive = true;
        //transform.gameObject.SetActive(true);
        //if(lights.Length > 0)
        //{
        //    foreach(Light light in lights)
        //    {
        //        light.enabled = false;
        //    }
        //}
    }

    public virtual void DisableTrigger() {
        _isActive = false;
        GetComponent<Renderer>().enabled = false;
        //_isActive = false;
        //transform.gameObject.SetActive(false);
    }
}
