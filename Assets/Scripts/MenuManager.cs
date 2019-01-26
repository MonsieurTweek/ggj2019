using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    const int START_SCENE_ID = 0;
    const int LOADING_SCENE_ID = 1;
    const int MAIN_SCENE_ID = 2;
    const int END_SCENE_ID = 3;

    public enum SceneID
    {
        Start, Loading, Main, End
    }
    public SceneID nextScene;

    [Header("----- Global -----")]
    public bool isReadyOnLoad;
    public float fakeLoadingTime = 3f;
    public Text textToDisplayWithDelay;
    [Header("----- End Screen -----")]
    public GameController gameController;
    public Text deathCounterTextArea;

    private bool _isReady = false;
    private int _nextSceneId;

    private float _timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        if(isReadyOnLoad == true)
        {
            _isReady = true;
        } else
        {
            _timeLeft = fakeLoadingTime;
        }

        switch(nextScene)
        {
            case SceneID.Start:
                _nextSceneId = START_SCENE_ID;
                break;

            case SceneID.Loading:
                _nextSceneId = LOADING_SCENE_ID;
                break;

            case SceneID.Main:
                _nextSceneId = MAIN_SCENE_ID;
                break;

            case SceneID.End:
                _nextSceneId = END_SCENE_ID;
                break;
        }

        if(deathCounterTextArea != null)
        {
            int deathCounter = gameController.GetDeathCounter();
            if (deathCounter > 1)
            {
                deathCounterTextArea.text = "By dying only " + deathCounter + " times ...";
            }
            else if (deathCounter == 1)
            {
                deathCounterTextArea.text = "By dying only " + deathCounter + " time ...";
            }
            else
            {
                deathCounterTextArea.text = "Without any death ?!";
            }

            gameController.RestartGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isReady == true)
        {
            if(Input.GetButtonDown("Fire1") == true)
            {
                SceneManager.LoadScene(_nextSceneId);
            }
        } else
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft < 0)
            {
                IsReady();
            }
        }
    }

    private void IsReady()
    {
        _isReady = true;

        if(textToDisplayWithDelay != null)
        {
            textToDisplayWithDelay.enabled = true;
        }

    }
}
