using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S002_Circle_Sender : SenderBehaviour
{
    protected override void OnEnable()
    {
        base.OnEnable();
        currentAngle = 0;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(currentAngularVelocity == senderMaxAngularVelocity)
        {
            senderAngularAcceleration *= -1;
        }
    }
}
