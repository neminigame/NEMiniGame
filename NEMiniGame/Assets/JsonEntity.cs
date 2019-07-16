using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class JsonEntity
{
    public Boolean home_state;
    public Boolean school_state;
    public Boolean company_state;
    public Boolean hospital_state;
    public Boolean teach_state;
    string JsonPath()
    {
        return Application.streamingAssetsPath + "/JsonTest.json";
    }

    public void RefreshData()
    {
        home_state = false;
        school_state = false;
        company_state = false;
        hospital_state = false;
        teach_state = false;
    }
    public string Tojson()
    {
        return JsonUtility.ToJson(this);
    }
    public void  updateTeachState(bool state)
    {
        teach_state = state;
        SaveJson(this);
    }
    public void updateHomeState(bool state)
    {
        home_state = state;
        SaveJson(this);
    }
    public void updateSchoolState(bool state)
    {
        school_state = state;
        SaveJson(this);
    }
    public void updateCompanyState(bool state)
    {
        company_state = state;
        SaveJson(this);
    }
    public void updateHospitalState(bool state)
    {
        hospital_state = state;
        SaveJson(this);
    }

    public void InitiateJson()
    {
        SaveJson(this);
    }
     void SaveJson(JsonEntity jsonentity)
    {
        //如果本地没有对应的json 文件，重新创建
        if (!File.Exists(JsonPath()))
        {
            File.Create(JsonPath());
        }
        string json = JsonUtility.ToJson(jsonentity, true);
        File.WriteAllText(JsonPath(), json);
        Debug.Log("保存成功");
    }
}
