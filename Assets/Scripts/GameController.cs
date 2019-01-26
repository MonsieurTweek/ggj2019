﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    const int START_SCENE_ID = 0;
    const int LOADING_SCENE_ID = 1;
    const int MAIN_SCENE_ID = 2;
    const int END_SCENE_ID = 3;

    public PlayerController player;
    public GameObject spawner;

    private static int _deathCounter = 0;
    private static bool _isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Death counter : " + _deathCounter);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DieAndRetry()
    {
        _deathCounter++;
        player.Reset();
        player.transform.position = spawner.transform.position;
    }

    public void EndGame()
    {
        SceneManager.LoadScene(END_SCENE_ID);
    }

    public int GetDeathCounter()
    {
        return _deathCounter;
    }
}
