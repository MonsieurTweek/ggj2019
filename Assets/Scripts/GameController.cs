using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public PlayerController player;
    public GameObject spawner;

    public int deathCounter;

    // Start is called before the first frame update
    void Start()
    {
        deathCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DieAndRetry()
    {
        deathCounter++;
        player.Reset();
        player.transform.position = spawner.transform.position;
    }
}
