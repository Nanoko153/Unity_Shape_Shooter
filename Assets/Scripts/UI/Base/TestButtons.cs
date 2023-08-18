using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtons : MonoBehaviour
{
    public void StartWave()
    {
        EventCenter.Instance.EventTrigger("StartWave");
        //EventCenter.Instance.EventTrigger("StartWave");
    }
}
