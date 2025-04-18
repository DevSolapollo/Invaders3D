using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveOverUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _totalScoreText;
    [SerializeField] private TextMeshProUGUI _waveOverText;
    [SerializeField] private TextMeshProUGUI _continueText;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private Color _gameOverColor;


    private void Awake() {
        _continueButton.onClick.AddListener(() => {
            if (GameManager.Instance.IsWaveComplete()) {
                GameManager.Instance.ContinueWave();
                Hide();
            } else {
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
            }
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
        if (GameManager.Instance.IsVictory() || GameManager.Instance.IsGameOver() || GameManager.Instance.IsWaveComplete()) {
            _scoreText.text = ScoreManager.Instance.GetScore().ToString();
            _totalScoreText.text = ScoreManager.Instance.GetTotalScore().ToString();
            AudioManager.Instance.StopMusic();

            if (GameManager.Instance.IsGameOver()) {
                _waveOverText.text = "GAME OVER";
                _waveOverText.color = _gameOverColor;
                _continueText.text = "RETRY";
            } else if (GameManager.Instance.IsWaveComplete()) {
                _waveOverText.text = "WAVE COMPLETE";
                _waveOverText.color = Color.yellow;
                _continueText.text = "NEXT WAVE";
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
