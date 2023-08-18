using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TypeText : MonoBehaviour
{
    TMP_Text text;
    //Animation anim;
    void Start()
    {
        text = GetComponent<TMP_Text>();
        //anim = GetComponent<Animation>();
        EventCenter.Instance.AddEventListener<string>("SwitchTypeUI", SwitchTypeUI);
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<string>("SwitchTypeUI", SwitchTypeUI);
    }

    public void SwitchTypeUI(string text)
    {
        transform.DOShakePosition(0.3f, new Vector3(5, 5, 0), 10, 90);
        this.text.text = text;
        if(text == "¦Á")
            this.text.color = Color.red;
        else
            this.text.color = Color.blue;
        //anim.Play();
    }
}
