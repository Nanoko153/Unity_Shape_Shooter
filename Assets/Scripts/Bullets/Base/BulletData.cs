using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Bullet",fileName = "new_Bullet")]
public class BulletData : ScriptableObject
{
    // [Header("�ӵ�����")]
    // public ObjectType BulletMode = ObjectType.Type_A;
    [Header("�˺�")]
    public int Damage = 10;

    [Header("���ٶ�����")]
    //���ٶ�
    public float LineVelocity = 0;
    //���ٶ�
    public float Acceleration = 0;

    [Header("���ٶ�����")]
    //���ٶ�
    public float AngularVelocity = 0;
    //�Ǽ��ٶ�
    public float AngularAcceleration = 0;

    [Header("����ٶ�")]
    //����ٶ�
    public float MaxVelocity = int.MaxValue;

    [Header("�������ò���")]
    public float ConfigValue_1 = 0;
    public float ConfigValue_2 = 0;
    public float ConfigValue_3 = 0;
}
