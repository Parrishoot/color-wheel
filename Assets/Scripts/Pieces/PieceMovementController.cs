using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PieceMovementController : MonoBehaviour
{
    [SerializeField]
    private PieceManager pieceManager;

    [SerializeField]
    private float tweenTime = .1f;

    [SerializeField]
    private AnimationCurve movementCurve;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = spriteRenderer.material;
        pieceManager.PieceMoved += ProcessMovement;
        pieceManager.OnDeath += () => enabled = false;
    }

    private void ProcessMovement(Direction direction) {

        switch(direction) {

            case Direction.UP:
            case Direction.DOWN:
                HandleVerticalMovement();
                break;

            case Direction.LEFT:
            case Direction.RIGHT:
                HandleHorizontalMovement();
                break;
        }
    }

    private void HandleVerticalMovement() {
        int newYCoord = pieceManager.Coords.y;
        DOTween.To(() => material.GetFloat("_InnerRadius"), x => material.SetFloat("_InnerRadius", x), BoardManager.Instance.GetInnerRadiusForYCoord(newYCoord), tweenTime).SetEase(Ease.InOutCubic);
        DOTween.To(() => material.GetFloat("_OuterRadius"), x => material.SetFloat("_OuterRadius", x), BoardManager.Instance.GetOuterRadiusForYCoord(newYCoord), tweenTime).SetEase(Ease.InOutCubic);
    }

    private void HandleHorizontalMovement() {
        int newXCoord = pieceManager.Coords.x;
        transform.DOLocalRotate(BoardManager.Instance.GetLocalRotationForXCoord(newXCoord), tweenTime).SetEase(movementCurve);
    }
}
