using System;
using UnityEngine;

public class Invaders : MonoBehaviour
{


    [SerializeField] private GameObject[] _invaders;
    [SerializeField] private int _columns = 10;
    [SerializeField] private float _spacingX = 10f;     
    [SerializeField] private float _spacingY = 4f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _moveDown = 8f;
    [SerializeField] private float _boundary = 53f;
    [SerializeField] private Vector3 _startPosition = new Vector3(-35f, 20f, 0f);


    private Vector3 _direction = Vector3.right;
    private bool _anyAlive;
    private bool _invadersSpawned = false;


    public static Invaders Instance { get; private set; }


    public event EventHandler OnInvaderWipe; 


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e) {
        if (GameManager.Instance.IsGamePlaying()) {
            SpawnInvaders();
        }
    }

    private void Update() {
        if (GameManager.Instance.IsGamePlaying()) {
            MoveInvaders();
        }
    }

    private void SpawnInvaders() {
        for (int row = 0; row < _invaders.Length; row++) {
            for (int col = 0; col < _columns; col++) {
                Vector3 spawnPos = _startPosition + new Vector3(col * _spacingX, -row * _spacingY, 0f);
                Instantiate(_invaders[row], spawnPos, Quaternion.identity, transform);
            }
        }

        _invadersSpawned = true;
    }

    private void MoveInvaders() {
        if (!_invadersSpawned || transform.childCount == 0) return;

        _anyAlive = false;
        
        // Check if any invaders are still alive
        foreach (Transform invader in transform) {
            if (invader.gameObject.activeInHierarchy) {
                _anyAlive = true;
                break;
            }
        }

        if (!_anyAlive) {
            OnInvaderWipe?.Invoke(this, EventArgs.Empty);
            return;
        }

        bool hitBoundary = false;

        foreach (Transform invader in transform) {
            if (!invader.gameObject.activeInHierarchy) continue;

            float nextX = invader.position.x + (_direction.x * _speed * Time.deltaTime);
            if (Mathf.Abs(nextX) >= _boundary) {
                hitBoundary = true;
                break;
            }
        }

        if (hitBoundary) {
            MoveDown();
            _direction = -_direction;
        } else {
            transform.position += _direction * _speed * Time.deltaTime;
        }
    }

    private void MoveDown() {
        transform.position += Vector3.down * _moveDown;
    }

}
