using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitChenGameManager : MonoBehaviour
{
    public static KitChenGameManager instance;
    public event EventHandler OnSTateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    private enum State
    {
        WaitingStart, 
        CountDownStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float countDownTimer = 3f;
    private float gamePlayingTimer;
    [SerializeField]private float gamePlayingTimerMax = 10f;
    private bool isPause = false;

    private void Awake()
    {
        state = State.WaitingStart;
        instance = this;
    }

    private void Start()
    {
        GameInput.instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.instance.OnInteractActions += GameInput_OnInteractActions;
    }

    private void GameInput_OnInteractActions(object sender, EventArgs e)
    {
        if(state == State.WaitingStart)
        {
            state = State.CountDownStart;
            OnSTateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        PauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingStart:            
                break;
            case State.CountDownStart:
                {
                    countDownTimer -= Time.deltaTime;
                    if (countDownTimer < 0)
                    {
                        gamePlayingTimer = gamePlayingTimerMax;
                        state = State.GamePlaying;
                        OnSTateChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
                break ;
            case State.GamePlaying:
                {
                    gamePlayingTimer -= Time.deltaTime;
                    if (gamePlayingTimer < 0)
                    {
                        state = State.GameOver;

                        OnSTateChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
                break;
            case State.GameOver:
                break;
        }

            
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountDownToStartActive()
    {
        return state == State.CountDownStart;
    }
    public bool IsCountDownToGameOver()
    {
        return state == State.GameOver;
    }

    public float GetCountDownToStart()
    {
        return countDownTimer;
    }

    public float GetPlayingTimerNomalized()
    {
        return 1-( gamePlayingTimer/gamePlayingTimerMax);
    }

    public void PauseGame()
    {
        isPause = !isPause;
        if(isPause)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
