using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonProcess : MonoBehaviour
{
    static string path;
    static string jsonString;
    string JsonPath()
    {
        return Application.streamingAssetsPath + "/JsonTest.json";
    }
    //void SaveJson(JsonEntity jsonentity)
    //{
    //    //如果本地没有对应的json 文件，重新创建
    //    if (!File.Exists(JsonPath()))
    //    {
    //        File.Create(JsonPath());
    //    }
    //    string json = JsonUtility.ToJson(jsonentity, true);
    //    File.WriteAllText(JsonPath(), json);
    //    Debug.Log("保存成功");
    //}

    private void Start()
    {
        path = JsonPath();
        jsonString = File.ReadAllText(path);
        JsonEntity jsonEntity = JsonUtility.FromJson<JsonEntity>(jsonString);
        if (jsonEntity.is_FirstLogin)
        {
            jsonEntity.RefreshData(); //更新初始json状态为全不激活
            jsonEntity.InitiateJson();
        }


    }
    public static JsonEntity requestJson() //获取某个是否被锁
    {
        jsonString = File.ReadAllText(path);
        JsonEntity json = JsonUtility.FromJson<JsonEntity>(jsonString);
        return json;
    }

}

