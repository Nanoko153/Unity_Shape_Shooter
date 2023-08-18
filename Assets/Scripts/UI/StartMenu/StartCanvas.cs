using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartCanvas : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Coroutine gotoMainMenuCoroutine;
    void Awake()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }
    void Start()
    {
        canvasGroup.alpha = 0;
        if(gotoMainMenuCoroutine != null)
            StopCoroutine(gotoMainMenuCoroutine);
        gotoMainMenuCoroutine = StartCoroutine(GotoMainMenuCoroutine());
    }

    IEnumerator GotoMainMenuCoroutine()
    {
        canvasGroup.DOFade(1,1f);
        yield return new WaitUntil(() => canvasGroup.alpha == 1);
        yield return new WaitForSeconds(2f);

        EventCenter.Instance.EventTrigger("ReturnMainMenu");
    }

}
