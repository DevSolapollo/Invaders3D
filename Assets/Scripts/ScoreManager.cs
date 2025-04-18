using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{


    public static ScoreManager Instance { get; private set; }


    private int _score;
    private int _totalScore;


    public event EventHandler OnScoreChanged;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start() {
        Invader.OnInvaderKilled += Invader_OnInvaderKilled;
    }

    private void Invader_OnInvaderKilled(object sender, Invader.OnInvaderKilledEventArgs e) {
        AddScore(e.points);
    }


    private void AddScore(int points) {
        _score += points;
        _totalScore += points;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }


    public int GetScore() {
        return _score;
    }

    public int GetTotalScore() {
        return _totalScore;
    }

    public void ResetScore() {
        _score = 0;
    }

}
