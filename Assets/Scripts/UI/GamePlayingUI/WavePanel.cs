using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavePanel : MonoBehaviour
{
    Animation anim;
    TMP_Text text;

    public AudioClip waveUpdateUISFX;

    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        anim = GetComponent<Animation>();
        EventCenter.Instance.AddEventListener("PlayWaveUpdateUI",PlayWaveUpdateUI);
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("PlayWaveUpdateUI",PlayWaveUpdateUI);
    }

    public void PlayWaveUpdateUI()
    {
        AudioManager.Instance.PlayUI(waveUpdateUISFX);
        text.text = "WAVE " + WaveManager.Instance.currentWaveNum;
        anim.Play();
    }

    public void StartWave()
    {
        EventCenter.Instance.EventTrigger("StartWave");
    }
}
