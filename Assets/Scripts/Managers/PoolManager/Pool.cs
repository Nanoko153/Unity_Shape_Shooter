using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]public class Pool
{
    public GameObject Prefab{ get { return prefab; } }

    public int Size{ get { return size; } }
    public int RuntimeSize { get { return queue.Count; } }


    [SerializeField] GameObject prefab; //声明需要的预制件（拖入）
    [SerializeField] int size = 1;      //声明对象池的大小变量

    Queue<GameObject> queue;            //声明队列 类型<GameObject> 名称queue

    Transform parent;

    public void Initialize(Transform parent)
    {
        queue = new Queue<GameObject>();            //new队列
        this.parent = parent;

        for(var i=0;i<size;i++)                     //生成size大小的预制件并存入队列
        {
            queue.Enqueue(Copy());
        }
    }

    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab,parent);  //创建预制体的copy
        copy.SetActive(false);                      //禁用掉预制体

        return copy;                                //返回生成的预制体copy
    }

    GameObject AvailableObject()
    {
        GameObject availableObject = null;              //设置一个GameObject的中间变量

        if(queue.Count>0 && !queue.Peek().activeSelf)   //池内大于0时,并且第一个对象未激活
        {
            availableObject = queue.Dequeue();          //中间变量获得池内的GameObject
        }
        else
        {
            availableObject = Copy();                   //否则重新copy一个预制件
        }

        queue.Enqueue(availableObject);                  //对象出列被获取后马上返回对象池

        return availableObject;                         //返回中间变量
    }

    #region 获取对象池对象及其重载
    public GameObject PreparedObject()
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);

        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position)
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;

        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position,Quaternion rotation)
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;

        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position, Quaternion rotation,Vector3 localScale)
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;

        return preparedObject;
    }
    #endregion

    public void Return(GameObject gameObject)
    {
        queue.Enqueue(gameObject);
    }
}
