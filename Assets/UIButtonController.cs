using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Image background;

    [SerializeField]
    private Material buttonHoveredMaterial;

    [SerializeField]
    private Material buttonUnhoveredMaterial;

    [SerializeField]
    private float punchScaleAmount = .1f;

    [SerializeField]
    private float punchRotateAmount = 1f;

    [SerializeField]
    private float punchDuration = .1f;

    [SerializeField]
    private float punchElasticity = .2f;

    [SerializeField]
    private TextFlutterController textFlutterController;

    private Tween punchTween;

    public void OnPointerClick(PointerEventData eventData) {

        punchTween?.Complete();
        transform.localScale = Vector3.one;

        punchTween = transform.DOPunchScale(punchScaleAmount * Vector3.one * 2f, punchDuration, elasticity: punchElasticity * 2);
        // transform.DOPunchRotation(punchRotateAmount * Vector3.one * 2f, punchDuration, elasticity: punchElasticity * 2);

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

    public void OnPointerExit(PointerEventData eventData)
    {
        background.material = buttonUnhoveredMaterial;
        textFlutterController.StopFlutter();
    }

    public abstract void OnClick();
}
