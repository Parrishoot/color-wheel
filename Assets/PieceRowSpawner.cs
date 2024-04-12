using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PieceRowSpawner : Singleton<PieceRowSpawner>
{
    [SerializeField]
    [Range(2, 5)]
    public int maxSpawnPieces = 3; 
    
    [SerializeField]
    private GameObject piecePrefab;

    private BoardManager boardManager;

    void Start() {
        boardManager = BoardManager.Instance;
        TickManager.Instance.OnTick += SpawnRow;
    }

    public void SpawnRow() {
        
        List<int> availableColumns = Enumerable.Range(0, boardManager.Columns)
                                               .Where(x => boardManager.ColumnOpen(x))
                                               .ToList();

        if(availableColumns.Count == 0) {
            Debug.LogWarning("No more available columns");
            return;
        }

        int numPiecesToSpawn = Random.Range(1, System.Math.Min(availableColumns.Count, maxSpawnPieces));
                                      
        List<int> spawnColumns = availableColumns.OrderBy(x => Random.Range(0, boardManager.Columns))
                                                 .Take(System.Math.Min(availableColumns.Count, maxSpawnPieces))
                                                 .ToList();

        foreach(int column in spawnColumns) {
            SpawnPiece(column);
        }

    } 

    private void SpawnPiece(int columnIndex) {

        GameObject piece = Instantiate(piecePrefab, transform);
        SpriteRenderer pieceSpriteRenderer = piece.GetComponent<SpriteRenderer>();
        PieceManager pieceManager = piece.GetComponent<PieceManager>();

        pieceManager.Init(new Vector2Int(columnIndex, boardManager.Rows - 1));

        pieceSpriteRenderer.material.SetFloat("_FillAmount", boardManager.GetScalePerColumn());
        pieceSpriteRenderer.material.SetFloat("_ChunkSize", boardManager.GetScalePerRow());

        piece.transform.localEulerAngles = boardManager.GetLocalRotationForXCoord(columnIndex);
        piece.transform.localScale = boardManager.GetLocalScaleForYCoord(boardManager.Rows - 1);

    }
}
