using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timeText;
    public float gamePlayingTime;

    Coroutine startTimerCoroutine;

    void Awake()
    {
        timeText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        EventCenter.Instance.AddEventListener("StartTimer", StartTimer);
    }
    void OnDestroy()
    {
        StopAllCoroutines();

        EventCenter.Instance.RemoveEventListener("StartTimer", StartTimer);
    }

    private void FixedUpdate()
    {

    }

    public void StartTimer()
    {
        if(startTimerCoroutine != null)
            StopCoroutine(startTimerCoroutine);
        startTimerCoroutine = StartCoroutine(StartTimerCoroutine());
    }

    IEnumerator StartTimerCoroutine()
    {
        while(GameManager.Instance.gameState == GameState.GamePlaying)
        {
            gamePlayingTime += Time.fixedDeltaTime;
            var ts = System.TimeSpan.FromSeconds(gamePlayingTime);
            timeText.text = string.Format("{0:00}:{1:00}.{2:000}s", ts.Minutes, ts.Seconds, ts.Milliseconds);
            yield return new WaitForFixedUpdate();
        }
    }

}
