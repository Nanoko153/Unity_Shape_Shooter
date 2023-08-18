using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    TMP_Text text;
    //Animation anim;
    void Start()
    {
        text = GetComponent<TMP_Text>();
        //anim = GetComponent<Animation>();
        EventCenter.Instance.AddEventListener<string>("SwitchLevelUI", SwitchLevelUI);
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<string>("SwitchLevelUI", SwitchLevelUI);
    }

    public void SwitchLevelUI(string text)
    {
        transform.DOShakePosition(0.3f, new Vector3(5, 5, 0), 10, 90);
        this.text.text = text;
    }
}
