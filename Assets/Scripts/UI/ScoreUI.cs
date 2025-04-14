using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI _score;


    private void Start() {
        ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void ScoreManager_OnScoreChanged(object sender, EventArgs e) {
        _score.text = ScoreManager.Instance.GetScore().ToString();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e) {
        if (GameManager.Instance.IsCountdownActive()) {
            Show();
        } else if (GameManager.Instance.IsVictory()) {
            Hide();
        }
        
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
