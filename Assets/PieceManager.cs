using System;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public Vector2Int Coords { get; private set;}
    
    public Action<Direction> PieceMoved { get; set; }

    private BoardManager boardManager;

    private void Start() {
        boardManager = BoardManager.Instance;
    }

    
    public void Init(Vector2Int coords) {
        Coords = coords;
    }

    public void Move(Direction direction) {

        Vector2Int movementVector = direction.GetMovementVector();
        Vector2Int newCoords = boardManager.GetWrappedVector(Coords + movementVector);


        if(boardManager.Occupied(newCoords)) {
            return;
        }

        Coords = newCoords;
        PieceMoved(direction);
    }
}
