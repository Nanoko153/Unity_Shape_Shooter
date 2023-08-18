using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_001/Appear",fileName = "Enemy_001_Appear")]
public class Enemy_001_Appear : EnemyState
{
    Vector3 movePos;
    Vector2 moveDir;
    float whitTime = 1f;
    float t;
    bool isCanMove;

    public override void Enter()
    {
        //anim.Play("enemy_001_move");

        enemy.SetVelocity(Vector2.zero);
        isCanMove = false;

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
            //�ƶ�
            enemy.transform.Translate(10 * moveDir * Time.fixedDeltaTime);

            //����ƶ��Ƿ񵽴�Ŀ�ĵ�
            Vector2 temp = enemy.transform.position - movePos;
            if(temp.SqrMagnitude() < 0.1)
            {
                stateMachine.SwitchState(typeof(Enemy_001_Move));
            }
        }
    }
}
