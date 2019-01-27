using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableInteraction : Trigger
{
    public override void EnableTrigger() {
        _isActive = true;
        transform.parent.GetComponent<Trigger>().EnableTrigger();
    }

    public override void DisableTrigger() {
        _isActive = false;
        transform.parent.GetComponent<Trigger>().DisableTrigger();
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }
}
