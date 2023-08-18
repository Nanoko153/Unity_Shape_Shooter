using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    Collider2D coll;
    public float durationTime = 30f;
    float timeInOnEnable;

    public float moveSpeed;

    Vector3 movePos;
    Vector2 moveDir;

    [Header("音效")]
    public AudioClip onEnableSFX;
    public AudioClip getSFX;
    public AudioClip onDisableSFX;

    [Header("特效")]
    public GameObject disableVFX;

    protected virtual void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    protected virtual void OnEnable()
    {
        //AudioManager.Instance.PlaySFX(onEnableSFX);
        timeInOnEnable = 0;
        movePos = ViewportManager.Instance.RandomAllPosition(0, 0);

        moveDir = (movePos - transform.position).normalized;
    }

    protected virtual void FixedUpdate()
    {
        timeInOnEnable += Time.fixedDeltaTime;
        if(timeInOnEnable >= durationTime)
        {
            //AudioManager.Instance.PlaySFX(onDisableSFX);
            gameObject.SetActive(false);
        }

        //移动
        Vector2 temp = transform.position - movePos;
        if(temp.SqrMagnitude() < 0.1)
        {
            movePos = ViewportManager.Instance.RandomAllPosition(0, 0);
        }

        //更新移动方向
        moveDir = (movePos - transform.position).normalized;
        transform.Translate(moveSpeed * moveDir * Time.fixedDeltaTime);
    }
}
