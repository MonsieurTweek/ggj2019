using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Trap
{

    [SerializeField]
    float _laserCooldown = 1.0f;

    [SerializeField]
    float _laserCurrentCooldown = 0.0f;

    [SerializeField]
    bool _isActive = true;

    [SerializeField]
    bool _canBlink = true;

    public override void ActiveTrapFromPlayer() {
        
    }

    public override void ActiveTrapFromTrigger() {
        _canBlink = false;
    }

    protected void Blink() {
        if(_canBlink == false)
        {
            return;
        }
        if(_laserCurrentCooldown <= Time.time)
        {
            _laserCurrentCooldown = Time.time + _laserCooldown;
            this.GetComponent<Collider>().enabled = !_isActive;

            Renderer[] laserRenderers = GetComponentsInChildren<Renderer>();
            foreach(Renderer laserRenderer in laserRenderers)
            {
                laserRenderer.enabled = !_isActive;
            }

            _isActive = !_isActive;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Blink();
        
    }
}
