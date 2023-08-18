using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] playerBulletPools;      //生成玩家子弹相关的数组
    [SerializeField] Pool[] enemyBulletPools;      //生成玩家子弹相关的数组

    [SerializeField] Pool[] enemiesPools;      //生成玩家子弹相关的数组

    [SerializeField] Pool[] bossPools;
    [SerializeField] Pool[] collection;

    [SerializeField] Pool[] vfxPools;

    static Dictionary<GameObject, Pool> dictionary;            //创建一个检索key是GameObject，检索值是Pool的字典

    private void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();    //初始化字典
        Initialize(playerBulletPools);
        Initialize(enemyBulletPools);
        Initialize(enemiesPools);
        Initialize(bossPools);
        Initialize(collection);
        Initialize(vfxPools);
    }

    #if UNITY_EDITOR //游戏停止时查看对象池大小
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
    /// 检测对象池大小，以及运行时的大小
    /// </summary>
    /// <param name="pools"></param>
    void CheckPoolSize(Pool[] pools)
    {
        foreach(var pool in pools)
        {
            if (pool.RuntimeSize>pool.Size)
            {
                Debug.LogWarning(string.Format("溢出对象池名称：{0}，运行时大小{1}，设置大小{2}",pool.Prefab.name,pool.RuntimeSize,pool.Size));
            }
        }
    }

    /// <summary>
    /// 各种Pool的生成，每生成一种Pool就创建一个物体来存储相关的对象
    /// </summary>
    /// <param name="pools"></param>
    void Initialize(Pool[] pools)
    {
        foreach(var pool in pools)
        {
    #if UNITY_EDITOR //只有在unity编译器中才能被编译运行
            if(dictionary.ContainsKey(pool.Prefab))             //字典种查找是否有相同的键值
            {
                Debug.LogError("出现相同预制体："+pool.Prefab.name);
                continue;
            }
    #endif

            dictionary.Add(pool.Prefab, pool);

            Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;
            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }

    #region 从字典中找到传入prefab对应的对象池并通过对象池中的函数取出一个预制件
    /// <summary>
    /// 根据传入的<paramref>，找到相关的对象池
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("对象池中没有这个预制体：" + prefab.name);
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
            Debug.LogError("对象池中没有这个预制体：" + prefab.name);
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
            Debug.LogError("对象池中没有这个预制体：" + prefab.name);
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
            Debug.LogError("对象池中没有这个预制体：" + prefab.name);
            return null;
        }
        #endif
        return dictionary[prefab].PreparedObject(position,rotation,localScale);
    }
#endregion
}