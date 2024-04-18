using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawnedFallingState : PieceFallingState
{
    public PieceSpawnedFallingState(PieceManager stateMachine) : base(stateMachine)
    {
    }

    public override void OnStart()
    {
        base.OnStart();
        InputManager.Instance.OnInput += Move;
    }

    public override void OnEnd()
    {        
        base.OnEnd();
        InputManager.Instance.OnInput -= Move;
    }

    private void Move(Direction direction) {
        StateMachine.Move(direction);
    }

    protected override void Slide()
    {
        if(StateMachine.OnGround()) {
            StateMachine.ChangeState(StateMachine.PieceIdleState);
        }
        else {
            StateMachine.Move(Direction.DOWN);
        }
    }
}
