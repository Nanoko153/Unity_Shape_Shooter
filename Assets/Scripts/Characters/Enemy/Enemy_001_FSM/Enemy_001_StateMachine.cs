using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_001_StateMachine : EnemyStateMachine
{
    protected override void OnEnable()
    {
        SwitchOn(stateTable[typeof(Enemy_001_Appear)]);
    }
}
