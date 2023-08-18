using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX_Slider : StateBar
{
    private void Start()
    {
        stateBar = GetComponent<Slider>();
    }

    public void SetAudioValue()
    {
        EventCenter.Instance.EventTrigger<float>("SetSFXVolumeValue", stateBar.value);
    }
}
