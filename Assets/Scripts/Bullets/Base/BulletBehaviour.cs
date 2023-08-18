using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public BulletData bulletData;

    public ObjectType bulletMode = ObjectType.Normal;
    [Header("伤害")]
    protected int damage = 0;

    [Header("线速度设置")]
    protected float lineVelocity = 0;               //线速度
    protected float acceleration = 0;               //加速度

    [Header("角速度设置")]
    protected float angularVelocity = 0;            //角速度
    protected float angularAcceleration = 0;        //角加速度

    [Header("最大速度")]
    protected float maxVelocity = int.MaxValue;     //最大速度

    [Header("扩充配置参数")]
    protected float configValue_1 = 0;
    protected float configValue_2 = 0;
    protected float configValue_3 = 0;
    // public LayerMask checkLayer;

    [Header("子弹击中音效")]
    public AudioClip hitSFX;

    [Header("子弹击中特效")]
    public GameObject hitVFX;

    protected virtual void OnEnable()
    {
        // bulletMode = bulletData.BulletMode;

        damage = bulletData.Damage;

        lineVelocity = bulletData.LineVelocity;
        acceleration = bulletData.Acceleration;
        angularVelocity = bulletData.AngularVelocity;
        angularAcceleration = bulletData.AngularAcceleration;
        maxVelocity = bulletData.MaxVelocity;
        configValue_1 = bulletData.ConfigValue_1;
        configValue_2 = bulletData.ConfigValue_2;
        configValue_3 = bulletData.ConfigValue_3;

    }
    protected virtual void FixedUpdate()
    {

        //更新当前线速度与角速度
        lineVelocity = Mathf.Clamp(lineVelocity + acceleration * Time.fixedDeltaTime, -maxVelocity, maxVelocity);
        angularVelocity += angularAcceleration * Time.fixedDeltaTime;

        //更新子弹位置
        transform.Translate(lineVelocity * Vector2.right * Time.fixedDeltaTime);
        transform.rotation *= Quaternion.Euler(Vector3.forward * angularVelocity * Time.fixedDeltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //如果碰撞到的层级带有角色脚本调用相应函数
        if(other.gameObject.TryGetComponent<Character>(out Character character))
        {
            if(bulletMode == ObjectType.Normal)
            {
                AudioManager.Instance.PlaySFX_RandomPitch(hitSFX);
                PoolManager.Release(hitVFX, transform.position);
                character.TakeDamage(damage);
                gameObject.SetActive(false);
                return;
            }

            if(bulletMode != character.characterMode)
            {
                AudioManager.Instance.PlaySFX_RandomPitch(hitSFX);
                PoolManager.Release(hitVFX, transform.position);
                character.TakeDamage(damage);
                gameObject.SetActive(false);
                return;
            }

        }
    }

}
