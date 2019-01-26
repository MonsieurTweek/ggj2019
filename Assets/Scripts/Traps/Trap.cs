using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Trap : MonoBehaviour
{

    public abstract void ActiveTrapFromPlayer();

    public abstract void ActiveTrapFromTrigger();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
