using System;
using UnityEngine;

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public static class DirectionExtensions {

    public static Vector2Int GetMovementVector(this Direction direction) {
        return direction switch
        {
            Direction.LEFT => Vector2Int.left,
            Direction.RIGHT => Vector2Int.right,
            Direction.UP => Vector2Int.up,
            Direction.DOWN => Vector2Int.down,
            _ => throw new NotImplementedException()
        };
    }
}
