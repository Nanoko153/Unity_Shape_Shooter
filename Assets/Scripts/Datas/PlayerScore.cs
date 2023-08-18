using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScore
{
        public string playerName;
        public string useTime;
        public string wave;
        public int score;

        public PlayerScore(string playerName, string useTime, string wave, int score)
        {
            this.playerName = playerName;
            this.useTime = useTime;
            this.wave = wave;
            this.score = score;
        }
}
