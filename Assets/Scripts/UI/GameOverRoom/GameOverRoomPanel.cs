using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOverRoomPanel : MonoBehaviour
{
    public GameOverRoomInformation information;
    PlayerScoreData playerScoreData;

    public AudioClip disPlayInformationSFX;

    void Awake()
    {
        information = GetComponentInChildren<GameOverRoomInformation>();
    }

    private void Start()
    {
        playerScoreData = SavePlayerDataManager.Instance.LoadPlayScoreData();

        StartCoroutine(DisplayPlayData());
    }

    IEnumerator DisplayPlayData()
    {
        information.playerNameCanvasGroup.alpha = 0;
        information.timeTextCanvasGroup.alpha = 0;
        information.waveTextCanvasGroup.alpha = 0;
        information.scoreTextCanvasGroup.alpha = 0;
        yield return new WaitForSeconds(1f);

        AudioManager.Instance.PlayUI(disPlayInformationSFX);
        information.playerNameCanvasGroup.DOFade(1, 0.5f);
        information.playerNameText.text = GameManager.Instance.tempPlayerScore.playerName;
        yield return new WaitForSeconds(0.5f);

        AudioManager.Instance.PlayUI(disPlayInformationSFX);
        information.timeTextCanvasGroup.DOFade(1, 0.5f);
        information.timeText.text = GameManager.Instance.tempPlayerScore.useTime;
        yield return new WaitForSeconds(0.5f);

        AudioManager.Instance.PlayUI(disPlayInformationSFX);
        information.waveTextCanvasGroup.DOFade(1, 0.5f);
        information.waveText.text = GameManager.Instance.tempPlayerScore.wave;
        yield return new WaitForSeconds(0.5f);

        AudioManager.Instance.PlayUI(disPlayInformationSFX);
        information.scoreTextCanvasGroup.DOFade(1, 0.5f);
        information.scoreText.text = GameManager.Instance.tempPlayerScore.score.ToString();
    }
}
