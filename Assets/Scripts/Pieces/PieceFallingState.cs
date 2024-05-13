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
        StateMachine.PieceClusterManager.CheckClusters();
        TickManager.Instance.OnTick -= Slide;
    }

    public override void OnStart()
    { 
        TickManager.Instance.OnTick += Slide;
    }

    public override void OnUpdate(float deltaTime)
    {
        if(StateMachine.OnGround()) {
            StateMachine.ChangeState(StateMachine.PieceIdleState);
        }
    }

    protected virtual void Slide() {
        StateMachine.Slide();
    }
}
