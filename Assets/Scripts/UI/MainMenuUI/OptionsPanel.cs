using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanel : MonoBehaviour
{
    public Animator anim;

    public bool isIn = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // void Start()
    // {
    //     EventCenter.Instance.AddEventListener("InOptionsPanel",InOptionsPanel);
    //     EventCenter.Instance.AddEventListener("OutOptionPanel",OutOptionPanel);
    // }

    // void OnDestroy()
    // {
    //     EventCenter.Instance.AddEventListener("InOptionsPanel",InOptionsPanel);
    //     EventCenter.Instance.AddEventListener("OutOptionPanel",OutOptionPanel);
    // }

    public void InOptionsPanel()
    {
        anim.Play("InOptionsPanel");
    }

    public void OutOptionPanel()
    {
        anim.Play("OutOptionsPanel");
    }
}
