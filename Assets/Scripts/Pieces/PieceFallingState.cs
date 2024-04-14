using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFallingState : GenericState<PieceManager>
{
    public PieceFallingState(PieceManager stateMachine) : base(stateMachine)
    {

    }

    public override void OnEnd()
    {
        TickManager.Instance.OnTick -= () => StateMachine.Move(Direction.DOWN);
    }

    public override void OnStart()
    {
        TickManager.Instance.OnTick += () => StateMachine.Move(Direction.DOWN);
    }

    public override void OnUpdate(float deltaTime)
    {
        if(StateMachine.OnGround()) {
            StateMachine.ChangeState(StateMachine.PieceIdleState);
        }
    }
}
