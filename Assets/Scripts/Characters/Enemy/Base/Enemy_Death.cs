using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Enemy/Base/Death",fileName = "Death")]
public class Enemy_Death : EnemyState
{
    public override void Enter()
    {
        //������Ч
        enemy.PlayDeathVFX();

        //ֹͣ�ƶ�
        enemy.SetVelocity(Vector2.zero);

        //��ӷ���
        EventCenter.Instance.EventTrigger<int>("AddScore", enemy.score);

        enemy.gameObject.SetActive(false);  //��������ȡ����ʾ�����ض����
    }
}
