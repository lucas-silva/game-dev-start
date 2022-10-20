using System;
using System.Collections.Generic;
using UnityEngine;

public class ChoraoAnimation : MonoBehaviour
{
    private enum State
    {
        Idle,
        Walking,
        Running,
        Rolling
    }

    private static readonly Dictionary<State, Action<Animator>> AnimateByState = new()
    {
        { State.Idle, a => a.SetInteger("transition", 0) },
        { State.Walking, a => a.SetInteger("transition", 1) },
        { State.Running, a => a.SetInteger("transition", 2) },
        { State.Rolling, a => a.SetTrigger("rolling") },
    };

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
        var state = GetState();
        var animate = AnimateByState[state];
        animate(Animator);
    }

    private State GetState()
    {
        if (Chorao.IsRolling) return State.Rolling;
        if (Chorao.IsRunning) return State.Running;
        if (Chorao.IsMoving) return State.Walking;
        return State.Idle;
    }
}
