using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StateBar : MonoBehaviour
{
    public Slider stateBar;

    [Header("基础数值设定")]
    public float maxValue;
    public float currentValue;

    protected virtual void UpdateSliderValue(float value,float duration)
    {
        currentValue = Mathf.Clamp(currentValue + value, 0, maxValue);
        stateBar.DOValue(currentValue/maxValue, duration, false);
    }
}
