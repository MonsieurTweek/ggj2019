using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Trap
{

    [SerializeField]
    private float _laserCooldown = 1.0f;

    [SerializeField]
    private float _laserCurrentCooldown = 0.0f;

    [SerializeField]
    private bool _blinkState = true;

    [SerializeField]
    private bool _canBlink = true;

    private Renderer[] _laserRenderers;
    private Collider _selfCollider;

    public override void ActiveTrapFromPlayer() {
        //_canBlink = false;
    }

    // Désactive le laser via un interrupteur
    public override void ActiveTrapFromTrigger() {
        if(_isActive == false)
        {
            return;
        }

        _canBlink = false;
        _blinkState = false;

        _selfCollider.enabled = _blinkState;
        StopSFX();

        foreach (Renderer laserRenderer in _laserRenderers)
        {
            laserRenderer.enabled = _blinkState;
        }
    }

    protected void Blink() {
        if(_canBlink == false)
        {
            if(audioSource.isPlaying == false)
            {
                //audioSource.loop = true;
                //PlaySFX(KeySFX.Ambiance);
            }
            return;
        }
        if(_laserCurrentCooldown <= Time.time)
        {
            _laserCurrentCooldown = Time.time + _laserCooldown;
            _selfCollider.enabled = !_blinkState;

            if(_selfCollider.enabled == true)
            {
                PlaySFX(KeySFX.Enable);
            } else
            {
                StopSFX();
            }
           
            foreach(Renderer laserRenderer in _laserRenderers)
            {
                laserRenderer.enabled = !_blinkState;

            }

            _blinkState = !_blinkState;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _laserRenderers = GetComponentsInChildren<Renderer>();
        _selfCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isActive == false)
        {
            return;
        }
        Blink();
        
    }
}
