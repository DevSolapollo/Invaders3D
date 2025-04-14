using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{


    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;


    private void Awake() {
        _startButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
        _exitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }

}
