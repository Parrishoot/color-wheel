using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawnState : GenericState<GameManager>
{
    public GameSpawnState(GameManager stateMachine) : base(stateMachine)
    {

    }

    public override void OnEnd()
    {
        
    }

    public override void OnStart()
    {
        PieceRowSpawner.Instance.SpawnRow();
        StateMachine.ChangeState(StateMachine.GameWaitForSettleState);
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }
}
