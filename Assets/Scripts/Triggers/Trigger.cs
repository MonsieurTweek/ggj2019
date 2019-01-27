using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public Trap[] targets;
    public Light[] lights;
    public bool needAction = true;
    public bool hideOnTrigger = false;

    [SerializeField]
    protected bool _isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        EnableTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoActiveTriggerFromPlayer() {
        if(_isActive == false)
        {
            return;
        }
        if(targets.Length > 0)
        {
            foreach(Trap target in targets)
            {
                target.ActiveTrapFromPlayer();
            }
           
            if(hideOnTrigger == true)
            {
                DisableTrigger();
            }
            Debug.Log("TRIGGER PLAYER ACTIVATED !");
        }
        
    }

    public virtual void DoActiveTriggerFromAction() {
        if(_isActive == false)
        {
            return;
        }
        if(targets.Length > 0)
        {
            foreach(Trap target in targets)
            {
                target.ActiveTrapFromTrigger();
            }
         }
        if(lights.Length > 0)
        {
            foreach(Light light in lights)
            {
                light.enabled = true;
            }
        }

        if(hideOnTrigger == true)
        {
            DisableTrigger();
        }
    }

    public virtual void EnableTrigger() { 
        _isActive = true;
        transform.gameObject.SetActive(true);

        if(lights.Length > 0)
        {
            foreach(Light light in lights)
            {
                light.enabled = false;
            }
        }
    }

    public virtual void DisableTrigger() {
        _isActive = false;
        transform.gameObject.SetActive(false);
    }
}
