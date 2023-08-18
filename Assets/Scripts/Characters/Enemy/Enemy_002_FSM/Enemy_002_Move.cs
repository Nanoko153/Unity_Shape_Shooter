using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_002/Move",fileName = "Enemy_002_Move")]
public class Enemy_002_Move : EnemyState
{
    Vector3 movePos;
    Vector2 moveDir;

    public float moveTime = 3f;

    // public float shootCD = 1f;
    float shootTime;

    float time;

    public override void Enter()
    {
        time = 0;

        movePos = ViewportManager.Instance.RandomAllPosition(0, 0);
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.LookAtTarget(moveDir);
    }

    public override void PhysicUpdate()
    {
        time += Time.fixedDeltaTime;
        if(time>=1)
        {
            //看向目标
            enemy.LookAtTarget(false, 0.5f);
            time = 0;
        }

        //移动
        Vector2 temp = enemy.transform.position - movePos;
        if(temp.SqrMagnitude() < 0.1)
        {
            stateMachine.SwitchState(typeof(Enemy_02_Shoot));
        }

        //更新移动方向
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.transform.Translate(enemy.moveSpeed * moveDir * Time.fixedDeltaTime);

        if(enemy.isDeath)
        {
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }
}
