using System;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Action<Direction> OnInput;

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale.Equals(0)) {
            return;
        }

        if(Input.GetKeyDown(KeyCode.D)) {
            OnInput?.Invoke(Direction.RIGHT);
        }
        else if(Input.GetKeyDown(KeyCode.A)) {
            OnInput?.Invoke(Direction.LEFT);
        }

        // TODO: REEVALUATE WHETHER OR NOT A PLAYER SHOULD BE ABLE TO DO THIS
        // else if(Input.GetKeyDown(KeyCode.S)) {
        //     OnInput?.Invoke(Direction.DOWN);
        // }
    }
}
