using System;
using Input;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        Gameplay,
        GameOver
    }
    
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnPauseEnabled;
    public event EventHandler OnPauseDisabled;

    [SerializeField] private float totalGameplayTime = 10f;
    
    private State _state;
    private float _countdownToStartTimer = 3f;
    private float _gameplayTimer = 10f;
    private bool _gamePaused;

    private void Awake()
    {
        Instance = this;
        
        _state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        
        GameInput.Instance.OnInteractAction += GameInputOnInteractAction;
    }

    private void GameInputOnInteractAction(object sender, EventArgs e)
    {
        if (_state == State.WaitingToStart)
        {
            _state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePause();
    }
    
    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                break;
            
            case State.CountdownToStart:
                
                _countdownToStartTimer -= Time.deltaTime;
                if (_countdownToStartTimer < 0f)
                {
                    _state = State.Gameplay;
                    _gameplayTimer = totalGameplayTime;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.Gameplay:
                
                _gameplayTimer -= Time.deltaTime;
                if (_gameplayTimer < 0f)
                {
                    _state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.GameOver:
                
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public bool IsGamePlaying()
    {
        return _state == State.Gameplay;
    }

    public bool IsCountdownToStartActive()
    {
        return _state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return _state == State.GameOver;
    }

    public float GetCountdownToStartTime()
    {
        return _countdownToStartTimer;
    }

    public float GetPlayingTimeNormalized()
    {
        return _gameplayTimer / totalGameplayTime;
    }
    
    public void TogglePause()
    {
        _gamePaused = !_gamePaused;

        Time.timeScale = _gamePaused ? 0f : 1f;

        if (_gamePaused)
        {
            OnPauseEnabled?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnPauseDisabled?.Invoke(this, EventArgs.Empty);
        }
    }
}
