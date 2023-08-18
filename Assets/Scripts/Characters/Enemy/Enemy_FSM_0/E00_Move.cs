using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Enemy/E00/Move",fileName = "E00_Move")]
public class E00_Move : EnemyState
{
    Vector3 movePos;
    Vector2 moveDir;
    public float moveSpeed;
    // float shootCD;

    public override void Enter()
    {
        //anim.Play("BaseEnemy_Move");

        movePos = ViewportManager.Instance.RandomAllPosition(0, 0);
        moveDir = (movePos - enemy.transform.position).normalized;

        Debug.Log("Now E00 State is: Moving");
    }

    public override void LogicUpdate()
    {

    }

    public override void PhysicUpdate()
    {
        //看向目标
        enemy.LookAtTarget();

        //移动
        Vector2 temp = enemy.transform.position - movePos;
        if(temp.SqrMagnitude() < 0.1)
        {
            movePos = ViewportManager.Instance.RandomAllPosition(0, 0);
        }

        //更新移动方向
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.transform.Translate(moveSpeed * moveDir * Time.fixedDeltaTime);

        if(enemy.isDeath)
        {
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }
}
