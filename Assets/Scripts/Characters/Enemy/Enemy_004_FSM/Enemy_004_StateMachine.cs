using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_004_StateMachine : EnemyStateMachine
{
    protected override void OnEnable()
    {
        SwitchOn(stateTable[typeof(Enemy_004_Appear)]);
    }
}
