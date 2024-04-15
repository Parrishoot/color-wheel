using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluster
{
    private const int MIN_PIECES = 4;

    public int ColorIndex { get; protected set; }

    public LinkedList<PieceClusterManager> PiecesInCluster;

    public Cluster(int colorIndex, PieceClusterManager pieceClusterManager) {
        this.ColorIndex = colorIndex;
        this.PiecesInCluster = new LinkedList<PieceClusterManager>();
        this.PiecesInCluster.AddFirst(pieceClusterManager);
    }

    public void MergeClusters(Cluster otherCluster) {

        // BUG: INIFITE LOOP HERE WHEN TWO ONE PIECE CLUSTERS MERGE
        while(otherCluster.PiecesInCluster.Count > 0) {
            
            PieceClusterManager pieceClusterManager = otherCluster.PiecesInCluster.First.Value;
            otherCluster.PiecesInCluster.RemoveFirst();
            
            pieceClusterManager.CurrentCluster = this;
            PiecesInCluster.AddLast(pieceClusterManager);
        }
    }

    public void DestroyCluster() {

        foreach(PieceClusterManager pieceClusterManager in PiecesInCluster) {
            pieceClusterManager.PieceManager.Destroy();
        }
    }

    public bool SizeReached() {
        return PiecesInCluster.Count >= MIN_PIECES;
    }
}
