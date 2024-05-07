using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameState : GenericState<GameManager>
{
    public PreGameState(GameManager stateMachine) : base(stateMachine)
    {

    }

    public override void OnEnd()
    {
        
    }

    public override void OnStart()
    {
        BoardManager.Instance.SetupBoard();
    }

    public override void OnUpdate(float deltaTime)
    {

    }
}
