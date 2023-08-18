using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullets_B05 : BulletBehaviour
{
    bool changSpeed;
    float time;
    protected override void OnEnable()
    {
        base.OnEnable();
        changSpeed = false;
        time = 0;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(!changSpeed && lineVelocity <= 0)
        {
            changSpeed = true;
            acceleration = 0;
            lineVelocity = 0;
        }
        if(changSpeed)
        {
            time += Time.fixedDeltaTime;
            if(time >= configValue_2)
            {
                acceleration = configValue_1;
            }
        }
    }
}
