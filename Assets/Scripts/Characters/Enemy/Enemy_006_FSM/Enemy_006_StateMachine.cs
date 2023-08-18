using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_006_StateMachine : EnemyStateMachine
{
    protected override void OnEnable()
    {
        SwitchOn(stateTable[typeof(Enemy_006_Appear)]);
    }
}
