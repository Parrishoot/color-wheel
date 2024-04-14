using System;
using UnityEngine;

public class PieceManager : StateMachine
{
    [field:SerializeReference]
    public Vector2Int Coords { get; private set;}
    
    public Action<Direction> PieceMoved { get; set; }

    private BoardManager boardManager;

    // States
    public PieceFallingState PieceFallingState { get; protected set; }
    public PieceSpawnedFallingState PieceSpawnedFallingState { get; protected set; }
    public PieceIdleState PieceIdleState { get; protected set; }

    public void Init(Vector2Int coords) {

        PieceFallingState = new PieceFallingState(this);
        PieceSpawnedFallingState = new PieceSpawnedFallingState(this);
        PieceIdleState = new PieceIdleState(this);

        ChangeState(PieceSpawnedFallingState);

        boardManager = BoardManager.Instance;
        UpdateCoords(coords, resetExisting: false);
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

    public bool OnGround() {
        for(int i = 0; i < Coords.y; i++) {
            if(!boardManager.Occupied(new Vector2Int(Coords.x, i))) {
                return false;
            }
        }
        return true;
    }

    private void UpdateCoords(Vector2Int newCoords, bool resetExisting = true) {

        if(resetExisting) {
            boardManager.Grid[Coords.x, Coords.y] = null;
        }
        
        Coords = newCoords;
        boardManager.Grid[Coords.x, Coords.y] = this;
    }
}
