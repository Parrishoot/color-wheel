using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: MAKE THIS ACTUALLY GOOD
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
        
    }

    public override void OnUpdate(float deltaTime)
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            StateMachine.ChangeState(StateMachine.GameStartingState);
        }
    }
}
