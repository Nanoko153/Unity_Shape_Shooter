using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy_StateMachine : EnemyStateMachine
{

    protected override void OnEnable()
    {
        SwitchOn(stateTable[typeof(BaseEnemy_Appear)]);
    }
}
