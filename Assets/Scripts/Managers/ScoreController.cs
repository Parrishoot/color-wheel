using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreController : Singleton<ScoreController>
{
    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private float punchScale = 1.1f;

    [SerializeField]
    private float elasticity =  1.2f;

    [SerializeField]
    private TextFlutterController textFlutterController;

    [SerializeField]
    public int CurrentScore { get; protected set; } = 0;

    void Start() {

        GameManager.Instance.GameOver += () => { 
            text.SetAlpha(0f);
        };

        GameManager.Instance.GameReset += () => CurrentScore = 0;
    }

    public void AddScore(int score) {

        if(text.alpha == 0) {
            text.SetAlpha(1f);
        }

        CurrentScore += score;

        transform.DOPunchScale(Vector3.one * punchScale, TickManager.Instance.TickTime / 4f, elasticity: elasticity);

        float regularMagnitude = textFlutterController.Magnitude;
        textFlutterController.Magnitude = 3 * regularMagnitude;
        DOTween.To(() => textFlutterController.Magnitude, x => textFlutterController.Magnitude = x, regularMagnitude, TickManager.Instance.TickTime).SetEase(Ease.InOutCubic);

        text.text = $"{CurrentScore:n0}";
    }
}
