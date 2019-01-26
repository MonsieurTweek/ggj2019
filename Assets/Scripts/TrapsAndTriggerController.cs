using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsAndTriggerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableAllTraps() {

    }

    public void DisableAllTraps() {

    }

    public void EnableAllTrigers() {
        GameObject[] triggersGO = GameObject.FindGameObjectsWithTag("Trigger");
        foreach(GameObject go in triggersGO)
        {
            go.GetComponent<Trigger>().EnableTrigger();
        }
    }

    public void DisableAllTrigers() {
        GameObject[] triggersGO = GameObject.FindGameObjectsWithTag("Trigger");
        foreach(GameObject go in triggersGO)
        {
            go.GetComponent<Trigger>().EnableTrigger();
        }
    }
}
