using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<EnemyController> enemyList;
    public int NumberOfEnemiesRemaining => enemyList.Count;

    public GameObject[] EnemyPrefab;

    public List<GameObject[]> enemyGroupList;

    public GameObject[] enemyGroup_1;
    public GameObject[] enemyGroup_2;
    public GameObject[] enemyGroup_3;
    public GameObject[] enemyGroup_4;
    public GameObject[] enemyGroup_5;
    public GameObject[] enemyGroup_6;
    public GameObject[] enemyGroup_7;
    public GameObject[] enemyGroup_8;
    public GameObject[] enemyGroup_9;
    public GameObject[] enemyGroup_10;
    public GameObject[] collectGroup;

    protected override void Awake()
    {
        base.Awake();
        enemyList = new List<EnemyController>();

        enemyGroupList = new List<GameObject[]>();
    }

    private void Start()
    {
        enemyGroupList.Add(enemyGroup_1);
        enemyGroupList.Add(enemyGroup_2);
        enemyGroupList.Add(enemyGroup_3);
        enemyGroupList.Add(enemyGroup_4);
        enemyGroupList.Add(enemyGroup_5);
        enemyGroupList.Add(enemyGroup_6);
        enemyGroupList.Add(enemyGroup_7);
        enemyGroupList.Add(enemyGroup_8);
        enemyGroupList.Add(enemyGroup_9);
        enemyGroupList.Add(enemyGroup_10);
    }

    /// <summary>
    /// 注册敌人
    /// </summary>
    /// <param name="enemy">EnemyController</param>
    public void RegisterEnemy(EnemyController enemy)
    {
        enemyList.Add(enemy);
    }

    /// <summary>
    /// 注销敌人
    /// </summary>
    /// <param name="enemy">EnemyController</param>
    public void RemoveEnemy(EnemyController enemy)
    {
        enemyList.Remove(enemy);

        //每次移除敌人都像事件中心发布一次当前敌人数量
        EventCenter.Instance.EventTrigger<int>("CheckCurrentEventNum", NumberOfEnemiesRemaining);
    }
}
