
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreScreenController : HideableUIObject
{
    [Header("Other Propertes")]
    [SerializeField]
    private UIDelegateButtonController playAgainButton;

    [SerializeField]
    private TMP_Text scoreText;

    protected override void Start() {
        base.Start();

        GameManager.Instance.GameOver += () => {
            scoreText.text = $"{ScoreController.Instance.CurrentScore:n0}";
            Show();
        };

        playAgainButton.OnButtonClick += () => {
            Hide();
            DOTween.Sequence()
                   .AppendInterval(transitionTime)
                   .OnComplete(() => GameManager.Instance.Reset())
                   .Play();
        };
    }
}
