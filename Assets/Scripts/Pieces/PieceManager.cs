using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.U2D.IK;

public class PieceManager : StateMachine
{
    public Vector2Int? Coords { get; private set;}
    
    public Action<Direction> PieceMoved { get; set; }

    public Action OnDeath { get; set; }

    // States
    public PieceFallingState PieceFallingState { get; protected set; }
    public PieceSpawnedFallingState PieceSpawnedFallingState { get; protected set; }
    public PieceIdleState PieceIdleState { get; protected set; }

    [field:SerializeField]
    public PieceClusterManager PieceClusterManager { get; protected set; }

    private List<Vector2Int> prevCoords = new List<Vector2Int>();

    // Hacky but it gets the job done
    public Action<Direction> QueuedMove;

    public void Init(Vector2Int coords) {

        PieceFallingState = new PieceFallingState(this);
        PieceSpawnedFallingState = new PieceSpawnedFallingState(this);
        PieceIdleState = new PieceIdleState(this);

        UpdateCoords(coords);
        ChangeState(PieceSpawnedFallingState);

        PiecesManager.Instance.PieceSpawned?.Invoke(this);
    }

    public void Move(Direction direction) {

        if(!CanMove(direction)) {
            return;
        }

        UpdateCoords(GetNewCoordsInDirection(direction));
        PieceMoved?.Invoke(direction);
        PieceClusterManager.ResetCluster();
        
        PiecesManager.Instance.PieceMoved?.Invoke();
    }

    public void Slide() {
        Vector2Int moveVector = Direction.DOWN.GetMovementVector() * HolesBeneathPiece();
        UpdateCoords(Coords.Value + moveVector);

        PieceClusterManager.ResetCluster();

        PieceMoved?.Invoke(Direction.DOWN);
    }

    public bool OnGround() {
        return HolesBeneathPiece() == 0;
    }

    private int HolesBeneathPiece() {

        int holes = 0;

        for(int i = 0; i < Coords.Value.y; i++) {
            if(!BoardManager.Instance.Occupied(new Vector2Int(Coords.Value.x, i))) {
                holes++;
            }
        }
        
        return holes;
    }

    public void Kill() {
        BoardManager.Instance.Grid[Coords.Value.x, Coords.Value.y] = null;
        OnDeath?.Invoke();

        enabled = false;
    }

    public bool IsIdle() {
        return currentState == PieceIdleState;
    }

    public bool IsControlled() {
        return currentState == PieceSpawnedFallingState;
    }

    private void UpdateCoords(Vector2Int newCoords) {

        BoardManager boardManager = BoardManager.Instance;

        if(Coords != null) {

            if(boardManager.Grid[Coords.Value.x, Coords.Value.y] != this) {
                Debug.LogWarning("This is bad!");
            }

            boardManager.Grid[Coords.Value.x, Coords.Value.y] = null;
            prevCoords.Add(Coords.Value);
        }

        boardManager.Grid[newCoords.x, newCoords.y] = this;
        Coords = newCoords;
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

        // BUG: IF TWO PIECES ARE MOVING, IT CAN GET

        // Otherwise, can still move there if the other piece is a controllable piece
        // that will slide off that space this tick. But want want to make sure to process that
        // movement in the correct order
        if(!IsControlled()) {
            return false;
        }

        PieceManager otherPieceManager = boardManager.Grid[newCoords.x, newCoords.y];
        if(otherPieceManager.IsControlled() 
           && otherPieceManager.CanMove(movementDirection)) {
            QueuedMove = (direction) => ProcessQueuedMove(movementDirection, otherPieceManager);
            otherPieceManager.PieceMoved += QueuedMove;
        }

        return false;
    }

    private void ProcessQueuedMove(Direction movementDirection, PieceManager otherPiece) {
        Move(movementDirection);
        otherPiece.PieceMoved -= QueuedMove;

        QueuedMove = null;
    }

    private Vector2Int GetNewCoordsInDirection(Direction direction) {
        Vector2Int movementVector = direction.GetMovementVector();
        Vector2Int newCoords = BoardManager.Instance.GetWrappedVector(Coords.Value + movementVector);

        return newCoords;
    }

    protected override void Update() {
        
        base.Update();

        if(BoardManager.Instance.Grid[Coords.Value.x, Coords.Value.y] != this) {
            Debug.LogWarning("THIS IS BAD NEWS!");
        }
    }
}
