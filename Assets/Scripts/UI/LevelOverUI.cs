using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelOverUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _levelOverText;
    [SerializeField] private TextMeshProUGUI _continueText;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private Color _gameOverColor;


    private void Awake() {
        _continueButton.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        });
        _mainMenuButton.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
        });
        _exitGameButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }


    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsVictory() || GameManager.Instance.IsGameOver()) {
            _scoreText.text = ScoreManager.Instance.GetScore().ToString();
            AudioManager.Instance.StopMusic();

            if (GameManager.Instance.IsGameOver()) {
                _levelOverText.text = "GAME OVER";
                _levelOverText.color = _gameOverColor;

                _continueText.text = "RETRY";
            }

            Show();
        } 
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
