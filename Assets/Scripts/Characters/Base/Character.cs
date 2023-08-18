using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("基础生命信息")]
    public float maxHP;         //声明最大生命值
    public float health;                  //声明当前生命值
    [Header("角色模式")]
    public ObjectType characterMode = ObjectType.Type_A;
    protected Rigidbody2D rb;
    protected Collider2D coll;

    [Header("角色积分")]
    public int score = 10;

    [Header("角色等级")]
    [Range(1,5)]
    public int level = 1;

    //[Header("基础状态")]
    [HideInInspector]
    public bool isDeath;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    protected virtual void OnEnable()
    {
        health = maxHP;     //启用时就获得最大生命值
        isDeath = false;
    }

    #region 刚体速度设置
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

    #region 生命值相关
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
        health = 0f;                                        //血量数值设为0，保证不会被负数显示
        //播放爆炸死亡音效
        //调用对象池中的死亡特效
        //更改基础状态
        isDeath = true;
        Debug.Log("death");
    }

    /// <summary>
    /// 【公开】【可重写】恢复生命值
    /// </summary>
    /// <param name="value"></param>
    public virtual void RestoreHealth(float value)
    {
        health = Mathf.Clamp(health+value, 0f, maxHP);
    }

    /// <summary>
    /// 【子类继承】持续回血
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
    /// 【子类继承】持续受伤
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