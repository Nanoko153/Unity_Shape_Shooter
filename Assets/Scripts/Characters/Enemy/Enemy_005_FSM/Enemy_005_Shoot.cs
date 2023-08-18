using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_005/Shoot",fileName = "Enemy_005_Shoot")]
public class Enemy_005_Shoot : EnemyState
{
    public float shootCD = 0.1f;

    float shootTime;

    float time;

    public int bulletNum = 5;

    int bulletCurrentNum;

    public override void Enter()
    {
        time = 0;
        shootTime = 0;
        bulletCurrentNum = 0;

        enemy.tempNum++;

        enemy.SetVelocity(Vector2.zero);

        enemy.LookAtTarget(false, 0.5f);
    }

    public override void PhysicUpdate()
    {
        time += Time.fixedDeltaTime;
        if(time >= 0.6f)
        {
            shootTime += Time.fixedDeltaTime;
            if(shootTime >= shootCD)
            {
                AudioManager.Instance.PlaySFX_RandomPitch(enemy.shootSFX[0]);
                PoolManager.Release(enemy.bulletPrefab[0], enemy.shootPos[0].position, enemy.planeTransform.rotation);
                shootTime = 0;
                bulletCurrentNum++;
            }

            if(enemy.tempNum >= 3)
            {
                stateMachine.SwitchState(typeof(Enemy_005_Disappear));
            }

            if(bulletCurrentNum >= bulletNum)
            {
                bulletCurrentNum = 0;
                stateMachine.SwitchState(typeof(Enemy_005_Move));
            }
        }

        if(enemy.isDeath)
        {
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }
}
