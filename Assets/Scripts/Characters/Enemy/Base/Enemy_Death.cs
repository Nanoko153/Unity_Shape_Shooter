using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Enemy/Base/Death",fileName = "Death")]
public class Enemy_Death : EnemyState
{
    public override void Enter()
    {
        //触发特效
        enemy.PlayDeathVFX();

        //停止移动
        enemy.SetVelocity(Vector2.zero);

        //添加分数
        EventCenter.Instance.EventTrigger<int>("AddScore", enemy.score);

        enemy.gameObject.SetActive(false);  //敌人死亡取消显示并返回对象池
    }
}
