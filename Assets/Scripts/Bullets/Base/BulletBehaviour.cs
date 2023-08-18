using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public BulletData bulletData;

    public ObjectType bulletMode = ObjectType.Normal;
    [Header("�˺�")]
    protected int damage = 0;

    [Header("���ٶ�����")]
    protected float lineVelocity = 0;               //���ٶ�
    protected float acceleration = 0;               //���ٶ�

    [Header("���ٶ�����")]
    protected float angularVelocity = 0;            //���ٶ�
    protected float angularAcceleration = 0;        //�Ǽ��ٶ�

    [Header("����ٶ�")]
    protected float maxVelocity = int.MaxValue;     //����ٶ�

    [Header("�������ò���")]
    protected float configValue_1 = 0;
    protected float configValue_2 = 0;
    protected float configValue_3 = 0;
    // public LayerMask checkLayer;

    [Header("�ӵ�������Ч")]
    public AudioClip hitSFX;

    [Header("�ӵ�������Ч")]
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

        //���µ�ǰ���ٶ�����ٶ�
        lineVelocity = Mathf.Clamp(lineVelocity + acceleration * Time.fixedDeltaTime, -maxVelocity, maxVelocity);
        angularVelocity += angularAcceleration * Time.fixedDeltaTime;

        //�����ӵ�λ��
        transform.Translate(lineVelocity * Vector2.right * Time.fixedDeltaTime);
        transform.rotation *= Quaternion.Euler(Vector3.forward * angularVelocity * Time.fixedDeltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //�����ײ���Ĳ㼶���н�ɫ�ű�������Ӧ����
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
