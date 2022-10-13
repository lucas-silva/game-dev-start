using System.Collections.Generic;
using UnityEngine;

public class ChoraoAnimation : MonoBehaviour
{
    private static readonly string transitionParameterName = "transition";

    private Chorao chorao;

    private Animator animator;

    public void Start()
    {
        chorao = GetComponent<Chorao>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        SetAnimationByState();
        RotateTowardsMovingDirection();
    }

    private void SetAnimationByState()
    {
        animator.SetInteger(transitionParameterName, (int)GetAnimationState());
    }

    private AnimationState GetAnimationState()
    {
        if (chorao.IsRunning) return AnimationState.Running;
        if (chorao.IsMoving) return AnimationState.Walking;
        return AnimationState.Idle;
    }

    private void RotateTowardsMovingDirection()
    {
        var direction = GetDirection();
        if (direction.HasValue) transform.eulerAngles = DirectionMap[direction.Value];
    }

    private Direction? GetDirection()
    {
        if (chorao.direction.x > 0) return Direction.Right;
        if (chorao.direction.x < 0) return Direction.Left;
        return null;
    }

    private enum AnimationState
    {
        Idle = 0,
        Walking = 1,
        Running = 2,
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
