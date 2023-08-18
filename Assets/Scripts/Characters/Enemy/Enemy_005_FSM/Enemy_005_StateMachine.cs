using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_005_StateMachine : EnemyStateMachine
{
    protected override void OnEnable()
    {
        SwitchOn(stateTable[typeof(Enemy_005_Appear)]);
    }
}
