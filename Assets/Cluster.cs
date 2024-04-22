using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluster
{
    public const int MIN_PIECES = 4;

    public int ColorIndex { get; protected set; }

    public LinkedList<PieceClusterManager> PiecesInCluster;

    private bool popped = false;

    public Cluster(int colorIndex, PieceClusterManager pieceClusterManager) {
        this.ColorIndex = colorIndex;
        this.PiecesInCluster = new LinkedList<PieceClusterManager>();
        this.PiecesInCluster.AddFirst(pieceClusterManager);
    }

    public void MergeClusters(Cluster otherCluster) {

        if(otherCluster == this) {
            return;
        }

        while(otherCluster.PiecesInCluster.Count > 0) {
            
            PieceClusterManager pieceClusterManager = otherCluster.PiecesInCluster.First.Value;
            otherCluster.PiecesInCluster.RemoveFirst();
            
            pieceClusterManager.CurrentCluster = this;
            PiecesInCluster.AddLast(pieceClusterManager);
        }
    }

    public void DestroyCluster() {

        if(popped) {
            return;
        }

        foreach(PieceClusterManager pieceClusterManager in PiecesInCluster) {
            pieceClusterManager.PieceManager.Kill();
        }

        popped = true;

        // Cluster Feedback
        // TODO: MAYBE MOVE THIS TO AN EVENT?
        CameraController.Instance.Shake(.7f, .1f);
        ParticleBurstController.Instance.Burst();
        SpeedRampManager.Instance.ClusterPopped();
    }

    public bool SizeReached() {
        return  Size() >= MIN_PIECES;
    }

    public int Size() {
        return PiecesInCluster.Count;
    }

    public bool Popped() {
        return popped;
    }
}
