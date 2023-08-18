using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [Header("当前波次")]
    public int currentWaveNum;

    [Header("最大敌人数量")]
    [SerializeField] int maxEnemiesNum = 50;

    // [Header("当前敌人数量")]
    // [SerializeField] int currentEnemiesNum;

    // [Header("最大敌人等级")]
    // [SerializeField] int maxEnemiesLevel;

    // [Header("当前敌人等级")]
    // [SerializeField] int currentEnemiesLevel;

    [Header("当前波次需要生成的敌人数量")]
    [SerializeField] int currentWaveSpawnTargetEnemyNum;

    [Header("当前场景内最多存在的敌人数量")]
    [SerializeField] int maximumEnemiesInTheScene = 10;

    [Header("当前波次剩余需要生成的敌人数量")]
    [SerializeField] int currentWaveSpawnResidueEnemyNum;

    [Header("敌人生成时间间隔")]
    [SerializeField] float SpawnTimeInterval = 0.5F;

    [Header("敌人测试")]
    public bool isTest;
    public int testEnemyNum;

    Coroutine spawnEnemiesCoroutine;

    bool isAward = false;
    bool isBoss = false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        EventCenter.Instance.AddEventListener<int>("CheckCurrentEventNum", UpdateWaveData);
        EventCenter.Instance.AddEventListener("StartWave", StartWave);
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<int>("CheckCurrentEventNum", UpdateWaveData);
        EventCenter.Instance.RemoveEventListener("StartWave", StartWave);
    }

    public void UpdateWaveData(int currentEnemy)
    {
        //如果当前敌人为0后，波次增加， //并开始生成敌人
        if(currentEnemy == 0 && currentWaveSpawnResidueEnemyNum == 0)
        {
            //更新波次
            currentWaveNum++;

            //更新当前波次需要生成的敌人
            currentWaveSpawnTargetEnemyNum = Mathf.Clamp(currentWaveNum/2 + 5, 0, maxEnemiesNum);
            if(currentWaveNum % 2 == 0)
            {
                isAward = true;
            }
            if(currentWaveNum /10 == 0)
            {
                isBoss = true;
            }

            //开始生成敌人
            EventCenter.Instance.EventTrigger("PlayWaveUpdateUI");
        }
    }

    public void StartWave()
    {
        if(spawnEnemiesCoroutine != null)
            StopCoroutine(spawnEnemiesCoroutine);
        if(isTest)
            spawnEnemiesCoroutine = StartCoroutine(SpawnEnemiesCoroutine(currentWaveSpawnTargetEnemyNum, true));
        else
            spawnEnemiesCoroutine = StartCoroutine(SpawnEnemiesCoroutine(currentWaveSpawnTargetEnemyNum));
    }

    //生成敌人
    public void SpawnEnemies(int spawnNum)
    {
        for(int i = 0; i < spawnNum; i++)
        {
            PoolManager.Release(EnemyManager.Instance.EnemyPrefab[testEnemyNum]);
        }
    }

    //生成敌人测试协程
    IEnumerator SpawnEnemiesCoroutine(int spawnNum)
    {
        currentWaveSpawnResidueEnemyNum = currentWaveSpawnTargetEnemyNum;

        //检查当前波次所在分组
        int currentHadGroup = Mathf.Clamp(currentWaveNum / 3, 0, EnemyManager.Instance.enemyGroupList.Count - 1);
        Debug.Log("currentHadGroup:"+currentHadGroup);

        for(int i = 0; i < spawnNum; i++)
        {
            //等待场景内敌人数量小于场景内限制数量
            yield return new WaitUntil(() => EnemyManager.Instance.NumberOfEnemiesRemaining < maximumEnemiesInTheScene);

            //固定生成奖励
            if(isAward)
            {
                PoolManager.Release(EnemyManager.Instance.collectGroup[0], ViewportManager.Instance.GetOutOfViewPosition());
                PoolManager.Release(EnemyManager.Instance.collectGroup[Random.Range(0,3)], ViewportManager.Instance.GetOutOfViewPosition());
                isAward = false;
            }

            //概率生成奖励5%
            int awardProbability = Random.Range(0,100);
            if(awardProbability < 15)
            {
                awardProbability = Random.Range(0,100);
                if(awardProbability < 65)
                {
                    PoolManager.Release(EnemyManager.Instance.collectGroup[0], ViewportManager.Instance.GetOutOfViewPosition());
                }
                else
                    PoolManager.Release(EnemyManager.Instance.collectGroup[Random.Range(0,3)], ViewportManager.Instance.GetOutOfViewPosition());
            }

            //生成Boss
            if(isBoss)
            {

                isBoss = false;
            }

            //随机抽取一组
            int extractGroup = Random.Range(0, currentHadGroup+1);
            //Debug.Log("extractGroup:"+extractGroup);

            //随机抽取组内敌人预制件序号
            int extractEnemyIndexInGroup = Random.Range(0, EnemyManager.Instance.enemyGroupList[extractGroup].Length);
            //Debug.Log("extractEnemyIndexInGroup:"+extractEnemyIndexInGroup);

            //生成敌人
            PoolManager.Release(EnemyManager.Instance.enemyGroupList[extractGroup][extractEnemyIndexInGroup], ViewportManager.Instance.GetOutOfViewPosition());
            //Debug.Log("Spawn Wave:"+ currentWaveNum + "\t No:" + i+1 + "enemy");

            //需要生成敌人的数量--
            currentWaveSpawnResidueEnemyNum--;

            //等待生成时间间隔
            yield return new WaitForSeconds(SpawnTimeInterval);
        }
    }

    IEnumerator SpawnEnemiesCoroutine(int spawnNum, bool isTest)
    {
        currentWaveSpawnResidueEnemyNum = currentWaveSpawnTargetEnemyNum;
        for(int i = 0; i < spawnNum; i++)
        {
            //等待场景内敌人数量小于场景内限制数量
            yield return new WaitUntil(() => EnemyManager.Instance.NumberOfEnemiesRemaining < maximumEnemiesInTheScene);

            //生成敌人
            PoolManager.Release(EnemyManager.Instance.EnemyPrefab[testEnemyNum], ViewportManager.Instance.GetOutOfViewPosition());
            //Debug.Log("Spawn Wave:"+ currentWaveNum + "\t No:" + i+1 + "enemy");

            //需要生成敌人的数量--
            currentWaveSpawnResidueEnemyNum--;

            //等待生成时间间隔
            yield return new WaitForSeconds(SpawnTimeInterval);
        }
    }
}
