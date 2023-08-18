using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullets_B02 : BulletBehaviour
{
    float time;

    [Header("余弦变化速率")]
    public float cosMultiply = 1000;
    protected override void OnEnable()
    {
        base.OnEnable();
        time = 0;
    }
    protected override void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        //更新当前线速度与角速度
        lineVelocity = Mathf.Clamp(lineVelocity + acceleration * Time.fixedDeltaTime, -maxVelocity, maxVelocity);
        angularVelocity += angularAcceleration * Time.fixedDeltaTime;

        float angle = Mathf.Cos((time * 500 * configValue_1) * Mathf.Deg2Rad) * angularVelocity * Mathf.Rad2Deg;

        //更新子弹位置
        transform.Translate(lineVelocity * Vector2.right * Time.fixedDeltaTime);
        transform.rotation *= Quaternion.Euler(Vector3.forward * angle * Time.fixedDeltaTime);
    }
}
