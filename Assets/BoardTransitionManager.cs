using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditorInternal;
using UnityEngine;

public class BoardTransitionManager : MonoBehaviour
{
    [SerializeField]
    private Transform startingPosTransform;

    [SerializeField]
    private Transform finalPosTransform;

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

    public void TransitionIn() {

        sequence?.Complete();

        transform.position = startingPosTransform.position;
        
        sequence = DOTween.Sequence()
            .Append(transform.DOMove(finalPosTransform.position, transitionTime).SetEase(Ease.OutBack, overshoot: transitionElasticity))
            .AppendInterval(waitTime)
            .OnComplete(() => GameManager.Instance.StartGame())
            .Play();
    }
}
