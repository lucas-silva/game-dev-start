using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoraoAnimation : MonoBehaviour
{
    private Chorao chorao;

    private Animator animator;

    private enum AnimationState
    {
        idle = 0,
        walking = 1,
    }

    void Start()
    {
        chorao = GetComponent<Chorao>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetInteger("transition", (int)GetAnimationState());
    }

    private AnimationState GetAnimationState()
    {
        var isMoving = chorao.direction.sqrMagnitude > 0;
        var state = isMoving ? AnimationState.walking : AnimationState.idle;
        return state;
    }
}
