using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;

    [Header("发射器初始配置")]
    //初始旋转角
    public float initRotation = 0;
    //发射器角速度
    public float currentAngularVelocity = 0;
    //发射器角加速度
    public float senderAngularAcceleration = 0;
    //发射器最大角速度
    public float senderMaxAngularVelocity = int.MaxValue;
    //子弹线数量
    public int lineCount = 0;
    //子弹线之间夹角
    public float lineAngle = 30;
    //发射间隔
    public float sendInterval = 0.1f;


    protected float currentAngle = 0;

    protected float currentTime = 0;

    protected virtual void OnEnable()
    {
        if(TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            currentAngle = enemy.planeTransform.rotation.eulerAngles.z;
        }
    }

    protected virtual void FixedUpdate()
    {
        currentAngularVelocity = Mathf.Clamp
            (currentAngularVelocity + senderAngularAcceleration * Time.fixedDeltaTime, -senderMaxAngularVelocity, senderMaxAngularVelocity);
        //更新角度
        currentAngle += currentAngularVelocity * Time.fixedDeltaTime;
        //限制角度
        if(Mathf.Abs(currentAngle) > 720)
            currentAngle -= Mathf.Sign(currentAngle) * 360;

        //更新时间
        currentTime += Time.fixedDeltaTime;
        if(currentTime > sendInterval)
        {
            currentTime -= sendInterval;
            SendByCount(lineCount, currentAngle);
        }
    }

    public void SendByCount(int count, float angle)
    {
        float temp = count % 2 == 0 ? angle + lineAngle / 2 : angle;

        //历遍每一条线
        for(int i = 0; i < count; ++i)
        {
            temp += Mathf.Pow(-1, i) * i * lineAngle;
            Send(temp);
        }
    }

    public void Send(float angle)
    {
        //生成子弹
        PoolManager.Release(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
    }
}
