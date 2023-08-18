using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_001/Move",fileName = "Enemy_001_Move")]
public class Enemy_001_Move : EnemyState
{
    Vector3 movePos;
    Vector2 moveDir;

    public float shootCD = 1f;
    float shootTime;

    float time;

    public override void Enter()
    {
        //anim.Play("player_move");

        shootTime = 0;

        time = 0;

        movePos = ViewportManager.Instance.RandomAllPosition(0, 0);
        moveDir = (movePos - enemy.transform.position).normalized;
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

        shootTime += Time.fixedDeltaTime;
        if(shootTime >= shootCD)
        {
            AudioManager.Instance.PlaySFX_RandomPitch(enemy.shootSFX[0]);
            PoolManager.Release(enemy.bulletPrefab[0], enemy.shootPos[0].position, enemy.shootPos[0].rotation);
            shootTime = 0;
        }
        switch(enemy.level)
        {
            case 1:
            shootTime += Time.fixedDeltaTime;
            if(shootTime >= shootCD)
            {
                AudioManager.Instance.PlaySFX_RandomPitch(enemy.shootSFX[0]);
                PoolManager.Release(enemy.bulletPrefab[0], enemy.shootPos[0].position, enemy.shootPos[0].rotation);
                shootTime = 0;
            }
            break;
            case 2:
            shootTime += Time.fixedDeltaTime;
            if(shootTime >= shootCD/2)
            {
                AudioManager.Instance.PlaySFX_RandomPitch(enemy.shootSFX[0]);
                PoolManager.Release(enemy.bulletPrefab[0], enemy.shootPos[0].position, enemy.shootPos[0].rotation);
                shootTime = 0;
            }
            break;
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
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }
}
