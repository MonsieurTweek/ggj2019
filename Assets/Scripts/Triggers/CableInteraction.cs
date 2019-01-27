using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableInteraction : Trigger
{
    public override void EnableTrigger() {
        transform.parent.gameObject.SetActive(true);
        base.EnableTrigger();
    }

    public override void DisableTrigger() {
        transform.parent.gameObject.SetActive(false);
        base.DisableTrigger();
    }
}
