using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [Header("��ǰ����")]
    public int currentWaveNum;

    [Header("����������")]
    [SerializeField] int maxEnemiesNum = 50;

    // [Header("��ǰ��������")]
    // [SerializeField] int currentEnemiesNum;

    // [Header("�����˵ȼ�")]
    // [SerializeField] int maxEnemiesLevel;

    // [Header("��ǰ���˵ȼ�")]
    // [SerializeField] int currentEnemiesLevel;

    [Header("��ǰ������Ҫ���ɵĵ�������")]
    [SerializeField] int currentWaveSpawnTargetEnemyNum;

    [Header("��ǰ�����������ڵĵ�������")]
    [SerializeField] int maximumEnemiesInTheScene = 10;

    [Header("��ǰ����ʣ����Ҫ���ɵĵ�������")]
    [SerializeField] int currentWaveSpawnResidueEnemyNum;

    [Header("��������ʱ����")]
    [SerializeField] float SpawnTimeInterval = 0.5F;

    [Header("���˲���")]
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
        //�����ǰ����Ϊ0�󣬲������ӣ� //����ʼ���ɵ���
        if(currentEnemy == 0 && currentWaveSpawnResidueEnemyNum == 0)
        {
            //���²���
            currentWaveNum++;

            //���µ�ǰ������Ҫ���ɵĵ���
            currentWaveSpawnTargetEnemyNum = Mathf.Clamp(currentWaveNum/2 + 5, 0, maxEnemiesNum);
            if(currentWaveNum % 2 == 0)
            {
                isAward = true;
            }
            if(currentWaveNum /10 == 0)
            {
                isBoss = true;
            }

            //��ʼ���ɵ���
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

    //���ɵ���
    public void SpawnEnemies(int spawnNum)
    {
        for(int i = 0; i < spawnNum; i++)
        {
            PoolManager.Release(EnemyManager.Instance.EnemyPrefab[testEnemyNum]);
        }
    }

    //���ɵ��˲���Э��
    IEnumerator SpawnEnemiesCoroutine(int spawnNum)
    {
        currentWaveSpawnResidueEnemyNum = currentWaveSpawnTargetEnemyNum;

        //��鵱ǰ�������ڷ���
        int currentHadGroup = Mathf.Clamp(currentWaveNum / 3, 0, EnemyManager.Instance.enemyGroupList.Count - 1);
        Debug.Log("currentHadGroup:"+currentHadGroup);

        for(int i = 0; i < spawnNum; i++)
        {
            //�ȴ������ڵ�������С�ڳ�������������
            yield return new WaitUntil(() => EnemyManager.Instance.NumberOfEnemiesRemaining < maximumEnemiesInTheScene);

            //�̶����ɽ���
            if(isAward)
            {
                PoolManager.Release(EnemyManager.Instance.collectGroup[0], ViewportManager.Instance.GetOutOfViewPosition());
                PoolManager.Release(EnemyManager.Instance.collectGroup[Random.Range(0,3)], ViewportManager.Instance.GetOutOfViewPosition());
                isAward = false;
            }

            //�������ɽ���5%
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

            //����Boss
            if(isBoss)
            {

                isBoss = false;
            }

            //�����ȡһ��
            int extractGroup = Random.Range(0, currentHadGroup+1);
            //Debug.Log("extractGroup:"+extractGroup);

            //�����ȡ���ڵ���Ԥ�Ƽ����
            int extractEnemyIndexInGroup = Random.Range(0, EnemyManager.Instance.enemyGroupList[extractGroup].Length);
            //Debug.Log("extractEnemyIndexInGroup:"+extractEnemyIndexInGroup);

            //���ɵ���
            PoolManager.Release(EnemyManager.Instance.enemyGroupList[extractGroup][extractEnemyIndexInGroup], ViewportManager.Instance.GetOutOfViewPosition());
            //Debug.Log("Spawn Wave:"+ currentWaveNum + "\t No:" + i+1 + "enemy");

            //��Ҫ���ɵ��˵�����--
            currentWaveSpawnResidueEnemyNum--;

            //�ȴ�����ʱ����
            yield return new WaitForSeconds(SpawnTimeInterval);
        }
    }

    IEnumerator SpawnEnemiesCoroutine(int spawnNum, bool isTest)
    {
        currentWaveSpawnResidueEnemyNum = currentWaveSpawnTargetEnemyNum;
        for(int i = 0; i < spawnNum; i++)
        {
            //�ȴ������ڵ�������С�ڳ�������������
            yield return new WaitUntil(() => EnemyManager.Instance.NumberOfEnemiesRemaining < maximumEnemiesInTheScene);

            //���ɵ���
            PoolManager.Release(EnemyManager.Instance.EnemyPrefab[testEnemyNum], ViewportManager.Instance.GetOutOfViewPosition());
            //Debug.Log("Spawn Wave:"+ currentWaveNum + "\t No:" + i+1 + "enemy");

            //��Ҫ���ɵ��˵�����--
            currentWaveSpawnResidueEnemyNum--;

            //�ȴ�����ʱ����
            yield return new WaitForSeconds(SpawnTimeInterval);
        }
    }
}
