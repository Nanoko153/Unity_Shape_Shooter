using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("����������Ϣ")]
    public float maxHP;         //�����������ֵ
    public float health;                  //������ǰ����ֵ
    [Header("��ɫģʽ")]
    public ObjectType characterMode = ObjectType.Type_A;
    protected Rigidbody2D rb;
    protected Collider2D coll;

    [Header("��ɫ����")]
    public int score = 10;

    [Header("��ɫ�ȼ�")]
    [Range(1,5)]
    public int level = 1;

    //[Header("����״̬")]
    [HideInInspector]
    public bool isDeath;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    protected virtual void OnEnable()
    {
        health = maxHP;     //����ʱ�ͻ���������ֵ
        isDeath = false;
    }

    #region �����ٶ�����
    public void SetVelocity (Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    public void SetVelocityX (float velocityX) {
        rb.velocity = new Vector2(velocityX ,rb.velocity.y);
    }

    public void SetVelocityY (float velocityY) {
        rb.velocity = new Vector2(rb.velocity.x ,velocityY);
    }
    #endregion

    #region ����ֵ���
    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health<=0f)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        health = 0f;                                        //Ѫ����ֵ��Ϊ0����֤���ᱻ������ʾ
        //���ű�ը������Ч
        //���ö�����е�������Ч
        //���Ļ���״̬
        isDeath = true;
        Debug.Log("death");
    }

    /// <summary>
    /// ��������������д���ָ�����ֵ
    /// </summary>
    /// <param name="value"></param>
    public virtual void RestoreHealth(float value)
    {
        health = Mathf.Clamp(health+value, 0f, maxHP);
    }

    /// <summary>
    /// ������̳С�������Ѫ
    /// </summary>
    /// <param name="waitTime"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    protected IEnumerator HealthRegenerateCorotine(WaitForSeconds waitTime,float percent)
    {
        while (health < maxHP)
        {
            yield return waitTime;

            RestoreHealth(maxHP * percent);
        }
    }

    /// <summary>
    /// ������̳С���������
    /// </summary>
    /// <param name="waitTime"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    protected IEnumerator DemageOverCorotine(WaitForSeconds waitTime, float percent)
    {
        while (health > 0f)
        {
            yield return waitTime;

            TakeDamage(maxHP * percent);
        }
    }
    #endregion
}

public enum ObjectType {Type_A, Type_B, Normal}