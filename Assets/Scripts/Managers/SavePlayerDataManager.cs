using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerDataManager : PersistentSingleton<SavePlayerDataManager>
{
    public string SaveName = "Player_Score";

    protected override void Awake()
    {
        base.Awake();
    }

    public void SavePlayerScoreData(string playerName, string useTime, int wave, int score)
    {
        var playerScoreData = LoadPlayScoreData();

        playerScoreData.list.Add(new PlayerScore(playerName, useTime, wave.ToString(), score));
        playerScoreData.list.Sort((x, y) => y.score.CompareTo(x.score));

        SaveSystem.SaveByJson(SaveName, playerScoreData);
    }

    public PlayerScoreData LoadPlayScoreData()
    {
        var playerScoreData = new PlayerScoreData();

        if(SaveSystem.SaveFileExists(SaveName))
        {
            playerScoreData = SaveSystem.LoadFromJson<PlayerScoreData>(SaveName);
        }
        else
        {
            while(playerScoreData.list.Count < 10)
            {
                playerScoreData.list.Add(new PlayerScore("-----", "--:--.---", "--", 0));
            }

            SaveSystem.SaveByJson(SaveName, playerScoreData);
        }

        return playerScoreData;
    }
}
