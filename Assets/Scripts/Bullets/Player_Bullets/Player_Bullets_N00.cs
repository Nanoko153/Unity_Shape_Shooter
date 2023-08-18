using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullets_N00 : BulletBehaviour
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(Mathf.Abs(lineVelocity) >= maxVelocity)
            acceleration *= -1;
    }
}
