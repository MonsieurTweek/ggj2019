using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : Trap
{

    private Animator _animator;

    public override void ActiveTrapFromPlayer() {
        _animator.SetBool("isTriggered", true);
    }

    public override void ActiveTrapFromTrigger() {
        //throw new System.NotImplementedException();
    }

    public override void EnableTrap() {
        _animator.SetBool("isTriggered", false);
        base.EnableTrap();
    }

    public override void DisableTrap() {
        _animator.SetBool("isTriggered", false);
        base.DisableTrap();
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("isTriggered", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
