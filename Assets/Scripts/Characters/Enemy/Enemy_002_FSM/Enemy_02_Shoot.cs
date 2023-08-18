using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_002/Shoot",fileName = "Enemy_002_Shoot")]
public class Enemy_02_Shoot : EnemyState
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

        enemy.LookAtTarget(false, 0.5f);
    }

    public override void PhysicUpdate()
    {
        time += Time.fixedDeltaTime;
        if(time >= 0.7)
        {
            shootTime += Time.fixedDeltaTime;
            if(shootTime >= shootCD)
            {
                AudioManager.Instance.PlaySFX_RandomPitch(enemy.shootSFX[0]);
                PoolManager.Release(enemy.bulletPrefab[0], enemy.transform.position, enemy.planeTransform.rotation);
                shootTime = 0;
                bulletCurrentNum++;
            }

            if(bulletCurrentNum >= bulletNum)
            {
                bulletCurrentNum = 0;
                stateMachine.SwitchState(typeof(Enemy_002_Move));
            }
        }

        if(enemy.isDeath)
        {
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }

}
