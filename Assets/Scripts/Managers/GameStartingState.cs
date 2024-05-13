using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartingState: GenericState<GameManager>
{
    public GameStartingState(GameManager stateMachine) : base(stateMachine)
    {
        
    }

    public override void OnEnd()
    {
        StateMachine.GameStarted?.Invoke();
    }

    public override void OnStart()
    {
        TickManager.Instance.StartTicking();

        StateMachine.ChangeState(StateMachine.GameSpawnState);
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }
}
