using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker
{
    public const int POINTS_PER_PIECE = 100;
    public const int POINTS_PER_EXTRA_PIECE = 150;

    private int currentScore = 0;

    private int currentMultipler = 1;

    // Update is called once per frame
    public void AddScore(int numPieces) {

        int baseScore = Cluster.MIN_PIECES * POINTS_PER_PIECE;
        int extraPieces = numPieces - Cluster.MIN_PIECES;

        currentScore += baseScore + extraPieces * POINTS_PER_EXTRA_PIECE;
        currentMultipler++;
    }

    public int CalcScore() {
        return currentScore * currentMultipler;
    }
}
