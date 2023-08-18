using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : ScriptableObject, IState
{
    protected Animator anim;
    protected EnemyController enemy;
    protected StateMachine stateMachine;

    float stateStartTime;
    protected bool IsAnimFinished => StateDuration >= anim.GetCurrentAnimatorStateInfo(0).length;
    protected float StateDuration => Time.time - stateStartTime;

    public void Init(Animator anim,EnemyController enemy, StateMachine stateMachine)
    {
        this.anim = anim;
        this.enemy = enemy;
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
