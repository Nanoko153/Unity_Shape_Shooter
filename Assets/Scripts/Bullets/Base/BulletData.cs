using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Bullet",fileName = "new_Bullet")]
public class BulletData : ScriptableObject
{
    // [Header("子弹类型")]
    // public ObjectType BulletMode = ObjectType.Type_A;
    [Header("伤害")]
    public int Damage = 10;

    [Header("线速度设置")]
    //线速度
    public float LineVelocity = 0;
    //加速度
    public float Acceleration = 0;

    [Header("角速度设置")]
    //角速度
    public float AngularVelocity = 0;
    //角加速度
    public float AngularAcceleration = 0;

    [Header("最大速度")]
    //最大速度
    public float MaxVelocity = int.MaxValue;

    [Header("扩充配置参数")]
    public float ConfigValue_1 = 0;
    public float ConfigValue_2 = 0;
    public float ConfigValue_3 = 0;
}
