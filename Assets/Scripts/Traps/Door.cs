using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Trap
{
    public float animationSpeed = 1.0f;

    [SerializeField]
    private bool _isOpen = true;

    private Collider[] _doorColliders;
    private Animator _animator;

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

    public void StartSound()
    {
        PlaySFX(KeySFX.Ambiance);
    }

    // Start is called before the first frame update
    void Start()
    {
        _doorColliders = GetComponentsInChildren<Collider>();

        _animator = gameObject.GetComponent<Animator>();
        _animator.speed = animationSpeed;
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
