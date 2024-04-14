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
        InputManager.Instance.OnInput += StateMachine.Move;
    }

    public override void OnEnd()
    {        
        base.OnEnd();
        InputManager.Instance.OnInput -= StateMachine.Move;
    }
}
