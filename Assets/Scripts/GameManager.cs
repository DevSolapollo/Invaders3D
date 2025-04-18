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
        WaveOver,
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
            case State.WaveOver:
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
        if (LevelManager.Instance.HasNextLevel()) {
            _state = State.WaveOver;
        } else {
            _state = State.Victory;
        }
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Countdown() {
        _countdownToStartTimer -= Time.deltaTime;
        if (_countdownToStartTimer < 0f) {
            _state = State.Playing;
            _gamePlayingTimer = _gamePlayingTimerMax;
            LevelManager.Instance.LoadCurrentLevel();
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


    public void ContinueWave() {
        _countdownToStartTimer = 3f;
        _state = State.Countdown;

        if (!AudioManager.Instance.IsMusicPlaying()) {
            AudioManager.Instance.PlayMusic(_gameMusic);
        }

        ScoreManager.Instance.ResetScore();
        LevelManager.Instance.LoadNextLevel();

        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TriggerVictory() {
        _state = State.Victory;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetCountdownToStartTimer() => _countdownToStartTimer;

    public bool IsGamePlaying() => _state == State.Playing;

    public bool IsCountdownActive() => _state == State.Countdown;

    public bool IsWaveComplete() => _state == State.WaveOver;

    public bool IsGameOver() => _state == State.GameOver;

    public bool IsVictory() => _state == State.Victory;
    
}
