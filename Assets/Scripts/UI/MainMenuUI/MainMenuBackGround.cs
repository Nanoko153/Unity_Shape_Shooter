using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuBackGround : MonoBehaviour
{
    public Renderer rend;
    public Material material;

    public float time;

    public int type = 3;

    public float switchTime = 10f;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void Start()
    {
        material = rend.material;
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if(time >= switchTime)
        {
            time = 0;

            if(type == 1)
            {
                material.DOColor(new Color(0.6f, 0.03f, 0.03f), "_BaseColor", 8);
                type++;
            }
            else if(type == 2)
            {
                material.DOColor(new Color(0.03f, 0.6f, 0.03f), "_BaseColor", 8);
                type++;
            }
            else if(type == 3)
            {
                material.DOColor(new Color(0.03f, 0.03f, 0.6f), "_BaseColor", 8);
                type++;
            }

            if(type == 4)
                type = 1;
        }
    }
}
