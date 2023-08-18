using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    public int score;
    int currentScore;
    [SerializeField] Vector3 scoreTextScale = new Vector3(1.1f, 1.1f,1f);

    public TMP_Text scoreText;

    Coroutine addScoreCoroutine;

    public Timer timer;

    protected override void Awake()
    {
        base.Awake();

        scoreText = GetComponentInChildren<TMP_Text>();
        timer = GetComponentInChildren<Timer>();
    }

    private void Start()
    {

        EventCenter.Instance.AddEventListener<int>("AddScore", AddScore);
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<int>("AddScore", AddScore);
    }

    public void UpdateText(int score)
    {
        scoreText.text = score.ToString("000000000");
    }

    public void ScaleText(Vector3 targetScale)
    {
        scoreText.rectTransform.localScale = targetScale;
    }

    public void ResetScore()
    {
        score = 0;
        UpdateText(score);
    }

    public void AddScore(int scorePoint)
    {
        currentScore += scorePoint;
        if(addScoreCoroutine != null)
            StopCoroutine(addScoreCoroutine);
        addScoreCoroutine = StartCoroutine(nameof(AddScoreCoroutine));
    }

    IEnumerator AddScoreCoroutine()
    {
        //ScaleText(scoreTextScale);

        while(score<currentScore)
        {
            score += 1;
            UpdateText(score);

            //ScaleText(scoreTextScale);

            yield return new WaitForFixedUpdate();

            //ScaleText(Vector3.one);
        }

        //ScaleText(Vector3.one);
    }

}
