using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S003_ChangeSpeed_Sender : SenderBehaviour
{
    protected override void OnEnable()
    {
        base.OnEnable();
        currentAngularVelocity = 0;
    }
}
