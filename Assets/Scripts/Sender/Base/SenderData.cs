using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Sender",fileName = "new_Sender")]
public class SenderData : ScriptableObject
{
    [Header("发射器初始配置")]
    //初始旋转角
    public float InitRotation = 0;
    //发射器角速度
    public float SenderAngularVelocity = 0.1f;
    //发射器角加速度
    public float SenderAngularAcceleration = 0;
    //发射器最大角速度
    public float SenderMaxAngularVelocity = int.MaxValue;
    //子弹线数量
    public int LineCount = 0;
    //子弹线之间夹角
    public float LineAngle = 30;
    //发射间隔
    public float SendInterval = 0.1f;
}
