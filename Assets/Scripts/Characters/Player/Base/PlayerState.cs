using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    protected Animator anim;
    protected PlayerController player;
    protected PlayerActionsInput input;
    protected PlayerStateMachine stateMachine;

    float stateStartTime;
    protected bool IsAnimFinished => StateCurrentDurationTime >= anim.GetCurrentAnimatorStateInfo(0).length;
    protected float StateCurrentDurationTime => Time.time - stateStartTime;
    protected float currentAnimatorTime => anim.GetCurrentAnimatorStateInfo(0).length;
    public void Init(Animator anim, PlayerController player,PlayerActionsInput input,PlayerStateMachine stateMachine)
    {
        this.anim = anim;
        this.player = player;
        this.input = input;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        stateStartTime=Time.time;
    }

    public virtual void Exit()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicUpdate()
    {

    }
}
