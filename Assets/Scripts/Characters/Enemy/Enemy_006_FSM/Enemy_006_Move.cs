using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_006/Move",fileName = "Enemy_006_Move")]
public class Enemy_006_Move : EnemyState
{
    Vector3 movePos;
    Vector2 moveDir;

    public float shootCD = 1f;

    float time;

    public override void Enter()
    {
        time = 0;

        movePos = ViewportManager.Instance.RandomAllPosition(0, 0);
        moveDir = (movePos - enemy.transform.position).normalized;
    }

    public override void PhysicUpdate()
    {
        time += Time.fixedDeltaTime;
        if(time>=30f)
        {
            stateMachine.SwitchState(typeof(Enemy_006_Disappear));
            time = 0;
        }

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
            PoolManager.Release(enemy.bulletPrefab[0], enemy.transform.position);
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }
}
