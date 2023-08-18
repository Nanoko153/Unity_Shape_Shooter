using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/PlayerState/Mode_2",fileName = "Player_Mode_2")]
public class Player_Mode_2 : PlayerState
{
    float shootCD_2;
    float shootCD_4;

    public override void Enter()
    {
        anim.Play("TypeB");

        shootCD_2 = 0;
        shootCD_4 = 0;

        player.characterMode = ObjectType.Type_B;
        EventCenter.Instance.EventTrigger<string>("SwitchTypeUI", "¦Â");
        AudioManager.Instance.PlaySFX(player.SwitchTypeSFX);

        Debug.Log("Now Player State is: Mode_2");

    }

    public override void LogicUpdate()
    {

        if(input.IsKeyDown_RightMouse)
            stateMachine.SwitchState(typeof(Player_Mode_1));

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
                shootCD_4 += Time.fixedDeltaTime;
                if(shootCD_4 >= player.shootCD_4/2 && player.isCanShooting)
                {
                    shootCD_4 = 0;
                    PoolManager.Release(player.bulletPrefab_4, player.shootPos_1.position, player.shootPos_1.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_2);
                }
            break;
            case 2:
                shootCD_2 += Time.fixedDeltaTime;
                shootCD_4 += Time.fixedDeltaTime;
                if(shootCD_2 >= player.shootCD_2 && player.isCanShooting)
                {
                    shootCD_2 = 0;
                    PoolManager.Release(player.bulletPrefab_2, player.shootPos_1.position, player.shootPos_1.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_2);
                }
                if(shootCD_4 >= player.shootCD_4 && player.isCanShooting)
                {
                    shootCD_4 = 0;
                    PoolManager.Release(player.bulletPrefab_4, player.shootPos_1.position, player.shootPos_1.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_2);
                }
            break;
            case 3:
                shootCD_2 += Time.fixedDeltaTime;
                shootCD_4 += Time.fixedDeltaTime;
                if(shootCD_2 >= player.shootCD_2 && player.isCanShooting)
                {
                    shootCD_2 = 0;
                    PoolManager.Release(player.bulletPrefab_2, player.shootPos_2.position, player.shootPos_1.rotation);
                    PoolManager.Release(player.bulletPrefab_2, player.shootPos_3.position, player.shootPos_1.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_2);
                }
                if(shootCD_4 >= player.shootCD_4 && player.isCanShooting)
                {
                    shootCD_4 = 0;
                    PoolManager.Release(player.bulletPrefab_4, player.shootPos_1.position, player.shootPos_1.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_4);
                }
            break;
            case 4:
                shootCD_2 += Time.fixedDeltaTime;
                shootCD_4 += Time.fixedDeltaTime;
                if(shootCD_2 >= player.shootCD_2 && player.isCanShooting)
                {
                    shootCD_2 = 0;
                    PoolManager.Release(player.bulletPrefab_2, player.shootPos_2.position, player.shootPos_2.rotation);
                    PoolManager.Release(player.bulletPrefab_2, player.shootPos_3.position, player.shootPos_3.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_2);
                }
                if(shootCD_4 >= player.shootCD_4 && player.isCanShooting)
                {
                    shootCD_4 = 0;
                    PoolManager.Release(player.bulletPrefab_4, player.shootPos_2.position, player.shootPos_1.rotation);
                    PoolManager.Release(player.bulletPrefab_4, player.shootPos_3.position, player.shootPos_1.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_4);
                }
            break;
            case 5:
                shootCD_2 += Time.fixedDeltaTime;
                shootCD_4 += Time.fixedDeltaTime;
                if(shootCD_2 >= player.shootCD_2 && player.isCanShooting)
                {
                    shootCD_2 = 0;
                    PoolManager.Release(player.bulletPrefab_2, player.shootPos_1.position, player.shootPos_1.rotation);
                    PoolManager.Release(player.bulletPrefab_2, player.shootPos_2.position, player.shootPos_2.rotation);
                    PoolManager.Release(player.bulletPrefab_2, player.shootPos_3.position, player.shootPos_3.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_2);
                }
                if(shootCD_4 >= player.shootCD_4 && player.isCanShooting)
                {
                    shootCD_4 = 0;
                    PoolManager.Release(player.bulletPrefab_4, player.shootPos_2.position, player.shootPos_2.rotation);
                    PoolManager.Release(player.bulletPrefab_4, player.shootPos_3.position, player.shootPos_3.rotation);
                    AudioManager.Instance.PlaySFX_RandomPitch(player.bulletSFX_4);
                }
            break;
        }
    }
}
