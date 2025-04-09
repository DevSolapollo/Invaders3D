using System;
using UnityEngine;

public class Invaders : MonoBehaviour
{


    [SerializeField] private GameObject[] _aliens;
    [SerializeField] private int _columns = 10;
    [SerializeField] private float _spacingX = 10f;     
    [SerializeField] private float _spacingY = 4f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _moveDown = 8f;
    [SerializeField] private float _boundary = 53f;
    [SerializeField] private Vector3 _startPosition = new Vector3(-35f, 20f, 0f);


    private Vector3 _direction = Vector3.right;


    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e) {
        if (GameManager.Instance.IsGamePlaying()) {
            SpawnInvaders();
        }
    }

    private void Update() {
        MoveInvaders();
    }

    private void SpawnInvaders() {
        for (int row = 0; row < _aliens.Length; row++) {
            for (int col = 0; col < _columns; col++) {
                Vector3 spawnPos = _startPosition + new Vector3(col * _spacingX, -row * _spacingY, 0f);
                Instantiate(_aliens[row], spawnPos, Quaternion.identity, transform);
            }
        }
    }

    private void MoveInvaders() {
        transform.position += _direction * _speed * Time.deltaTime;

        foreach (Transform alien in transform) {
            if (!alien.gameObject.activeInHierarchy) continue;

            if (Mathf.Abs(alien.position.x) >= _boundary) {
                MoveDown();
                _direction *= -1f; // Reverse direction
                break;
            }
        }
    }

    private void MoveDown() {
        transform.position += Vector3.down * _moveDown;
    }

}
