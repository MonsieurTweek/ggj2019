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
    public bool needFadeOut = false;
    [Header("----- End Screen -----")]
    public GameController gameController;
    public Text deathCounterTextArea;

    private bool _isReady = false;
    private int _nextSceneId;

    private float _timeLeft;

    private AudioSource _audioSource;

    public static float currentClipTime;

    private const int TIME_TO_FADE_OUT = 2;
    private float _elapsedTime = 0f;
    private bool _isFadingOut = false;
    private int _timerCountDown = TIME_TO_FADE_OUT;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (currentClipTime != 0f)
        {
            _audioSource.time = currentClipTime;
        }

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
                deathCounterTextArea.text = "Without dying ! Who are you ?!";
            }

            gameController.RestartGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ready to go to next screen
        if(_isReady == true)
        {
            // Need to press A
            if(Input.GetButtonDown("Fire1") == true && _isFadingOut == false)
            {
                if(textToDisplayWithDelay != null)
                {
                    Animator textBlinkAnimator = textToDisplayWithDelay.GetComponent<Animator>();
                    if (textBlinkAnimator != null)
                    {
                        textBlinkAnimator.SetBool("isActive", false);
                        textToDisplayWithDelay.enabled = false;
                    }
                }
                // If we need a fadeout for the music, we start it
                if (needFadeOut == true)
                {
                    _isFadingOut = true;
                } else
                {
                    if(_nextSceneId == LOADING_SCENE_ID)
                    {
                        currentClipTime = _audioSource.time;
                    }
                    SceneManager.LoadScene(_nextSceneId);
                }
            }

            if(_isFadingOut == true)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= 1f)
                {
                    _elapsedTime = _elapsedTime % 1f;
                    _timerCountDown--;
                    if(_timerCountDown < 0)
                    {
                        SceneManager.LoadScene(_nextSceneId);
                    }
                }
                FadeOutMusic();
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
            Animator textBlinkAnimator = textToDisplayWithDelay.GetComponent<Animator>();
            if(textBlinkAnimator != null)
            {
                textBlinkAnimator.SetBool("isActive", true);
            }
        }

    }

    public void FadeOutMusic()
    {
        if (_audioSource.volume > 0)
        {
            _audioSource.volume = _audioSource.volume - (Time.deltaTime / TIME_TO_FADE_OUT);
        }
    }
}
