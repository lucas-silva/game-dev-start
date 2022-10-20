using System;
using System.Collections.Generic;
using UnityEngine;

public class ChoraoAnimation : MonoBehaviour
{
    private Chorao Chorao;

    private Animator Animator;

    public void Start()
    {
        Chorao = GetComponent<Chorao>();
        Animator = GetComponent<Animator>();
    }

    public void Update() => SetAnimationByState();

    private void SetAnimationByState()
    {
        var state = GetAnimationState();
        var animateAction = AnimationMap[state];
        animateAction(Animator);
    }

    private AnimationState GetAnimationState()
    {
        if (Chorao.IsRolling) return AnimationState.Rolling;
        if (Chorao.IsRunning) return AnimationState.Running;
        if (Chorao.IsMoving) return AnimationState.Walking;
        return AnimationState.Idle;
    }

    private readonly Dictionary<AnimationState, Action<Animator>> AnimationMap = new()
    {
        { AnimationState.Idle, (Animator a) => a.SetInteger(TransitionParameter, 0) },
        { AnimationState.Walking, (Animator a) => a.SetInteger(TransitionParameter, 1) },
        { AnimationState.Running, (Animator a) => a.SetInteger(TransitionParameter, 2) },
        { AnimationState.Rolling, (Animator a) => a.SetTrigger(RollingParameter) },
    };

    private enum AnimationState
    {
        Idle,
        Walking,
        Running,
        Rolling
    }

    private static readonly string TransitionParameter = "transition";

    private static readonly string RollingParameter = "rolling";
}
