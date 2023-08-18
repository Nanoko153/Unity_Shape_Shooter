using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : StateBar
{
    void Awake()
    {
        stateBar = GetComponent<Slider>();
    }
    private void Start()
    {
        stateBar.value = AudioManager.Instance.sfx.volume;
    }

    public void SetAudioValue()
    {
        EventCenter.Instance.EventTrigger<float>("SetSFXVolumeValue", stateBar.value);
    }
}
