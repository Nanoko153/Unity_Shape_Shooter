using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_003/Disappear",fileName = "Enemy_003_Disappear")]
public class Enemy_003_Disappear : EnemyState
{
    Vector3 movePos;
    Vector2 moveDir;

    public float moveTime = 3f;

    float time;

    public override void Enter()
    {
        time = 0;

        movePos = ViewportManager.Instance.GetOutOfViewPosition();
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.LookAtTarget(moveDir);

    }

    public override void PhysicUpdate()
    {
        time += Time.fixedDeltaTime;
        if(time>=1)
        {
            //����Ŀ��

            time = 0;
        }

        //�ƶ�
        Vector2 temp = enemy.transform.position - movePos;
        if(temp.SqrMagnitude() < 0.1)
        {
            enemy.gameObject.SetActive(false);
        }

        //�����ƶ�����
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.transform.Translate(enemy.moveSpeed * moveDir * Time.fixedDeltaTime);

        if(enemy.isDeath)
        {
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }
}
