using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrophy : Trigger
{
    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DoActiveTriggerFromAction() {
        gameController.EndGame();
    }
}
