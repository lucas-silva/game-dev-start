using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsInputDirection : MonoBehaviour
{
    public void Update()
    {
        var direction = GetDirection();

        if (direction.HasValue) 
            transform.eulerAngles = DirectionMap[direction.Value];
    }

    private Direction? GetDirection()
    {
        var x = Input.GetAxisRaw("Horizontal");
        if (x  > 0) return Direction.Right;
        if (x < 0) return Direction.Left;
        return null;
    }

    private enum Direction
    {
        Right,
        Left
    }

    private static readonly Dictionary<Direction, Vector2> DirectionMap = new()
    {
        { Direction.Right, new Vector2(0, 0) },
        { Direction.Left, new Vector2(0, 180) },
    };
}
