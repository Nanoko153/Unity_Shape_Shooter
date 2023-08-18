using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_003_StateMachine : EnemyStateMachine
{
    protected override void OnEnable()
    {
        SwitchOn(stateTable[typeof(Enemy_003_Appear)]);
    }
}
