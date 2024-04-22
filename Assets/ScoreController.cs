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

    private int currentScore = 0;

    public void AddScore(int score) {
        currentScore += score;

        transform.DOPunchScale(Vector3.one * punchScale, TickManager.Instance.TickTime / 4f, elasticity: elasticity);

        text.text = $"{currentScore:n0}";
    }
}
