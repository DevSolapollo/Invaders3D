using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    

    public static LevelManager Instance { get; private set; }


    [SerializeField] private LevelSO[] _levels;


    private int _currentLevelIndex = 0;


    public LevelSO CurrentLevel => _levels[_currentLevelIndex];


    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public void LoadNextLevel() {
        _currentLevelIndex++;
        if (_currentLevelIndex < _levels.Length) {
            Invaders.Instance.SpawnInvaders(CurrentLevel.InvaderRows);
        } else {
            GameManager.Instance.TriggerVictory();
        }
    }

    public void LoadCurrentLevel() {
        Invaders.Instance.SpawnInvaders(CurrentLevel.InvaderRows);
    }

    public bool HasNextLevel() {
        return _currentLevelIndex + 1 < _levels.Length;
    }

}
