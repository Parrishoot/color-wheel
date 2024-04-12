using System;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public Vector2Int Coords { get; private set;}
    
    public Action<Direction> PieceMoved { get; set; }

    private BoardManager boardManager;
    
    public void Init(Vector2Int coords) {
        boardManager = BoardManager.Instance;
        UpdateCoords(coords);
    }

    public void Move(Direction direction) {

        Vector2Int movementVector = direction.GetMovementVector();
        Vector2Int newCoords = boardManager.GetWrappedVector(Coords + movementVector);


        if(boardManager.Occupied(newCoords)) {
            return;
        }

        UpdateCoords(newCoords);
        PieceMoved(direction);
    }

    private void UpdateCoords(Vector2Int newCoords) {
        Coords = newCoords;
        boardManager.Grid[Coords.x, Coords.y] = this;
    }
}
