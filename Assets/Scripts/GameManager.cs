using System;
using System.Globalization;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class GameManager : MonoBehaviour
{


    [SerializeField] private AudioClip _gameMusic;


    public static GameManager Instance { get; private set; }


    public event EventHandler OnStateChanged;


    private enum State {
        Waiting,
        Countdown,
        Playing,
        GameOver,
        Victory
    }

    private State _state;
    private float _countdownToStartTimer = 3f;
    private float _gamePlayingTimer;
    private float _gamePlayingTimerMax = 60f;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Debug.LogError("Multiple GameManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _state = State.Waiting;
    }

    private void Start() {
        GameEvents.OnGameOver += GameEvents_GameOver;
        GameInput.Instance.OnShoot += GameInput_OnShoot;
        Invaders.Instance.OnInvaderWipe += Invaders_OnInvaderWipe;

        AudioManager.Instance.StopMusic();
    }

    private void OnDestroy() {
        GameEvents.OnGameOver -= GameEvents_GameOver;
    }

    private void Update() {
        switch (_state) {
            case State.Waiting:
                break;
            case State.Countdown:
                Countdown();
                break;
            case State.Playing:
                Gameplay();
                break;
            case State.GameOver:
                break;
            case State.Victory:
                break;
        }
    }

    private void GameInput_OnShoot(object sender, EventArgs e) {
        if (_state == State.Waiting) {
            _state = State.Countdown;
            AudioManager.Instance.PlayMusic(_gameMusic);
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameEvents_GameOver() {
        _state = State.GameOver;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Invaders_OnInvaderWipe(object sender, EventArgs e) {
        _state = State.Victory;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Countdown() {
        _countdownToStartTimer -= Time.deltaTime;
        if (_countdownToStartTimer < 0f) {
            _state = State.Playing;
            _gamePlayingTimer = _gamePlayingTimerMax;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Gameplay() {
        _gamePlayingTimer -= Time.deltaTime;
        if (_gamePlayingTimer < 0f) {
            _state = State.GameOver;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }


    public float GetCountdownToStartTimer() {
        return _countdownToStartTimer;
    }


    public bool IsGamePlaying() {
        return _state == State.Playing;
    }

    public bool IsCountdownActive() {
        return _state == State.Countdown;
    }

    public bool IsGameOver() {
        return _state == State.GameOver;
    }
    public bool IsVictory() {
        return _state == State.Victory;
    }

}
