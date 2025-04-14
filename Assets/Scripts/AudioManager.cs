using UnityEngine;

public class AudioManager : MonoBehaviour
{
    

    public static AudioManager Instance { get; private set; }


    [SerializeField] private AudioSource _sfxSource;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void PlaySFX(AudioClip clip, float volume = 1f, float pitchVariance = 0.2f) {
        if (clip != null) {
            _sfxSource.pitch = 1f + Random.Range(-pitchVariance, pitchVariance);
            _sfxSource.PlayOneShot(clip, volume);
        }
    }

}
