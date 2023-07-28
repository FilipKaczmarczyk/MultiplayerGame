using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        Gameplay,
        GameOver
    }

    private State _state;
    private float _waitingToStartTimer = 1f;
    private float _countdownToStartTimer = 3f;
    private float _gameplayTimer = 10f;

    private void Awake()
    {
        Instance = this;
        
        _state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                
                _waitingToStartTimer -= Time.deltaTime;
                if (_waitingToStartTimer < 0f)
                {
                    _state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.CountdownToStart:
                
                _countdownToStartTimer -= Time.deltaTime;
                if (_countdownToStartTimer < 0f)
                {
                    _state = State.Gameplay;
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

    public float GetCountdownToStartTime()
    {
        return _countdownToStartTimer;
    }
}
