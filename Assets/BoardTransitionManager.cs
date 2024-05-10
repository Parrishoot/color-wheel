using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoardTransitionManager : MonoBehaviour
{
    [SerializeField]
    private Transform startingPosTransform;

    [SerializeField]
    private Transform gamePosTransform;

    [SerializeField]
    private Transform resettingPosTransform;

    [SerializeField]
    private float transitionTime = 1f;

    [SerializeField]
    private float transitionElasticity;

    [SerializeField]
    private float waitTime = 2f;

    [SerializeField]
    private BoardManager boardManager;

    private Sequence sequence;

    void Awake() {
        boardManager.OnReset += TransitionIn;
    }

    void Start() {
        GameManager.Instance.GameOver += TransitionOut;
    }

    public void TransitionIn() {

        sequence?.Complete();

        transform.position = startingPosTransform.position;
        
        sequence = DOTween.Sequence()
            .Append(transform.DOMove(gamePosTransform.position, transitionTime).SetEase(Ease.OutBack, overshoot: transitionElasticity))
            .AppendInterval(waitTime)
            .OnComplete(() => GameManager.Instance.StartGame())
            .Play();
    }

    public void TransitionOut() {

        sequence?.Complete();

        transform.position = gamePosTransform.position;
        
        sequence = DOTween.Sequence()
            .Append(transform.DOMove(resettingPosTransform.position, transitionTime * 1.5f).SetEase(Ease.InBack, overshoot: transitionElasticity * 2f))
            .AppendInterval(waitTime)
            .Play();
    }
}
