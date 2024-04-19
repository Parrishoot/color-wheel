using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceClusterManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [field:SerializeField]
    public PieceManager PieceManager { get; private set; }

    public Cluster CurrentCluster { get; set; }

    protected String guid;

    // Start is called before the first frame update
    void Start()
    {
        InitCluster();
        guid = Guid.NewGuid().ToString();
    }

    public void ResetCluster() {

        if(CurrentCluster == null) {
            InitCluster();
        }

        int colorIndex = CurrentCluster.ColorIndex;

        CurrentCluster.PiecesInCluster.Remove(this);
        CurrentCluster = new Cluster(colorIndex, this);
    }

    public void CheckClusters() {
        foreach(Direction direction in Enum.GetValues(typeof(Direction))) {
            Vector2Int spotToCheck = BoardManager.Instance.GetWrappedVector(PieceManager.Coords + direction.GetMovementVector());

            if(!CanMergeClustersAtLocation(spotToCheck)) {
                continue;
            }

            CurrentCluster.MergeClusters(BoardManager.Instance.Grid[spotToCheck.x, spotToCheck.y].PieceClusterManager.CurrentCluster);
        }

        if(CurrentCluster.SizeReached()) {
            CameraController.Instance.Shake(.7f, .1f);
            ParticleBurstController.Instance.Burst();
            CurrentCluster.DestroyCluster();
        }
    }

    private bool CanMergeClustersAtLocation(Vector2Int spotToCheck) {
        return BoardManager.Instance.Valid(spotToCheck) && 
               BoardManager.Instance.Occupied(spotToCheck) && 
               BoardManager.Instance.Grid[spotToCheck.x, spotToCheck.y].PieceClusterManager.CurrentCluster.ColorIndex == CurrentCluster.ColorIndex;
    }

    private void InitCluster() {
        int colorIndex = ColorManager.Instance.GetRandomColorIndex();
        spriteRenderer.color = ColorManager.Instance.Colors[colorIndex];

        CurrentCluster = new Cluster(colorIndex, this);
    }
}
