using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : PersistentSingleton<SaveSystem>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public static void SaveByJson(string saveFileName, object data)
    {
        var json = JsonUtility.ToJson(data, true);    //��dataת��JSON�ı�

        var path = Path.Combine(Application.persistentDataPath, saveFileName);  //�ļ�·�� ��ƽ̨���+�Զ���·��

        try
        {
            File.WriteAllText(path, json);

            Debug.Log($"�浵�ɹ�\n{path}");
        }
        catch(System.Exception exception)
        {
            #if UNITY_EDITOR

            Debug.LogError($"�浵��������{path}\n{exception}");

            #endif
        }
    }

    public static T LoadFromJson<T>(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);  //�ļ�·�� ��ƽ̨���+�Զ���·��
        Debug.Log(Application.persistentDataPath);

        try
        {
            var json = File.ReadAllText(path);

            var data = JsonUtility.FromJson<T>(json);

            Debug.Log($"�����ɹ�\n{path}");

            return data;
        }
        catch(System.Exception exception)
        {
            #if UNITY_EDITOR

            Debug.LogError($"������������{path}\n{exception}");

            #endif

            return default;
        }
    }

    public static void DeleteSaveFile(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);  //�ļ�·�� ��ƽ̨���+�Զ���·��

        try
        {
            File.Delete(path);
        }
        catch(System.Exception exception)
        {
            #if UNITY_EDITOR

            Debug.LogError($"ɾ����������{path}\n{exception}");

            #endif
        }
    }

    public static bool SaveFileExists(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        return File.Exists(path);
    }
}
