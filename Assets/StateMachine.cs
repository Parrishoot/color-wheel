using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State currentState { get; private set;}

    // Update is called once per frame
    protected virtual void Update()
    {
        currentState?.OnUpdate(Time.deltaTime);
    }

    protected void FixedUpdate() {
        currentState?.OnFixedUpdate(Time.fixedDeltaTime);    
    }

    public void ChangeState(State newState) {
        currentState?.OnEnd();

        currentState = newState;
        newState.OnStart();
    }
}