using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_003/Move",fileName = "Enemy_003_Move")]
public class Enemy_003_Move : EnemyState
{
    Vector3 movePos;
    Vector2 moveDir;

    public float moveTime = 3f;

    // float time;

    public override void Enter()
    {
        // time = 0;

        movePos = ViewportManager.Instance.ninePositionInView[Random.Range(0,9)];
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.LookAtTarget(moveDir);
    }

    public override void PhysicUpdate()
    {
        // time += Time.fixedDeltaTime;
        // if(time>=1)
        // {
        //     //看向目标

        //     time = 0;
        // }

        //移动
        Vector2 temp = enemy.transform.position - movePos;
        if(temp.SqrMagnitude() < 0.01)
        {
            stateMachine.SwitchState(typeof(Enemy_003_Shoot));
        }

        //更新移动方向
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.transform.Translate(3 * moveDir * Time.fixedDeltaTime);

        if(enemy.isDeath)
        {
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }
}
