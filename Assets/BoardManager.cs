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

    [SerializeField]
    public int rows = 8;

    [SerializeField]
    private int columns = 6;

    [SerializeField]
    private GameObject piecePrefab;

    [SerializeField]
    public PieceManager[,] Grid { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        SetupBoard();
    }

    void SetupBoard() {

        Grid = new PieceManager[columns, rows];

        for(int i = 0; i < columns; i++) {
            for(int j = 0; j < rows; j++) {

                GameObject piece = Instantiate(piecePrefab, transform);
                SpriteRenderer pieceSpriteRenderer = piece.GetComponent<SpriteRenderer>();
                PieceManager pieceManager = piece.GetComponent<PieceManager>();

                pieceManager.Init(new Vector2Int(j, i));

                pieceSpriteRenderer.material.SetFloat("_FillAmount", GetScalePerColumn());
                pieceSpriteRenderer.material.SetFloat("_ChunkSize", GetScalePerRow());

                piece.transform.localEulerAngles = GetLocalRotationForXCoord(i);
                piece.transform.localScale = GetLocalScaleForYCoord(j);
            }
        }
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
        return Vector3.forward * GetRotationPerColumn() * xCoord;
    }

    public Vector3 GetLocalScaleForYCoord(int yCoord) {
        float scalePerRow = GetScalePerRow();
        return (maxScale - (yCoord * scalePerRow)) * Vector3.one;
    }

    public Vector2Int GetWrappedVector(Vector2Int coords) {
        Debug.Log(coords);
        Debug.Log(new Vector2Int((columns + coords.x) % columns, coords.y));

        return new Vector2Int((columns + coords.x) % columns, coords.y);
    }

    private float GetRotationPerColumn() {
        return 360 * GetScalePerColumn();
    }

    private float GetScalePerColumn() {
        return 1 / (float) columns;
    }

    private float GetScalePerRow() {
        return (maxScale - minScale) / rows;
    }
}
