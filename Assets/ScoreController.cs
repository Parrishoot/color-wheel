using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : Singleton<ScoreController>
{
    [SerializeField]
    private TMP_Text text;

    private int currentScore = 0;

    public void AddScore(int score) {
        currentScore += score;
        text.text = $"{currentScore:n0}";
    }
}
