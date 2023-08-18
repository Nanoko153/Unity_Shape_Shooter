using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_005/Move",fileName = "Enemy_005_Move")]
public class Enemy_005_Move : EnemyState
{
    Vector3 movePos;
    Vector2 moveDir;

    public override void Enter()
    {
        movePos = ViewportManager.Instance.ninePositionInView[Random.Range(0,9)];
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.LookAtTarget(moveDir);

    }

    public override void PhysicUpdate()
    {

        //移动
        Vector2 temp = enemy.transform.position - movePos;
        if(temp.SqrMagnitude() < 0.01)
        {
            stateMachine.SwitchState(typeof(Enemy_005_Shoot));
        }

        //更新移动方向
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.transform.Translate(10 * moveDir * Time.fixedDeltaTime);

        if(enemy.isDeath)
        {
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }
}
