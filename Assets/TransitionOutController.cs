using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenTransitionController : Singleton<TitleScreenTransitionController> 
{
    [SerializeField]
    private RectTransform titleGameObject;

    [SerializeField]
    private RectTransform buttonsContainerGameObject;

    [SerializeField]
    private float translationAmount = 1000f;

    [SerializeField]
    private float transitionDuration = 1f;

    [SerializeField]
    private float transitionElasicity = 1f;

    public Action TransitionComplete { get; set;}

    protected Vector3 titleOriginalPos;

    protected Vector3 buttonContainerOriginalPos;

    private bool canTransition = true;

    void Start() {
        buttonContainerOriginalPos = buttonsContainerGameObject.position;
        titleOriginalPos = titleGameObject.position;

        TransitionComplete += () => {
            canTransition = true;
        };
    }

    public void Hide()
    {

        if(!canTransition) {
            return;
        }

        canTransition = false;

         DOTween.Sequence()
            .Append(titleGameObject.DOMoveY(titleGameObject.position.y + translationAmount, transitionDuration).SetEase(Ease.InOutBack, overshoot: transitionElasicity))
            .Join(buttonsContainerGameObject.DOMoveY(buttonsContainerGameObject.position.y - translationAmount, transitionDuration).SetEase(Ease.InOutBack, overshoot: transitionElasicity))
            .Play()
            .OnComplete(() => TransitionComplete?.Invoke());
    }

    public void Show()
    {
        if(!canTransition) {
            return;
        }

        canTransition = false;


         DOTween.Sequence()
            .Append(titleGameObject.DOMoveY(titleGameObject.position.y - translationAmount, transitionDuration).SetEase(Ease.InOutBack, overshoot: transitionElasicity))
            .Join(buttonsContainerGameObject.DOMoveY(buttonsContainerGameObject.position.y + translationAmount, transitionDuration).SetEase(Ease.InOutBack, overshoot: transitionElasicity))
            .Play()
            .OnComplete(() => TransitionComplete?.Invoke());
    }
}
