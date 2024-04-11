using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private PieceManager pieceManagerToMove;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D)) {
            pieceManagerToMove?.Move(Direction.RIGHT);
        }
        else if(Input.GetKeyDown(KeyCode.A)) {
            pieceManagerToMove?.Move(Direction.LEFT);
        }
        else if(Input.GetKeyDown(KeyCode.S)) {
            pieceManagerToMove?.Move(Direction.DOWN);
        }
        else if(Input.GetKeyDown(KeyCode.W)) {
            pieceManagerToMove?.Move(Direction.UP);
        }
    }
}
