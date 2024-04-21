using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PieceRowSpawner : Singleton<PieceRowSpawner>
{
    [SerializeField]
    [Range(1, 5)]
    public int maxSpawnPieces = 3; 
    
    [SerializeField]
    private GameObject piecePrefab;

    public Action PiecesPlaced;

    private BoardManager boardManager;

    private List<PieceManager> currentPieces;

    void Start() {
        currentPieces = new List<PieceManager>();
    }

    private void CheckSpawnRow() {
        
        foreach(PieceManager pieceManager in currentPieces) {
            if(pieceManager.IsControlled()) {
                return;
            }
        }

        PiecesPlaced?.Invoke();
    }

    public void SpawnRow() {

        BoardManager boardManager = BoardManager.Instance;

        currentPieces = new List<PieceManager>();

        List<int> availableColumns = Enumerable.Range(0, boardManager.Columns)
                                               .Where(x => boardManager.ColumnOpen(x))
                                               .ToList();

        if(availableColumns.Count == 0) {
            Debug.LogWarning("No more available columns");
            return;
        }

        int numPiecesToSpawn = UnityEngine.Random.Range(1, Math.Min(availableColumns.Count, maxSpawnPieces));
                                      
        List<int> spawnColumns = availableColumns.OrderBy(x => UnityEngine.Random.Range(0, boardManager.Columns))
                                                 .Take(Math.Min(availableColumns.Count, maxSpawnPieces))
                                                 .ToList();

        foreach(int column in spawnColumns) {
            SpawnPiece(column);
        }

        CameraController.Instance.Shake(.1f, .07f);

    } 

    public void SpawnPiece(int columnIndex, int? rowIndex = null) {

        if(rowIndex == null) {
            rowIndex = BoardManager.Instance.Rows - 1;
        }

        GameObject piece = Instantiate(piecePrefab, transform);
        SpriteRenderer pieceSpriteRenderer = piece.GetComponent<SpriteRenderer>();
        PieceManager pieceManager = piece.GetComponent<PieceManager>();

        pieceManager.Init(new Vector2Int(columnIndex, rowIndex.Value));

        pieceSpriteRenderer.material.SetFloat("_Fill", BoardManager.Instance.GetScalePerColumn());

        pieceSpriteRenderer.material.SetFloat("_OuterRadius", BoardManager.Instance.GetOuterRadiusForYCoord(rowIndex.Value));
        pieceSpriteRenderer.material.SetFloat("_InnerRadius", BoardManager.Instance.GetInnerRadiusForYCoord(rowIndex.Value));

        piece.transform.localEulerAngles = BoardManager.Instance.GetLocalRotationForXCoord(columnIndex);

        currentPieces.Add(pieceManager);
    }
}
