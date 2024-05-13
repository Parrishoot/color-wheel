using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CurrentScoreController : Singleton<CurrentScoreController>
{
    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private float elasticity = 1f;

    [SerializeField]
    private float accentuateScale = 1.5f;

    private bool showing = false;

    private Vector3 startingPos;

    void Start() {
        startingPos = transform.position;
    }

    public void SetScoreText(ScoreTracker scoreTracker) {
       text.text =$"{scoreTracker.CurrentScore:n0} X {scoreTracker.CurrentMultipler:n0}";
    }

    public void Show() {

        if(showing) {
            Accentuate();
            return;
        }

        showing = true;
        transform.position = startingPos;
        transform.DOScale(Vector3.one, TickManager.Instance.TickTime / 2).SetEase(Ease.OutBack, overshoot: elasticity);
    }

    public void Hide(ScoreTracker scoreTracker) {
        
        showing = false;
        int scoreToAdd = scoreTracker.CalcScore();

        if(scoreToAdd == 0) {
            return;
        }

        Sequence s = DOTween.Sequence()
                            .Append(transform.DOScale(Vector3.zero, TickManager.Instance.TickTime / 2).SetEase(Ease.InBack, overshoot: elasticity))
                            .Join(transform.DOMove(ScoreController.Instance.transform.position, TickManager.Instance.TickTime / 2f).SetEase(Ease.InBack, overshoot: elasticity))
                            .OnComplete(() => {
                                ScoreController.Instance.AddScore(scoreToAdd);
                                SoundManager.Instance.PlayCashIn();
                            })
                            .Play();
    }

    private void Accentuate() {
        transform.DOPunchScale(Vector3.one * accentuateScale, TickManager.Instance.TickTime / 4f, elasticity: elasticity);
    }
}
