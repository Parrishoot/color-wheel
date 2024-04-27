using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressToStartTextController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.GameStarted += Hide;
        GameManager.Instance.GameOver += ShowGameOverText;
    }

    private void Hide() {
        text.SetAlpha(0f);
    }

    private void Show() {
        text.SetAlpha(1f);
    }

    private void ShowGameOverText() {
        text.text = string.Format("Game Over! Your Score: {0}\nPress Space to Restart", ScoreController.Instance.CurrentScore);
        Show();
    }
}
