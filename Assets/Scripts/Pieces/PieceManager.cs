using System;
using System.ComponentModel;
using UnityEngine;

public class PieceManager : StateMachine
{
    [field:SerializeReference]
    [ReadOnly(true)]
    public Vector2Int Coords { get; private set;}
    
    public Action<Direction> PieceMoved { get; set; }

    public Action OnDeath { get; set; }

    // States
    public PieceFallingState PieceFallingState { get; protected set; }
    public PieceSpawnedFallingState PieceSpawnedFallingState { get; protected set; }
    public PieceIdleState PieceIdleState { get; protected set; }

    [field:SerializeField]
    public PieceClusterManager PieceClusterManager { get; protected set; }

    public void Init(Vector2Int coords) {

        PieceFallingState = new PieceFallingState(this);
        PieceSpawnedFallingState = new PieceSpawnedFallingState(this);
        PieceIdleState = new PieceIdleState(this);

        ChangeState(PieceSpawnedFallingState);

        UpdateCoords(coords, resetExisting: false);

        PiecesManager.Instance.PieceSpawned?.Invoke(this);
    }

    public void Move(Direction direction) {

        if(!CanMove(direction)) {
            return;
        }

        UpdateCoords(GetNewCoordsInDirection(direction));
        PieceMoved(direction);
        
        PiecesManager.Instance.PieceMoved?.Invoke();
    }

    public void Slide() {
        Vector2Int moveVector = Direction.DOWN.GetMovementVector() * HolesBeneathPiece();
        UpdateCoords(Coords + moveVector);
        PieceMoved(Direction.DOWN);
    }

    public bool OnGround() {
        return HolesBeneathPiece() == 0;
    }

    private int HolesBeneathPiece() {

        int holes = 0;

        for(int i = 0; i < Coords.y; i++) {
            if(!BoardManager.Instance.Occupied(new Vector2Int(Coords.x, i))) {
                holes++;
            }
        }
        
        return holes;
    }

    public void Kill() {
        BoardManager.Instance.Grid[Coords.x, Coords.y] = null;
        OnDeath?.Invoke();

        enabled = false;
    }

    public bool IsIdle() {
        return currentState == PieceIdleState;
    }

    public bool IsControlled() {
        return currentState == PieceSpawnedFallingState;
    }

    private void UpdateCoords(Vector2Int newCoords, bool resetExisting = true) {

        BoardManager boardManager = BoardManager.Instance;

        if(resetExisting) {
            boardManager.Grid[Coords.x, Coords.y] = null;
        }
        
        Coords = newCoords;
        boardManager.Grid[Coords.x, Coords.y] = this;
    }

    private bool CanMove(Direction movementDirection) {
        
        Vector2Int newCoords = GetNewCoordsInDirection(movementDirection);
        BoardManager boardManager = BoardManager.Instance;

        // If this is as far as we can move, return false
        if(!boardManager.Valid(newCoords)) {
            return false;
        }

        // If it's a valid space and it's open, we can move there
        if(!boardManager.Occupied(newCoords)) {
            return true;
        }

        // Otherwise, can still move there if the other piece is a controllable piece
        // that will slide off that space this tick
        PieceManager otherPieceManager = boardManager.Grid[newCoords.x, newCoords.y];
        return IsControlled() 
                && otherPieceManager.IsControlled() 
                && otherPieceManager.CanMove(movementDirection);
    }

    private Vector2Int GetNewCoordsInDirection(Direction direction) {
        Vector2Int movementVector = direction.GetMovementVector();
        Vector2Int newCoords = BoardManager.Instance.GetWrappedVector(Coords + movementVector);

        return newCoords;
    }
}
