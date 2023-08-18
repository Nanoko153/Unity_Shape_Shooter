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
        var json = JsonUtility.ToJson(data, true);    //将data转成JSON文本

        var path = Path.Combine(Application.persistentDataPath, saveFileName);  //文件路径 多平台相对+自定义路径

        try
        {
            File.WriteAllText(path, json);

            Debug.Log($"存档成功\n{path}");
        }
        catch(System.Exception exception)
        {
            #if UNITY_EDITOR

            Debug.LogError($"存档发生错误{path}\n{exception}");

            #endif
        }
    }

    public static T LoadFromJson<T>(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);  //文件路径 多平台相对+自定义路径
        Debug.Log(Application.persistentDataPath);

        try
        {
            var json = File.ReadAllText(path);

            var data = JsonUtility.FromJson<T>(json);

            Debug.Log($"读档成功\n{path}");

            return data;
        }
        catch(System.Exception exception)
        {
            #if UNITY_EDITOR

            Debug.LogError($"读档发生错误{path}\n{exception}");

            #endif

            return default;
        }
    }

    public static void DeleteSaveFile(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);  //文件路径 多平台相对+自定义路径

        try
        {
            File.Delete(path);
        }
        catch(System.Exception exception)
        {
            #if UNITY_EDITOR

            Debug.LogError($"删档发生错误{path}\n{exception}");

            #endif
        }
    }

    public static bool SaveFileExists(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        return File.Exists(path);
    }
}
