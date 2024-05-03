using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HideableUIObject : MonoBehaviour
{
    [SerializeField]
    private float transitionTime = .5f;

    [SerializeField]
    private float transitionElasticity = 2f;

    public void Show() {
        transform.DOScale(Vector3.one, transitionTime).SetEase(Ease.OutBack, overshoot: transitionElasticity);
    }

    public void Hide() {
        transform.DOScale(Vector3.zero, transitionTime).SetEase(Ease.InBack, overshoot: transitionElasticity);
    }
}
