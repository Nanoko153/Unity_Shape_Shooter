using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedPanel : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        EventCenter.Instance.AddEventListener("PlayInPausedUI", PlayInPausedPanel);
        EventCenter.Instance.AddEventListener("PlayOutPausedUI", PlayOutPausedPanel);
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("PlayInPausedUI", PlayInPausedPanel);
        EventCenter.Instance.RemoveEventListener("PlayOutPausedUI", PlayOutPausedPanel);
    }

    public void PlayInPausedPanel()
    {
        anim.Play("InPaused");
    }

    public void PlayOutPausedPanel()
    {
        anim.Play("OutPaused");
    }

    public void ContinueGame()
    {
        EventCenter.Instance.EventTrigger("ContinueGame");
    }
}
