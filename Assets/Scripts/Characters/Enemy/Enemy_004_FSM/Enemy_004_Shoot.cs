using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_004/Shoot",fileName = "Enemy_004_Shoot")]
public class Enemy_004_Shoot : EnemyState
{
    public float shootCD = 0.1f;

    float shootTime;

    float time;

    public int bulletNum = 3;
    int bulletCurrentNum;

    public override void Enter()
    {
        time = 0;
        shootTime = 0;
        bulletCurrentNum = 0;

        enemy.SetVelocity(Vector2.zero);

        enemy.LookAtTarget(false, 2f);
    }

    public override void PhysicUpdate()
    {
        time += Time.fixedDeltaTime;
        if(time >= 2.3f)
        {
            shootTime += Time.fixedDeltaTime;
            if(shootTime >= shootCD)
            {
                AudioManager.Instance.PlaySFX_RandomPitch(enemy.shootSFX[0]);
                PoolManager.Release(enemy.bulletPrefab[0], enemy.shootPos[0].position, enemy.planeTransform.rotation);
                shootTime = 0;
                bulletCurrentNum++;
            }

            if(bulletCurrentNum >= bulletNum)
            {
                bulletCurrentNum = 0;
                stateMachine.SwitchState(typeof(Enemy_004_Move));
            }
        }

        if(enemy.isDeath)
        {
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }
}
