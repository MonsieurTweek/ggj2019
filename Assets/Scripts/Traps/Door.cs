using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Trap
{
    public float animationSpeed = 1.0f;

    [SerializeField]
    private bool _isOpen = true;

    private Collider[] _doorColliders;

    public override void ActiveTrapFromPlayer() {
        PlaySFX(KeySFX.Death);
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

        Animator animator = gameObject.GetComponent<Animator>();
        animator.speed = animationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
