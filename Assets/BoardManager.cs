using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    [field:SerializeField]
    public float MinScale { get; private set; } = .4f;
    
    [field:SerializeField]
    public float MaxScale { get; private set; }= .8f;

    [field: SerializeField]
    public int Rows { get; private set; } = 8; 

    [field: SerializeField]
    public int Columns { get; private set; }= 6;

    [SerializeField]
public PieceManager[,] Grid { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        SetupBoard();
        TickManager.Instance.StartTicking();
    }

    void SetupBoard() {
        Grid = new PieceManager[Columns, Rows];
    }

    public bool Valid(Vector2Int coords) {
        
        if(coords.y < 0 || coords.y >= Grid.GetLength(1)) {
            return false;
        }

        return true;
    }

    public bool Occupied(Vector2Int coords) {

        if(!Valid(coords)) {
            return true;
        }

        return Grid[coords.x, coords.y] != null;
    }

    public Vector3 GetLocalRotationForXCoord(int xCoord) {
        return Vector3.forward * GetRotationPerColumn() * (Columns - 1 - xCoord);
    }

    public float GetOuterRadiusForYCoord(int yCoord) {
        float scalePerRow = GetRowRadiusScale();
        return MaxScale - (Rows - 1 - yCoord) * scalePerRow;
    }

    public float GetInnerRadiusForYCoord(int yCoord) {
        return GetOuterRadiusForYCoord(yCoord - 1);
    }

    public Vector2Int GetWrappedVector(Vector2Int coords) {
        return new Vector2Int((Columns + coords.x) % Columns, coords.y);
    }

    public bool ColumnOpen(int columnIndex) {
        return Grid[columnIndex, Rows - 1] == null;
    }

    private float GetRotationPerColumn() {
        return 360 * GetScalePerColumn();
    }

    public float GetScalePerColumn() {
        return 1 / (float) Columns;
    }

    public float GetRowRadiusScale() {
        return (MaxScale - MinScale) / (Rows + 1);
    }
}
