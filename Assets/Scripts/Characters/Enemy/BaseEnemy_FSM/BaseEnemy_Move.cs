using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Enemy/BaseEnemy/Move",fileName = "BaseEnemy_Move")]
public class BaseEnemy_Move : EnemyState
{
    Vector3 movePos;
    Vector2 moveDir;
    // float shootCD;

    public override void Enter()
    {
        //anim.Play("player_move");

        // shootCD = 0;

        movePos = ViewportManager.Instance.RandomAllPosition(0, 0);
        moveDir = (movePos - enemy.transform.position).normalized;

        Debug.Log("Now BaseEnemy State is: Moving");
    }

    public override void LogicUpdate()
    {

    }

    public override void PhysicUpdate()
    {
        //看向目标
        enemy.LookAtTarget(false);

        //移动
        Vector2 temp = enemy.transform.position - movePos;
        if(temp.SqrMagnitude() < 0.1)
        {
            movePos = ViewportManager.Instance.RandomAllPosition(0, 0);
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
