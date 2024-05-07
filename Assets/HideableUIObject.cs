using System;
using DG.Tweening;
using UnityEngine;

public class HideableUIObject : MonoBehaviour
{
    [Header("Transition Properties")]
    [SerializeField]
    protected float transitionTime = .5f;

    [SerializeField]
    private float transitionElasticity = 2f;

    [SerializeField]
    private bool doScale = true;

    [SerializeField]
    private Vector3 finalScale = Vector3.zero;

    [SerializeField]
    private bool doMove = true;

    [SerializeField]
    private Vector3 finalPosOffset = Vector3.zero;

    [SerializeField]
    private bool doRotate = true;

    [SerializeField]
    private Vector3 finalRotation = Vector3.zero;

    [SerializeField]
    private bool startHidden = false;
    
    public Action OnShow { get; set; }

    public Action OnHide { get; set; }

    private Vector3 startingScale;
    private Vector3 startingPos;
    private Vector3 startingRot;

    private bool hidden;

    protected virtual void Start() {
        startingScale = transform.localScale;
        startingPos = transform.localPosition;
        startingRot = transform.localEulerAngles;

        if(startHidden) {

            if(doScale) {
                transform.localScale = finalScale;
            }
            
            if(doMove) {
                transform.localPosition = startingPos + finalPosOffset;
            }
            
            if(doRotate) {
                transform.localEulerAngles = finalRotation;
            }
        }

        hidden = startHidden;
    }

    public virtual void Show() {

        if(!hidden) {
            return;
        }

        if(doScale) {
            transform.DOScale(startingScale, transitionTime).SetEase(Ease.OutBack, overshoot: transitionElasticity).SetUpdate(true);
        }

        if(doMove) {
            transform.DOLocalMove(startingPos, transitionTime).SetEase(Ease.OutBack, overshoot: transitionElasticity).SetUpdate(true);
        }

        if(doRotate) {
            transform.DOLocalRotate(startingRot, transitionTime).SetEase(Ease.OutBack, overshoot: transitionElasticity).SetUpdate(true);
        }

        hidden = false;
        OnShow?.Invoke();
    }

    public virtual void Hide() {


        if(hidden) {
            return;
        }

        if(doScale) {
            transform.DOScale(finalScale, transitionTime).SetEase(Ease.InBack, overshoot: transitionElasticity).SetUpdate(true);
        }

        if(doMove) {
            transform.DOLocalMove(startingPos + finalPosOffset, transitionTime).SetEase(Ease.InBack, overshoot: transitionElasticity).SetUpdate(true);
        }

        if(doRotate) {
            transform.DOLocalRotate(finalRotation, transitionTime).SetEase(Ease.InBack, overshoot: transitionElasticity).SetUpdate(true);
        }

        hidden = true;
        OnHide?.Invoke();
    }

    public void Toggle() {
        if(hidden) {
            Show();
        }
        else {
            Hide();
        }
    }
}
