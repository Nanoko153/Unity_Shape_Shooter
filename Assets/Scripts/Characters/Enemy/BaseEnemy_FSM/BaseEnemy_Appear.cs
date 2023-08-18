using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Enemy/BaseEnemy/Appear",fileName = "BaseEnemy_Appear")]
public class BaseEnemy_Appear : EnemyState
{
    Vector3 movePos;
    Vector2 moveDir;
    float whitTime = 1f;
    float t;
    bool isCanMove;

    public override void Enter()
    {
        anim.Play("BaseEnemy_Move");

        enemy.SetVelocity(Vector2.zero);
        isCanMove = false;

        Debug.Log("Now BaseEnemy State is: Appear");

        t = 0;
    }

    public override void LogicUpdate()
    {

    }

    public override void PhysicUpdate()
    {
        t += Time.fixedDeltaTime;
        if(t >= whitTime && isCanMove == false)
        {
            movePos = ViewportManager.Instance.RandomAllPosition(0,0);
            moveDir = (movePos - enemy.transform.position).normalized;
            enemy.LookAtTarget(moveDir);
            isCanMove = true;
        }

        if(isCanMove)
        {
            //移动
            enemy.transform.Translate(10 * moveDir * Time.fixedDeltaTime);

            //检查移动是否到达目的地
            Vector2 temp = enemy.transform.position - movePos;
            if(temp.SqrMagnitude() < 0.1)
            {
                stateMachine.SwitchState(typeof(BaseEnemy_Move));
            }
        }
    }
}
