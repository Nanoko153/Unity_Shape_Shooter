using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public OptionsPanel optionsPanel;

    public AudioClip buttonSFX;

    public void PlayButtonSFX()
    {
        AudioManager.Instance.PlayUI(buttonSFX);
    }
    public void GamePlay()
    {
        PlayButtonSFX();
        EventCenter.Instance.EventTrigger("LoadGamePlayScene");
    }

    public void PlayerData()
    {
        PlayButtonSFX();
        EventCenter.Instance.EventTrigger("LoadPlayerDataScene");
    }

    public void Options()
    {
        PlayButtonSFX();
        if(!optionsPanel.isIn)
        {
            optionsPanel.InOptionsPanel();
            optionsPanel.isIn = true;
        }
        else
        {
            optionsPanel.OutOptionPanel();
            optionsPanel.isIn = false;
        }
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayUI(buttonSFX);

        Application.Quit();
        Debug.Log("ÓÎÏ·ÍË³ö");
    }
}
