using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]public class Pool
{
    public GameObject Prefab{ get { return prefab; } }

    public int Size{ get { return size; } }
    public int RuntimeSize { get { return queue.Count; } }


    [SerializeField] GameObject prefab; //������Ҫ��Ԥ�Ƽ������룩
    [SerializeField] int size = 1;      //��������صĴ�С����

    Queue<GameObject> queue;            //�������� ����<GameObject> ����queue

    Transform parent;

    public void Initialize(Transform parent)
    {
        queue = new Queue<GameObject>();            //new����
        this.parent = parent;

        for(var i=0;i<size;i++)                     //����size��С��Ԥ�Ƽ����������
        {
            queue.Enqueue(Copy());
        }
    }

    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab,parent);  //����Ԥ�����copy
        copy.SetActive(false);                      //���õ�Ԥ����

        return copy;                                //�������ɵ�Ԥ����copy
    }

    GameObject AvailableObject()
    {
        GameObject availableObject = null;              //����һ��GameObject���м����

        if(queue.Count>0 && !queue.Peek().activeSelf)   //���ڴ���0ʱ,���ҵ�һ������δ����
        {
            availableObject = queue.Dequeue();          //�м������ó��ڵ�GameObject
        }
        else
        {
            availableObject = Copy();                   //��������copyһ��Ԥ�Ƽ�
        }

        queue.Enqueue(availableObject);                  //������б���ȡ�����Ϸ��ض����

        return availableObject;                         //�����м����
    }

    #region ��ȡ����ض���������
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
