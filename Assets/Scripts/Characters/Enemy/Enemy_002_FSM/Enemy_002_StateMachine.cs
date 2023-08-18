using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_002_StateMachine : EnemyStateMachine
{
    protected override void OnEnable()
    {
        SwitchOn(stateTable[typeof(Enemy_002_Appear)]);
    }
}
