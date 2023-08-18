using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : Character
{
    [Header("����")]
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lookAtDir;
    public Transform planeTransform;

    [Header("�ƶ�����")]
    public float moveSpeed = 5f;

    [Header("����λ��")]
    public Transform[] shootPos;

    [Header("�ӵ�")]
    public GameObject[] bulletPrefab;

    [Header("Sander")]
    public GameObject[] sander;

    [Header("Laser")]
    public GameObject[] Laser;

    [HideInInspector]
    public bool isCanShooting;

    [Header("׷������")]
    public GameObject target;
    public float angularVelocity = 5;

    [Header("SFX")]
    public AudioClip[] shootSFX;

    [Header("VFX")]
    public GameObject deathVFX;
    public GameObject deathVFX_X;

    [HideInInspector]
    public int tempNum;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyManager.Instance.RegisterEnemy(this);
        if(GameManager.Instance.player  != null)
            target = GameManager.Instance.player.gameObject;

        coll.enabled = true;

        tempNum = 0;
    }

    private void OnDisable()
    {
        EnemyManager.Instance.RemoveEnemy(this);
    }

    public override void Die()
    {
        base.Die();
        coll.enabled = false;
    }

    #region ʹ����Ч
    public void PlayDeathVFX()
    {
        PoolManager.Release(deathVFX, transform.position);
        PoolManager.Release(deathVFX_X, transform.position);
    }
    #endregion


    #region Move����
    public Vector2 GetRandomMoveDir()
    {
        return ViewportManager.Instance.RandomAllPosition(0, 0) - transform.position;
    }

    public void MoveTo(Vector2 to, float time , bool snapping = true)
    {
        rb.DOMove(to, time, snapping);
    }
    #endregion

    #region LookAt����
    public void LookAtTarget()
    {
        //���Ŀ�귽��
        lookAtDir = (target.transform.position - transform.position).normalized;
        //���Ŀ�����Լ���x������ĽǶ�(0~180)
        float targetAngle = Mathf.Acos(Vector2.Dot(Vector2.right, lookAtDir)) * Mathf.Rad2Deg;
        //����y��λ��ϸ��Ϊ(0~360)
        if(lookAtDir.y < 0)
            targetAngle = -targetAngle;

        planeTransform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }

    public void LookAtTarget(Vector3 direction)
    {
        //���Ŀ�귽��
        lookAtDir = direction;
        //���Ŀ�����Լ���x������ĽǶ�(0~180)
        float targetAngle = Mathf.Acos(Vector2.Dot(Vector2.right, lookAtDir)) * Mathf.Rad2Deg;
        //����y��λ��ϸ��Ϊ(0~360)
        if(lookAtDir.y < 0)
            targetAngle = -targetAngle;

        planeTransform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }

    public void LookAtTarget(bool isImmediately)
    {
        //���Ŀ�귽��
        lookAtDir = (target.transform.position - transform.position).normalized;


        if (isImmediately)
        {
            //���Ŀ�����Լ���x������ĽǶ�(0~180)
            float targetAngle = Mathf.Acos(Vector2.Dot(Vector2.right, lookAtDir)) * Mathf.Rad2Deg;
            //����y��λ��ϸ��Ϊ(-180~180)
            if(lookAtDir.y < 0)
                targetAngle = -targetAngle;
            planeTransform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
        else
        {
            //���Ŀ�����Լ���x������ĽǶ�(0~180)
            float targetAngle = Mathf.Acos(Vector2.Dot(Vector2.right, lookAtDir)) * Mathf.Rad2Deg;
            //����y��λ��ϸ��Ϊ(-180~180)
            if(lookAtDir.y < 0)
                targetAngle = -targetAngle;
            planeTransform.DORotate(new Vector3(0, 0, targetAngle), 1f, RotateMode.Fast);
        }
    }

    public void LookAtTarget(bool isImmediately, float time)
    {
        //���Ŀ�귽��
        lookAtDir = (target.transform.position - transform.position).normalized;


        if (isImmediately)
        {
            //���Ŀ�����Լ���x������ĽǶ�(0~180)
            float targetAngle = Mathf.Acos(Vector2.Dot(Vector2.right, lookAtDir)) * Mathf.Rad2Deg;
            //����y��λ��ϸ��Ϊ(-180~180)
            if(lookAtDir.y < 0)
                targetAngle = -targetAngle;
            planeTransform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
        else
        {
            //���Ŀ�����Լ���x������ĽǶ�(0~180)
            float targetAngle = Mathf.Acos(Vector2.Dot(Vector2.right, lookAtDir)) * Mathf.Rad2Deg;
            //����y��λ��ϸ��Ϊ(-180~180)
            if(lookAtDir.y < 0)
                targetAngle = -targetAngle;
            planeTransform.DORotate(new Vector3(0, 0, targetAngle), time, RotateMode.Fast);
        }
    }
    #endregion

}

public enum EnemyStates
{
    Appear,
    Move,
    Attack,
    Disappear
}
