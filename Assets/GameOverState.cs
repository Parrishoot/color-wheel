using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : GenericState<GameManager>
{
    public GameOverState(GameManager stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnd()
    {
        
    }

    public override void OnStart()
    {
        // TODO: ADD ACTUAL GAME OVER LOGIC HERE
        Debug.LogWarning("Game Over!");
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }
}
