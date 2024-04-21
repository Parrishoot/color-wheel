using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoringState : GenericState<GameManager>
{
    private ScoreTracker scoreTracker;

    public GameScoringState(GameManager stateMachine) : base(stateMachine)
    {
        scoreTracker = new ScoreTracker();
    }

    public override void OnEnd()
    {
        
    }

    public override void OnStart()
    {
        HashSet<Cluster> clusters = new HashSet<Cluster>();

        // Find all active clusters
        foreach(PieceManager pieceManager in StateMachine.GetActivePieces()) {
            clusters.Add(pieceManager.PieceClusterManager.CurrentCluster);
        }

        bool clusterPopped = false;

        foreach(Cluster cluster in clusters) {
            if(cluster.SizeReached()) {
                scoreTracker.AddScore(cluster.Size());
                clusterPopped = true;
                cluster.DestroyCluster();
            }
        }

        if(clusterPopped) {
            StateMachine.ChangeState(StateMachine.GameWaitForSettleState);
            return;
        }

        ScoreController.Instance.AddScore(scoreTracker.CalcScore());
        StateMachine.ChangeState(StateMachine.GameSpawnState);

        scoreTracker = new ScoreTracker();
        
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }
}
