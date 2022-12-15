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

    public void Update() => Animate();

    private Action Animate => Chorao switch
    {
        { IsRolling: true } => () => Animator.SetTrigger("rolling"),
        { IsWalking: true } => () => Animator.SetInteger("transition", 1),
        { IsRunning: true } => () => Animator.SetInteger("transition", 2),
        _ => () => Animator.SetInteger("transition", 0),
    };
}
