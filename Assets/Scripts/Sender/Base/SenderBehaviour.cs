using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;

    [Header("��������ʼ����")]
    //��ʼ��ת��
    public float initRotation = 0;
    //���������ٶ�
    public float currentAngularVelocity = 0;
    //�������Ǽ��ٶ�
    public float senderAngularAcceleration = 0;
    //�����������ٶ�
    public float senderMaxAngularVelocity = int.MaxValue;
    //�ӵ�������
    public int lineCount = 0;
    //�ӵ���֮��н�
    public float lineAngle = 30;
    //������
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
        //���½Ƕ�
        currentAngle += currentAngularVelocity * Time.fixedDeltaTime;
        //���ƽǶ�
        if(Mathf.Abs(currentAngle) > 720)
            currentAngle -= Mathf.Sign(currentAngle) * 360;

        //����ʱ��
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

        //����ÿһ����
        for(int i = 0; i < count; ++i)
        {
            temp += Mathf.Pow(-1, i) * i * lineAngle;
            Send(temp);
        }
    }

    public void Send(float angle)
    {
        //�����ӵ�
        PoolManager.Release(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
    }
}
