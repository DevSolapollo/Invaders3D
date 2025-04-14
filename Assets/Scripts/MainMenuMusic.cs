using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{


    [SerializeField] private AudioClip _menuMusic;


    private void Start() {
        AudioManager.Instance.PlayMusic(_menuMusic);
    }

}
