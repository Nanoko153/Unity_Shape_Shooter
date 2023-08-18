using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E00_StateMachine : StateMachine
{
    public Animator anim;

    EnemyController enemy;

    [Header("States")]

    //���������飬����ͳһ����״̬��
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

    void OnEnable()
    {
        SwitchOn(stateTable[typeof(E00_Move)]);
    }
}
