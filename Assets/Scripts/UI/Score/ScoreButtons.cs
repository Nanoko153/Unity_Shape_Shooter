using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreButtons : MonoBehaviour
{
    public AudioClip returnMainMenuButtonAudio;
    public void ReturnMainMenu()
    {
        AudioManager.Instance.PlayUI(returnMainMenuButtonAudio);
        EventCenter.Instance.EventTrigger("ReturnMainMenu");
    }
}
