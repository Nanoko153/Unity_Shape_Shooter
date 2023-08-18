using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullets_N02_Follow : BulletBehaviour
{
    float time;
    public float minBallisticAngle;
    public float maxBallisticAngle;
    float ballisticAngle;

    Vector3 targetDirection;

    GameObject target;

    protected override void OnEnable()
    {
        base.OnEnable();
        time = 0;
        StartCoroutine(HomingCoroutine(GetTarget()));
    }

    public GameObject GetTarget()
    {
        if(EnemyManager.Instance.NumberOfEnemiesRemaining != 0)
        {
            //return target = EnemyManager.Instance.enemyList[0].gameObject;
            foreach(EnemyController enemy in EnemyManager.Instance.enemyList)
            {
                if(bulletMode != enemy.characterMode)
                {
                    return enemy.gameObject;
                }
            }
        }
        return null;
    }
    public IEnumerator HomingCoroutine(GameObject target)
    {
        ballisticAngle = Random.Range(minBallisticAngle, maxBallisticAngle);
        while (gameObject.activeSelf && target != null)
        {
            if(target.activeSelf)
            {
                //追踪目标
                //方向
                targetDirection = target.transform.position - transform.position;

                //旋转
                transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg, Vector3.forward);
                transform.rotation *= Quaternion.Euler(0f, 0f, ballisticAngle);
            }
            yield return null;
        }
    }
}
