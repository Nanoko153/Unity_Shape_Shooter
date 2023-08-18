using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactivate : MonoBehaviour
{
    [SerializeField] bool destroyGameObject;
    [SerializeField] float LifeTime = 5f;
    float tempTime;

    void OnEnable()
    {
        tempTime = LifeTime;
    }

    void FixedUpdate()
    {
        if(destroyGameObject)
            Destroy(gameObject);
        else
        {
            tempTime -=Time.fixedDeltaTime;
            if(tempTime <= 0)
                gameObject.SetActive(false);
        }
    }
}
