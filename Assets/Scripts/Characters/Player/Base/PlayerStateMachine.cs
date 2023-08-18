using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Animator anim;
    //public Animator anim_attackPos;

    [HideInInspector]
    public PlayerController player;

    PlayerActionsInput input;
    [Header("States")]

    //集合形数组，用来统一管理状态类
    [SerializeField] PlayerState[] states;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        player = GetComponent<PlayerController>();

        input = GetComponent<PlayerActionsInput>();

        stateTable = new Dictionary<System.Type, IState>(states.Length);

        foreach(PlayerState state in states)
        {
            state.Init(anim, player, input, this);
            stateTable.Add(state.GetType(),state);
        }
    }

    void Start()
    {
        SwitchOn(stateTable[typeof(Player_Mode_1)]);
    }
}
