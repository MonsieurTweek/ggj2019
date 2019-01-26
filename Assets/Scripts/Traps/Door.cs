﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Trap
{

    [SerializeField]
    private bool _isOpen = true;

    private Collider[] _doorColliders;

    public override void ActiveTrapFromPlayer() {
        throw new System.NotImplementedException();
    }

    public override void ActiveTrapFromTrigger() {
        throw new System.NotImplementedException();
    }

    public void SwitchDoorState() {
      
        foreach(Collider doorCollider in _doorColliders)
        {
            doorCollider.enabled = _isOpen;
        }
        _isOpen = !_isOpen;
    }

    // Start is called before the first frame update
    void Start()
    {
        _doorColliders = GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
