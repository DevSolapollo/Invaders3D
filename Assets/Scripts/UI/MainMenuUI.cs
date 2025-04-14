using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{


    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private AudioClip _buttonSound;


    private void Awake() {
        _startButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(_buttonSound);
            Loader.Load(Loader.Scene.GameScene);
        });
        _exitButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(_buttonSound);
            Application.Quit();
        });

        Time.timeScale = 1f;
    }

}
