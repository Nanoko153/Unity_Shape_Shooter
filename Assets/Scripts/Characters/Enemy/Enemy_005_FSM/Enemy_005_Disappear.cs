using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName ="FSM/Enemy/Enemy_005/Disappear",fileName = "Enemy_005_Disappear")]
public class Enemy_005_Disappear : EnemyState
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
        //�ƶ�
        Vector2 temp = enemy.transform.position - movePos;
        if(temp.SqrMagnitude() < 0.1)
        {
            enemy.gameObject.SetActive(false);
        }

        //�����ƶ�����
        moveDir = (movePos - enemy.transform.position).normalized;
        enemy.transform.Translate(15 * moveDir * Time.fixedDeltaTime);

        if(enemy.isDeath)
        {
            stateMachine.SwitchState(typeof(Enemy_Death));
        }
    }
}
