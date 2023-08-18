using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] playerBulletPools;      //��������ӵ���ص�����
    [SerializeField] Pool[] enemyBulletPools;      //��������ӵ���ص�����

    [SerializeField] Pool[] enemiesPools;      //��������ӵ���ص�����

    [SerializeField] Pool[] bossPools;
    [SerializeField] Pool[] collection;

    [SerializeField] Pool[] vfxPools;

    static Dictionary<GameObject, Pool> dictionary;            //����һ������key��GameObject������ֵ��Pool���ֵ�

    private void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();    //��ʼ���ֵ�
        Initialize(playerBulletPools);
        Initialize(enemyBulletPools);
        Initialize(enemiesPools);
        Initialize(bossPools);
        Initialize(collection);
        Initialize(vfxPools);
    }

    #if UNITY_EDITOR //��Ϸֹͣʱ�鿴����ش�С
    private void OnDestroy()
    {
        CheckPoolSize(playerBulletPools);
        CheckPoolSize(enemyBulletPools);
        CheckPoolSize(enemiesPools);
        CheckPoolSize(bossPools);
        CheckPoolSize(collection);
        CheckPoolSize(vfxPools);
    }
    #endif

    /// <summary>
    /// ������ش�С���Լ�����ʱ�Ĵ�С
    /// </summary>
    /// <param name="pools"></param>
    void CheckPoolSize(Pool[] pools)
    {
        foreach(var pool in pools)
        {
            if (pool.RuntimeSize>pool.Size)
            {
                Debug.LogWarning(string.Format("�����������ƣ�{0}������ʱ��С{1}�����ô�С{2}",pool.Prefab.name,pool.RuntimeSize,pool.Size));
            }
        }
    }

    /// <summary>
    /// ����Pool�����ɣ�ÿ����һ��Pool�ʹ���һ���������洢��صĶ���
    /// </summary>
    /// <param name="pools"></param>
    void Initialize(Pool[] pools)
    {
        foreach(var pool in pools)
        {
    #if UNITY_EDITOR //ֻ����unity�������в��ܱ���������
            if(dictionary.ContainsKey(pool.Prefab))             //�ֵ��ֲ����Ƿ�����ͬ�ļ�ֵ
            {
                Debug.LogError("������ͬԤ���壺"+pool.Prefab.name);
                continue;
            }
    #endif

            dictionary.Add(pool.Prefab, pool);

            Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;
            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }

    #region ���ֵ����ҵ�����prefab��Ӧ�Ķ���ز�ͨ��������еĺ���ȡ��һ��Ԥ�Ƽ�
    /// <summary>
    /// ���ݴ����<paramref>���ҵ���صĶ����
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�������û�����Ԥ���壺" + prefab.name);
            return null;
        }
        #endif
        return dictionary[prefab].PreparedObject();
    }

    public static GameObject Release(GameObject prefab,Vector3 position)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�������û�����Ԥ���壺" + prefab.name);
            return null;
        }
        #endif
        return dictionary[prefab].PreparedObject(position);
    }
    public static GameObject Release(GameObject prefab, Vector3 position,Quaternion rotation)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�������û�����Ԥ���壺" + prefab.name);
            return null;
        }
        #endif
        return dictionary[prefab].PreparedObject(position,rotation);
    }
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�������û�����Ԥ���壺" + prefab.name);
            return null;
        }
        #endif
        return dictionary[prefab].PreparedObject(position,rotation,localScale);
    }
#endregion
}