using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    protected Image background;

    [SerializeField]
    protected Material buttonHoveredMaterial;

    [SerializeField]
    protected Material buttonUnhoveredMaterial;

    [SerializeField]
    private float punchScaleAmount = .05f;

    [SerializeField]
    private float punchRotateAmount = 1f;

    [SerializeField]
    private float punchDuration = .3f;

    [SerializeField]
    private float punchElasticity = 1f;

    [SerializeField]
    protected TextFlutterController textFlutterController;

    [SerializeField]
    private AudioSource audioSource;

    private Tween punchTween;

    void Start() {
        background.material = buttonUnhoveredMaterial;
    }

    public void OnPointerClick(PointerEventData eventData) {

        punchTween?.Complete();
        transform.localScale = Vector3.one;

        punchTween = transform.DOPunchScale(punchScaleAmount * Vector3.one * 2f, punchDuration, elasticity: punchElasticity * 2);
        // transform.DOPunchRotation(punchRotateAmount * Vector3.one * 2f, punchDuration, elasticity: punchElasticity * 2);

        if(audioSource != null) {
            audioSource.PlayWithPitchVariance(new Vector2(.9f, 1.1f));
        }

        OnClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        punchTween?.Complete();
        transform.localScale = Vector3.one;

        punchTween = transform.DOPunchScale(punchScaleAmount * Vector3.one, punchDuration, elasticity: punchElasticity);
        // transform.DOBlendablePunchRotation(punchRotateAmount * Vector3.one, punchDuration, elasticity: punchElasticity);
        background.material = buttonHoveredMaterial;
        textFlutterController.StartFlutter();

    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        background.material = buttonUnhoveredMaterial;
        textFlutterController.StopFlutter();
    }

    public abstract void OnClick();
}
