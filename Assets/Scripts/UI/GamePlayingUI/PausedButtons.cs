using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedButtons : MonoBehaviour
{
    public void ContinueGame()
    {
        EventCenter.Instance.EventTrigger("ContinueGame");
    }

    public void ReturnMainMenu()
    {
        EventCenter.Instance.EventTrigger("ReturnMainMenu");
    }
}
