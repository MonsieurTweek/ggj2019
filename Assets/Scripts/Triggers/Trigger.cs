using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public Trap target;
    public bool needAction = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoActiveTriggerFromPlayer() {
        if(target != null)
        {
            target.ActiveTrapFromPlayer();
        }
        Debug.Log("TRIGGER PLAYER ACTIVATED !");
    }

    public void DoActiveTriggerFromAction() {
        if(target != null)
        {
            target.ActiveTrapFromTrigger();
            Debug.Log("TRIGGER ACTION ACTIVATED !");
        }
    }
}
