using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScorePanle : MonoBehaviour
{
    public Information[] information;
    PlayerScoreData playerScoreData;

    public AudioClip displayPlayDataAudio;

    void Awake()
    {
        information = GetComponentsInChildren<Information>();
    }

    private void Start()
    {
        playerScoreData = SavePlayerDataManager.Instance.LoadPlayScoreData();

        StartCoroutine(DisplayPlayData());
    }

    IEnumerator DisplayPlayData()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < information.Length; i++)
        {
            AudioManager.Instance.PlayUI(displayPlayDataAudio);

            information[i].canvasGroup.alpha = 0;
            information[i].canvasGroup.DOFade(1, 0.5f);

            information[i].rankText.text = (i+1).ToString();
            information[i].playerNameText.text = playerScoreData.list[i].playerName;
            information[i].timeText.text = playerScoreData.list[i].useTime;
            information[i].waveText.text = playerScoreData.list[i].wave;
            information[i].scoreText.text = playerScoreData.list[i].score.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
