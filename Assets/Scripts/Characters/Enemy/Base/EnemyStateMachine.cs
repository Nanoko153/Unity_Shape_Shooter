using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Animator anim;

    EnemyController enemy;

    [Header("States")]

    //集合形数组，用来统一管理状态类
    [SerializeField] EnemyState[] states;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        enemy = GetComponent<EnemyController>();

        stateTable = new Dictionary<System.Type, IState>(states.Length);

        foreach(EnemyState state in states)
        {
            EnemyState newState = Instantiate(state);
            newState.Init(anim, enemy, this);
            stateTable.Add(state.GetType(),newState);
        }
    }

    protected virtual void OnEnable()
    {
        SwitchOn(stateTable[typeof(BaseEnemy_Move)]);
    }
}
