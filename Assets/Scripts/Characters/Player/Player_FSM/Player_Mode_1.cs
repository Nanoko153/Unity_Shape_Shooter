using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/PlayerState/Mode_1",fileName = "Player_Mode_1")]
public class Player_Mode_1 : PlayerState
{
    float shootCD_1;
    float shootCD_3;

    public override void Enter()
    {
        anim.Play("TypeA");

        shootCD_1 = 0;

        player.characterMode = ObjectType.Type_A;
        EventCenter.Instance.EventTrigger<string>("SwitchTypeUI", "¦Á");
        AudioManager.Instance.PlaySFX(player.SwitchTypeSFX);

        Debug.Log("Now Player State is: Mode_1");
    }

    public override void LogicUpdate()
    {

        if(input.IsKeyDown_RightMouse)
            stateMachine.SwitchState(typeof(Player_Mode_2));

        // if(player.isDeath)
        // {
        //     player.isDeath = false;
        // }
    }

    public override void PhysicUpdate()
    {
        player.UpdatePlaneDir();

        player.Move(player.moveSpeed);

        switch(player.level)
        {
            case 1:
                shootCD_3 += Time.fixedDeltaTime;
                if(shootCD_3 >= player.shootCD_3/2 && player.isCanShooting)
                {
                    shootCD_3 = 0;
                    PoolManager.Release(player.bulletPrefab_3, player.shootPos_1.position, player.shootPos_1.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_1);
                }
            break;
            case 2:
                shootCD_1 += Time.fixedDeltaTime;
                shootCD_3 += Time.fixedDeltaTime;
                if(shootCD_1 >= player.shootCD_1 && player.isCanShooting)
                {
                    shootCD_1 = 0;
                    PoolManager.Release(player.bulletPrefab_1, player.shootPos_1.position, player.shootPos_1.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_1);
                }
                if(shootCD_3 >= player.shootCD_3 && player.isCanShooting)
                {
                    shootCD_3 = 0;
                    PoolManager.Release(player.bulletPrefab_3, player.shootPos_1.position, player.shootPos_1.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_1);
                }
            break;
            case 3:
                shootCD_1 += Time.fixedDeltaTime;
                shootCD_3 += Time.fixedDeltaTime;
                if(shootCD_1 >= player.shootCD_1 && player.isCanShooting)
                {
                    shootCD_1 = 0;
                    PoolManager.Release(player.bulletPrefab_1, player.shootPos_2.position, player.shootPos_1.rotation);
                    PoolManager.Release(player.bulletPrefab_1, player.shootPos_3.position, player.shootPos_1.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_1);
                }
                if(shootCD_3 >= player.shootCD_3 && player.isCanShooting)
                {
                    shootCD_3 = 0;
                    PoolManager.Release(player.bulletPrefab_3, player.shootPos_1.position, player.shootPos_1.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_3);
                }
            break;
            case 4:
                shootCD_1 += Time.fixedDeltaTime;
                shootCD_3 += Time.fixedDeltaTime;
                if(shootCD_1 >= player.shootCD_1 && player.isCanShooting)
                {
                    shootCD_1 = 0;
                    PoolManager.Release(player.bulletPrefab_1, player.shootPos_2.position, player.shootPos_2.rotation);
                    PoolManager.Release(player.bulletPrefab_1, player.shootPos_3.position, player.shootPos_3.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_1);
                }
                if(shootCD_3 >= player.shootCD_3 && player.isCanShooting)
                {
                    shootCD_3 = 0;
                    PoolManager.Release(player.bulletPrefab_3, player.shootPos_2.position, player.shootPos_1.rotation);
                    PoolManager.Release(player.bulletPrefab_3, player.shootPos_3.position, player.shootPos_1.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_3);
                }
            break;
            case 5:
                shootCD_1 += Time.fixedDeltaTime;
                shootCD_3 += Time.fixedDeltaTime;
                if(shootCD_1 >= player.shootCD_1 && player.isCanShooting)
                {
                    shootCD_1 = 0;
                    PoolManager.Release(player.bulletPrefab_1, player.shootPos_1.position, player.shootPos_1.rotation);
                    PoolManager.Release(player.bulletPrefab_1, player.shootPos_2.position, player.shootPos_2.rotation);
                    PoolManager.Release(player.bulletPrefab_1, player.shootPos_3.position, player.shootPos_3.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_1);
                }
                if(shootCD_3 >= player.shootCD_3 && player.isCanShooting)
                {
                    shootCD_3 = 0;
                    PoolManager.Release(player.bulletPrefab_3, player.shootPos_2.position, player.shootPos_2.rotation);
                    PoolManager.Release(player.bulletPrefab_3, player.shootPos_3.position, player.shootPos_3.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_3);
                }
            break;
        }
    }
}
