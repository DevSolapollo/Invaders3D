using System;
using UnityEngine;

public class StartUI : MonoBehaviour
{


    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Show();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e) {
        if (GameManager.Instance.IsCountdownActive()) {
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
