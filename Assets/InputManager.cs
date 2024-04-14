using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Action<Direction> OnInput;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D)) {
            OnInput?.Invoke(Direction.RIGHT);
        }
        else if(Input.GetKeyDown(KeyCode.A)) {
            OnInput?.Invoke(Direction.LEFT);
        }
        else if(Input.GetKeyDown(KeyCode.S)) {
            OnInput?.Invoke(Direction.DOWN);
        }
        
        // TODO: REMOVE THIS
        if(Input.GetKeyDown(KeyCode.Space)) {
            PieceRowSpawner.Instance.SpawnRow();
        }
    }
}
