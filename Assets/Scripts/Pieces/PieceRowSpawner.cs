using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PieceRowSpawner : Singleton<PieceRowSpawner>
{
    [SerializeField]
    [Range(1, 5)]
    public int maxSpawnPieces = 3; 
    
    [SerializeField]
    private GameObject piecePrefab;

    private BoardManager boardManager;

    void Start() {
        boardManager = BoardManager.Instance;
        // TickManager.Instance.OnTick += SpawnRow;
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

    public void SpawnPiece(int columnIndex, int? rowIndex = null) {

        if(rowIndex == null) {
            rowIndex = boardManager.Rows - 1;
        }

        GameObject piece = Instantiate(piecePrefab, transform);
        SpriteRenderer pieceSpriteRenderer = piece.GetComponent<SpriteRenderer>();
        PieceManager pieceManager = piece.GetComponent<PieceManager>();

        pieceManager.Init(new Vector2Int(columnIndex, rowIndex.Value));

        pieceSpriteRenderer.material.SetFloat("_Fill", BoardManager.Instance.GetScalePerColumn());

        pieceSpriteRenderer.material.SetFloat("_OuterRadius", BoardManager.Instance.GetOuterRadiusForYCoord(rowIndex.Value));
        pieceSpriteRenderer.material.SetFloat("_InnerRadius", BoardManager.Instance.GetInnerRadiusForYCoord(rowIndex.Value));

        Color newColor = Color.white;
        newColor.r = UnityEngine.Random.Range(0f, 1f);
        newColor.b = UnityEngine.Random.Range(0f, 1f);
        newColor.g = UnityEngine.Random.Range(0f, 1f);

        pieceSpriteRenderer.color = newColor;

        piece.transform.localEulerAngles = BoardManager.Instance.GetLocalRotationForXCoord(columnIndex);

    }
}
