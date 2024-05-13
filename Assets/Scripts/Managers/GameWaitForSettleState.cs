using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaitForSettleState : GenericState<GameManager>
{
    public GameWaitForSettleState(GameManager stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnd()
    {
        TickManager.Instance.OnTick -= CheckNextState;
    }

    public override void OnStart()
    {
        TickManager.Instance.OnTick += CheckNextState;
    }

    public override void OnUpdate(float deltaTime)
    {
    }

    private void CheckNextState() {

        if(PiecesFalling()) {
            return;
        }

        StateMachine.ChangeState(StateMachine.GameScoringState);        
    }

    private bool PiecesFalling() {
        
        foreach(PieceManager pieceManager in StateMachine.GetActivePieces()) {
            if(!pieceManager.IsIdle()) {
                return true;
            }
        }

        return false;
    }
}
