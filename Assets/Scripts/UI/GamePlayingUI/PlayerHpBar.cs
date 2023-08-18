using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHpBar : StateBar
{
    public Text text;
    private void Start()
    {
        stateBar = GetComponent<Slider>();
        text = GetComponentInChildren<Text>();
        EventCenter.Instance.AddEventListener<float>("PlayerHpRecover", Recover);
        EventCenter.Instance.AddEventListener<float>("PlayerHpReduce", Reduce);
        EventCenter.Instance.AddEventListener<float>("PlayerHpInit", PlayHpBarInit);

    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<float>("PlayerHpRecover", Recover);
        EventCenter.Instance.RemoveEventListener<float>("PlayerHpReduce", Reduce);
        EventCenter.Instance.RemoveEventListener<float>("PlayerHpInit", PlayHpBarInit);

    }

    public void PlayHpBarInit(float maxValue)
    {
            this.maxValue = maxValue;
            this.currentValue = maxValue;
            UpdateText();
    }

    public void Recover(float recoverValue)
    {
        UpdateSliderValue(recoverValue,  0.5f);
        UpdateText();
    }

    public void Reduce(float damageValue)
    {
        UpdateSliderValue(-damageValue, 0.5f);
        UpdateText();
        transform.DOShakePosition(1f, new Vector3(1f, 0.5f, 0), 10, 90, true, true, ShakeRandomnessMode.Full);
    }

    public void UpdateText()
    {
        text.text = currentValue.ToString();
    }
}
