using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullets_B02 : BulletBehaviour
{
    float time;

    [Header("���ұ仯����")]
    public float cosMultiply = 1000;
    protected override void OnEnable()
    {
        base.OnEnable();
        time = 0;
    }
    protected override void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        //���µ�ǰ���ٶ�����ٶ�
        lineVelocity = Mathf.Clamp(lineVelocity + acceleration * Time.fixedDeltaTime, -maxVelocity, maxVelocity);
        angularVelocity += angularAcceleration * Time.fixedDeltaTime;

        float angle = Mathf.Cos((time * 500 * configValue_1) * Mathf.Deg2Rad) * angularVelocity * Mathf.Rad2Deg;

        //�����ӵ�λ��
        transform.Translate(lineVelocity * Vector2.right * Time.fixedDeltaTime);
        transform.rotation *= Quaternion.Euler(Vector3.forward * angle * Time.fixedDeltaTime);
    }
}
