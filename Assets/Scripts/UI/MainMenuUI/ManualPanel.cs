using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualPanel : MonoBehaviour
{
    public Canvas canvas;

    public AudioClip buttonSFX;

    void Start()
    {
        canvas.enabled = false;
    }

    public void PlayButtonSFX()
    {
        AudioManager.Instance.PlayUI(buttonSFX);
    }

    public void OpenManual()
    {
        PlayButtonSFX();
        canvas.enabled = true;
    }

    public void CloseManual()
    {
        PlayButtonSFX();
        canvas.enabled = false;
    }
}
