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
        TickManager.Instance.OnTick -= Slide;
    }

    public override void OnStart()
    {
        StateMachine.PieceClusterManager.ResetCluster();
        TickManager.Instance.OnTick += Slide;
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }

    protected virtual void Slide() {
        if(StateMachine.OnGround()) {
            StateMachine.ChangeState(StateMachine.PieceIdleState);
        }
        else {
            StateMachine.Slide();
        }
    }
}
