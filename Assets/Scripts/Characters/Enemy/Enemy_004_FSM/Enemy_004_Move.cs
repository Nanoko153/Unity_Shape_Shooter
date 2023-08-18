using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_004/Move",fileName = "Enemy_004_Move")]
public class Enemy_004_Move : EnemyState
{
    Vector3 movePos;
    Vector2 moveDir;

    public float moveTime = 3f;


    public override void Enter()
    {
        movePos = ViewportManager.Instance.RandomAllPosition(0, 0);
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.LookAtTarget(moveDir);

    }

    public override void PhysicUpdate()
    {

        //移动
        Vector2 temp = enemy.transform.position - movePos;
        if(temp.SqrMagnitude() < 0.01)
        {
            stateMachine.SwitchState(typeof(Enemy_004_Shoot));
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
