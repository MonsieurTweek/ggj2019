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
    private bool _isActive = true;

    [SerializeField]
    private bool _canBlink = true;

    private Renderer[] _laserRenderers;
    private Collider _selfCollider;

    public override void ActiveTrapFromPlayer() {
        _canBlink = false;
        _selfCollider.enabled = false;
    }

    public override void ActiveTrapFromTrigger() {
        _canBlink = false;
        _isActive = false;

        _selfCollider.enabled = _isActive;

        foreach(Renderer laserRenderer in _laserRenderers)
        {
            laserRenderer.enabled = _isActive;
        }
    }

    protected void Blink() {
        if(_canBlink == false)
        {
            return;
        }
        if(_laserCurrentCooldown <= Time.time)
        {
            _laserCurrentCooldown = Time.time + _laserCooldown;
            _selfCollider.enabled = !_isActive;
           
            foreach(Renderer laserRenderer in _laserRenderers)
            {
                laserRenderer.enabled = !_isActive;
            }

            _isActive = !_isActive;
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
        Blink();
        
    }
}
