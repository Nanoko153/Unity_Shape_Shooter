using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    Animator anim;

    public TMP_InputField playerNameText;
    public TMP_Text text;

    public AudioClip nextButtonSFX;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerNameText = GetComponentInChildren<TMP_InputField>();
    }
    private void Start()
    {
        playerNameText.text = "Nobody";

        EventCenter.Instance.AddEventListener("GameOver",PlayGameOverAnim);
        //EventCenter.Instance.AddEventListener("GameOver",GameOverToSaveData);
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("GameOver",PlayGameOverAnim);
        //EventCenter.Instance.RemoveEventListener("GameOver",GameOverToSaveData);
    }

    public void PlayGameOverAnim()
    {
        anim.Play("GameOver");
    }

    public void NextButton()
    {
        AudioManager.Instance.PlayUI(nextButtonSFX);
        GameOverToSaveData();
        EventCenter.Instance.EventTrigger("LoadGameOverRoomScene");
    }

    public void GameOverToSaveData()
    {
        //保存数据到硬盘
        string text = playerNameText.text;
        SavePlayerDataManager.Instance.SavePlayerScoreData
        (text, ScoreManager.Instance.timer.timeText.text, WaveManager.Instance.currentWaveNum, ScoreManager.Instance.score);

        //保存数据到中间变量
        GameManager.Instance.tempPlayerScore.playerName = text;
        GameManager.Instance.tempPlayerScore.useTime = ScoreManager.Instance.timer.timeText.text;
        GameManager.Instance.tempPlayerScore.wave = WaveManager.Instance.currentWaveNum.ToString();
        GameManager.Instance.tempPlayerScore.score = ScoreManager.Instance.score;
    }
}
