using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_003/Shoot",fileName = "Enemy_003_Shoot")]
public class Enemy_003_Shoot : EnemyState
{
    public float shootTimeOfDuration = 10f;

    float shootTime;

    float time;

    public int bulletNum = 3;
    // int bulletCurrentNum;

    public override void Enter()
    {
        time = 0;
        shootTime = 0;
        // bulletCurrentNum = 0;
        enemy.tempNum++;

        enemy.SetVelocity(Vector2.zero);

        enemy.LookAtTarget(false, 0.5f);

        enemy.sander[0].SetActive(true);

    }

    public override void PhysicUpdate()
    {
        shootTime += Time.fixedDeltaTime;
        if(shootTime >= shootTimeOfDuration)
        {
            stateMachine.SwitchState(typeof(Enemy_003_Move));
        }

        if(enemy.tempNum >= 6)
        {
            stateMachine.SwitchState(typeof(Enemy_003_Disappear));
        }

        if(enemy.isDeath)
        {
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }

    public override void Exit()
    {
        foreach(GameObject sender in enemy.sander)
        {
            sender.SetActive(false);
        }
    }
}
