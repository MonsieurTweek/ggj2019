using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Trap : MonoBehaviour
{

    [SerializeField]
    protected bool _isActive = true;

    public abstract void ActiveTrapFromPlayer();

    public abstract void ActiveTrapFromTrigger();

    public void EnableTrap() {
        _isActive = true;
        transform.root.gameObject.SetActive(true);
    }

    public void DisableTrap() {
        _isActive = false;
        transform.root.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
