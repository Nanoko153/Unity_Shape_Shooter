using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_006/Disappear",fileName = "Enemy_006_Disappear")]
public class Enemy_006_Disappear : EnemyState
{
    Vector3 movePos;
    Vector2 moveDir;

    public float moveTime = 3f;


    public override void Enter()
    {

        movePos = ViewportManager.Instance.GetOutOfViewPosition();
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.LookAtTarget(moveDir);

    }

    public override void PhysicUpdate()
    {
        //移动
        Vector2 temp = enemy.transform.position - movePos;
        if(temp.SqrMagnitude() < 0.1)
        {
            enemy.gameObject.SetActive(false);
        }

        //更新移动方向
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.transform.Translate(5 * moveDir * Time.fixedDeltaTime);

        if(enemy.isDeath)
        {
            PoolManager.Release(enemy.bulletPrefab[0], enemy.transform.position);
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }
}
