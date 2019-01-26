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

    public void DoActiveTrigger() {
        if(target != null)
        {
            target.ActiveTrapFromTrigger();
        }
        Debug.Log("TRIGGER ACTIVATED !");
    }
}
