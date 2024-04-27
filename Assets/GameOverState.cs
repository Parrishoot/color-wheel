using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        StateMachine.GameOver?.Invoke();
    }

    public override void OnUpdate(float deltaTime)
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
