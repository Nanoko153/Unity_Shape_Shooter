using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Sender",fileName = "new_Sender")]
public class SenderData : ScriptableObject
{
    [Header("��������ʼ����")]
    //��ʼ��ת��
    public float InitRotation = 0;
    //���������ٶ�
    public float SenderAngularVelocity = 0.1f;
    //�������Ǽ��ٶ�
    public float SenderAngularAcceleration = 0;
    //�����������ٶ�
    public float SenderMaxAngularVelocity = int.MaxValue;
    //�ӵ�������
    public int LineCount = 0;
    //�ӵ���֮��н�
    public float LineAngle = 30;
    //������
    public float SendInterval = 0.1f;
}
