using System;
using UnityEngine;

public class Turret : MonoBehaviour
{


    [SerializeField] private Transform _rocketPosition;
    [SerializeField] private GameObject _rocketPrefab;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private float _rocketSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _boundary;
    [SerializeField] private float shootCooldown;


    private float lastShootTime = -Mathf.Infinity;
    private float _moveInput;


    private void Start() {
        GameInput.Instance.OnShoot += GameInput_OnShoot;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void OnDestroy() {
        GameInput.Instance.OnShoot -= GameInput_OnShoot;
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            Destroy(gameObject);
        }
    }

    private void GameInput_OnShoot(object sender, EventArgs e) {
        if (!GameManager.Instance.IsGamePlaying())
            return;

        if (Time.time < lastShootTime + shootCooldown)
            return;

        GameObject rocket = Instantiate(_rocketPrefab, _rocketPosition.position, Quaternion.identity);
        AudioManager.Instance.PlaySFX(_shootSound);

        Rigidbody rb = rocket.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = Vector3.up * _rocketSpeed;

        lastShootTime = Time.time;
    }

    private void Update() {
        if (GameManager.Instance.IsGamePlaying() || GameManager.Instance.IsCountdownActive()) {
            _moveInput = GameInput.Instance.GetMoveInput();

            Vector3 move = new Vector3(_moveInput * _moveSpeed * Time.deltaTime, 0f, 0f);

            transform.Translate(move);

            float clampedX = Mathf.Clamp(transform.position.x, -_boundary, _boundary);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }

}
