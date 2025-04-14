using System;
using System.Collections;
using UnityEngine;

public class Invader : MonoBehaviour
{


    public static event EventHandler<OnInvaderKilledEventArgs> OnInvaderKilled;


    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private float _flashDuration = 0.1f;


    public virtual int points => 25;
    public virtual int maxHealth => 1;

    private int _currentHealth;
    private Material _material;
    private Color _originalColor;


    private void Awake() {
        _material = _renderer.material;
        _originalColor = _material.color;
    }

    private void OnEnable() {
        _currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Turret>() != null) {
            GameEvents.TriggerGameOver();
        }
    }

    private void PlayHitSound() {
        AudioManager.Instance.PlaySFX(_hitSound);
    }

    private void Die() {
        OnInvaderKilled?.Invoke(this, new OnInvaderKilledEventArgs {
            points = points
        });

        gameObject.SetActive(false);
    }

    private IEnumerator FlashDamage() {
        _material.color = _flashColor;
        yield return new WaitForSeconds(_flashDuration);
        _material.color = _originalColor;
    }


    public void TakeDamage() {
        _currentHealth--;

        PlayHitSound();
        StartCoroutine(FlashDamage());
        

        if (_currentHealth <= 0) {
            Die();
        }
    }


    public class OnInvaderKilledEventArgs : EventArgs {
        public int points;
    }


}
