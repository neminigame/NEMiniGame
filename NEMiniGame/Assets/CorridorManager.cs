using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CorridorManager : MonoBehaviour
{
    public static CorridorManager instance;
    public Transform TeacherTransform;
    public List<Transform> CorridorStuTransforms;
    public List<Material> stuMats;
    public List<Material> checkMats;
    public Material teacherMat,teacherCheckMat;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        stuMats = new List<Material>();
        checkMats = new List<Material>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var temp = transform.GetChild(i);
            if (temp.name.Contains("an"))
            {
                CorridorStuTransforms.Add(temp);
                //Debug.Log(temp.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>());
                //Debug.Log(temp.GetChild(1).GetComponent<MeshRenderer>());
                stuMats.Add(temp.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial);
                checkMats.Add(temp.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial);
            }
            else
            {
                TeacherTransform = temp;
                teacherMat = temp.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial;
                teacherCheckMat = temp.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //isLerp表示是否启用动画，启用的话使用duration进行控制
    public void SetCorridorStusMeshAlpha(float alpha, bool isLerp = false, float duration = 0)
    {
        for (int i = 0; i < stuMats.Count; i++)
        {
            var colorTemp = stuMats[i].GetColor("_Color");
            if (isLerp)
            {
                stuMats[i].DOColor(new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha), "_Color", duration);
            }
            else
                stuMats[i].SetColor("_Color", new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha));
        }
    }
    public void SetCorridorCheckAlpha(float alpha, bool isLerp = false, float duration = 0)
    {
        for (int i = 0; i < checkMats.Count; i++)
        {
            var colorTemp = checkMats[i].GetColor("_Color");
            if (isLerp)
            {
                checkMats[i].DOColor(new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha), "_Color", duration);
            }
            else
                checkMats[i].SetColor("_Color", new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha));
        }
    }
    public void SetTeacherMeshAlpha(float alpha,bool isLerp = false,float duration=0)
    {
        var colorTemp = teacherMat.GetColor("_Color");
        if (isLerp)
        {
            teacherMat.DOColor(new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha), "_Color", duration);
        }
        else
            teacherMat.SetColor("_Color", new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha));       
    }
    public void SetTeacherCheckAlpha(float alpha, bool isLerp = false, float duration = 0)
    {
        var colorTemp = teacherCheckMat.GetColor("_Color");
        if (isLerp)
        {
            teacherCheckMat.DOColor(new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha), "_Color", duration);
        }
        else
            teacherCheckMat.SetColor("_Color", new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha));        
    }
    public void SetStusAlpha(float alpha, bool isLerp = false, float duration = 0)
    {
        SetCorridorStusMeshAlpha(alpha, isLerp, duration);
        SetCorridorCheckAlpha(0.3f * alpha, isLerp, duration);
    }
    public void SetTeacherAlpha(float alpha, bool isLerp = false, float duration = 0)
    {
        SetTeacherMeshAlpha(alpha, isLerp, duration);
        SetTeacherCheckAlpha(0.3f * alpha, isLerp, duration);
    }
}
