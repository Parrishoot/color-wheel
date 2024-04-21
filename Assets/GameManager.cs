using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : StateMachine
{
    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    // STATES
    public GameStartingState GameStartingState { get; protected set; }
    public GameSpawnState GameSpawnState { get; protected set; }
    public GameWaitForSettleState GameWaitForSettleState { get; protected set; }
    public GameScoringState GameScoringState { get; protected set; }
    public GameOverState GameOverState { get; protected set; }

    protected virtual void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton() {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    public void Start() {
        GameStartingState = new GameStartingState(this);
        GameSpawnState = new GameSpawnState(this);
        GameWaitForSettleState = new GameWaitForSettleState(this);
        GameScoringState = new GameScoringState(this);
        GameOverState = new GameOverState(this);

        ChangeState(GameStartingState);
    }

    public List<PieceManager> GetActivePieces() {
        return new List<PieceManager>(FindObjectsOfType<PieceManager>());
    }
}
