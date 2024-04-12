using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    [SerializeField]
    private float minScale = .4f;
    
    [SerializeField]
    private float maxScale = 1f;

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
        return Vector3.forward * GetRotationPerColumn() * (Rows - 1 - xCoord);
    }

    public Vector3 GetLocalScaleForYCoord(int yCoord) {
        float scalePerRow = GetScalePerRow();
        return (maxScale - ((Columns - 1 - yCoord) * scalePerRow)) * Vector3.one;
    }

    public Vector2Int GetWrappedVector(Vector2Int coords) {
        Debug.Log(coords);
        Debug.Log(new Vector2Int((Columns + coords.x) % Columns, coords.y));

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

    public float GetScalePerRow() {
        return (maxScale - minScale) / Rows;
    }
}
