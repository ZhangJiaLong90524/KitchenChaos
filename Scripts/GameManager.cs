using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    [SerializeField] private float countdownTimeMax = 3f;
    [SerializeField] private float gamePlayingTimeMax = 10f;
    private float _gamePlayingTimer;
    private State _state;

    public float CountdownTimer { get; private set; }
    public static GameManager Instance { get; private set; }

    public bool IsWaitingToStart=>_state == State.WaitingToStart;

    public bool IsGamePlaying=>_state == State.GamePlaying;
    public bool IsCountdownToStart=>_state == State.CountdownToStart;
    public bool IsGameOver=>_state == State.GameOver;

    public float GamePlayingTimerNormalized=>_gamePlayingTimer / gamePlayingTimeMax;

    private void Awake()
    {
        Instance = this;

        _state = State.WaitingToStart;
    }

    private void Start()
    {
        CountdownTimer = countdownTimeMax;
        _gamePlayingTimer = gamePlayingTimeMax;


        GameInput.Instance.OnPauseAction += (_, _)=>ToggleGamePause();


        GameInput.Instance.OnInteractAction += (_, _)=>
        {
            if (_state == State.WaitingToStart)
            {
                _state = State.CountdownToStart;

                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
        };
    }

    private void Update()
    {
        switch (_state)
        {
            case State.CountdownToStart:
                CountdownTimer -= Time.deltaTime;
                if (CountdownTimer <= 0)
                {
                    _state = State.GamePlaying;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;

            case State.GamePlaying:
                _gamePlayingTimer -= Time.deltaTime;
                if (_gamePlayingTimer <= 0)
                {
                    _state = State.GameOver;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;

            case State.WaitingToStart:
            case State.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ToggleGamePause()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;


            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;


            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }


    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
}