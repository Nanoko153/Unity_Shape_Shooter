using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Information : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TMP_Text rankText;
    public TMP_Text playerNameText;
    public TMP_Text timeText;
    public TMP_Text waveText;
    public TMP_Text scoreText;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
}
