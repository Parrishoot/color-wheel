using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker
{
    public const int POINTS_PER_PIECE = 100;
    public const int POINTS_PER_EXTRA_PIECE = 150;

    public int CurrentScore { get; protected set; } = 0;

    public int CurrentMultipler { get; protected set; } = 0;

    // Update is called once per frame
    public void AddScore(int numPieces) {

        int baseScore = Cluster.MIN_PIECES * POINTS_PER_PIECE;
        int extraPieces = numPieces - Cluster.MIN_PIECES;

        CurrentScore += baseScore + extraPieces * POINTS_PER_EXTRA_PIECE;
        CurrentMultipler++;
    }

    public int CalcScore() {
        return CurrentScore * CurrentMultipler;
    }
}
