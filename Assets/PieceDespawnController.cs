using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PieceDespawnController : MonoBehaviour
{
    [SerializeField]
    private float rotateAmount = 720f;

    [SerializeField]
    private AnimationCurve animationCurve;

    [SerializeField]
    private PieceManager pieceManager;

    [SerializeField]
    [Range(0f, 3f)]
    private float elasticity = 1f;

    [SerializeField]
    [Range(1, 10)]
    private int tickScale = 4;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Material material;

    void Start() {
        pieceManager.OnDeath += PlayAnimation;
        material = spriteRenderer.material;
    }

    // Update is called once per frame
    void PlayAnimation()
    {
        Color startColor = material.GetColor("_Color");

        Sequence s = DOTween.Sequence();

        s.easeOvershootOrAmplitude = elasticity;

        s.Join(transform.DOLocalRotate(transform.localEulerAngles + Vector3.forward * rotateAmount, 
                                      TickManager.Instance.TickTime * tickScale, RotateMode.FastBeyond360)
                        .SetEase(Ease.InBack, overshoot: elasticity))
         .Join(transform.DOScale(Vector3.zero, 
                                TickManager.Instance.TickTime * tickScale)
                        .SetEase(Ease.InBack, overshoot: elasticity))
         .Join(spriteRenderer.DOColor(Color.white, TickManager.Instance.TickTime * tickScale / 2f)
                             .SetEase(Ease.InOutCubic)
                             .OnComplete(() => spriteRenderer.DOFade(0f, TickManager.Instance.TickTime * tickScale / 2f)
                                                             .SetEase(Ease.InOutCubic)))
         .OnComplete(() => Destroy(gameObject))
         .Play();
    }
}
