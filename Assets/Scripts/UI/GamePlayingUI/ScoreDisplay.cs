using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    int score;
    int currentScore;
    [SerializeField] Vector3 scoreTextScale = new Vector3(1.2f, 1.2f,1f);

    public TMP_Text scoreText;

    private void Awake()
    {

    }

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
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
        StartCoroutine(nameof(AddScoreCoroutine));
    }

    IEnumerator AddScoreCoroutine()
    {
        ScaleText(scoreTextScale);
        while(score<currentScore)
        {
            score += 1;
            UpdateText(score);

            yield return null;
        }

        ScaleText(Vector3.one);
    }
}
