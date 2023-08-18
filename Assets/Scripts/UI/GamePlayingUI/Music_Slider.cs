using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Music_Slider : StateBar
{
    void Awake()
    {
        stateBar = GetComponent<Slider>();
    }
    private void Start()
    {
        stateBar.value = AudioManager.Instance.music.volume;
    }

    public void SetAudioValue()
    {
        EventCenter.Instance.EventTrigger<float>("SetMusicVolumeValue", stateBar.value);
    }
}
