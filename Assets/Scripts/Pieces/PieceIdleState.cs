using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceIdleState : GenericState<PieceManager>
{

    public PieceIdleState(PieceManager stateMachine) : base(stateMachine)
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
        if(!StateMachine.OnGround()) {
            StateMachine.ChangeState(StateMachine.PieceFallingState);
        }
    }
}
