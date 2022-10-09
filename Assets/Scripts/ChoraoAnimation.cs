using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoraoAnimation : MonoBehaviour
{
    static readonly string transitionParameterName = "transition";

    private Chorao chorao;

    private Animator animator;

    private enum AnimationState
    {
        idle = 0,
        walking = 1,
    }

    private enum Direction
    {
        Right,
        Left
    }

    static readonly Dictionary<Direction, Vector2> DirectionMap = new Dictionary<Direction, Vector2>
    {
        { Direction.Right, new Vector2(0, 0) },
        { Direction.Left, new Vector2(0, 180) },
    };

    void Start()
    {
        chorao = GetComponent<Chorao>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetInteger(transitionParameterName, (int)GetAnimationState());

        var direction = GetDirection();
        if (direction.HasValue) transform.eulerAngles = DirectionMap[direction.Value];
    }

    private AnimationState GetAnimationState()
    {
        var isMoving = chorao.direction.sqrMagnitude > 0;
        var state = isMoving ? AnimationState.walking : AnimationState.idle;
        return state;
    }

    private Direction? GetDirection()
    {
        if (chorao.direction.x > 0) return Direction.Right;
        if (chorao.direction.x < 0) return Direction.Left;
        return null;
    }
}
