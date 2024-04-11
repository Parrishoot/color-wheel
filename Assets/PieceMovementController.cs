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

    // Start is called before the first frame update
    void Start()
    {
        pieceManager.PieceMoved += ProcessMovement;
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
        transform.DOScale(BoardManager.Instance.GetLocalScaleForYCoord(newYCoord), tweenTime).SetEase(movementCurve);
    }

    private void HandleHorizontalMovement() {
        int newXCoord = pieceManager.Coords.x;
        transform.DOLocalRotate(BoardManager.Instance.GetLocalRotationForXCoord(newXCoord), tweenTime).SetEase(movementCurve);
    }

}
