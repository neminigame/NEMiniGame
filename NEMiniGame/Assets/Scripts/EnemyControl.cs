using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public enum EnemyType
    {
        Dad = 1,
        Mom,
        DD,
        Dog,
        Robot1,
        Robot2,
        Class1Woman1,
        Class1Woman2,
        Class1Woman3,
        Class1Man1,
        Class1Man2,
        Class1Man3,
        Teacher,
        CorridorMan1,
        CorridorMan2,
        CorridorMan3,
        CorridorMan4,
        CorridorMan5,
        Other
    }
    public enum LoopType
    {
        None,
        Loop,
        Pinpong
    }
    public enum DetectType
    {
        Partol,
        FindOne,
        FindTwo
    }
    public GameMode gameMode = GameMode.Normal;
    public EnemyType enemyType;
    public float speed = 1.0f;
    [Tooltip("巡视后停留时长")]
    public float stayTime = 3f;//敌人持续同一个方向的时间
    public LoopType loopType; 
    public iTween.EaseType easeType;
    public GameObject player;
    public int detectNum;
    public float restTime=3.0f;
    private bool isAlive = true; 
    private Transform target;
    private bool isRevert=false;
    private bool haveupgrade=false;
    private GameObject exclamationMark;
    private GameObject questionMark;
    public Material meshMat;
    public float upgradeScale;
    public bool UsePosition = false;
    private Camera cam;
    private Vector3 upgradeTrans;
    public BoxCollider boxCollider;
    public bool isStartUpgrade;
    public float animTime=1.5f;
    // Start is called before the first frame update
    void Start()
    {
        isStartUpgrade = false;
        upgradeTrans = transform.Find("check").localScale * upgradeScale;
        exclamationMark = transform.Find("ExclamationMark").gameObject;
        questionMark = transform.Find("QuestionMark").gameObject;
        cam = Camera.main;
        detectNum = 0;

        try
        {
            boxCollider = transform.GetChild(0).GetComponent<BoxCollider>();
            meshMat = transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial;
        }
        catch (Exception e)
        {
            throw;
        }

        if (loopType == LoopType.Pinpong)
        {
            isRevert = !isRevert;
            try
            {
                transform.position = (Vector3)iTweenPath.GetPath(enemyType.ToString()).GetValue(0);
                iTween.MoveTo(gameObject, iTween.Hash(
                    "path", iTweenPath.GetPath(enemyType.ToString()),
                    "delay", stayTime,
                    "easetype", easeType,
                    "looptype", iTween.LoopType.none,
                    "speed", speed,
                    "orienttopath", true,
                    "lookTime", 1.1,
                    "axis", "y",
                    "oncomplete", "myCompleteFun",
                    "oncompletetarget", gameObject));
            }
            catch (Exception)
            {
               
            } 

        }
        else
        {
            try
            {
                transform.position = (Vector3)iTweenPath.GetPath(enemyType.ToString()).GetValue(0);
                iTween.MoveTo(gameObject, iTween.Hash(
                    "path", iTweenPath.GetPath(enemyType.ToString()),
                    "delay", stayTime,
                    "easetype", easeType,
                    "looptype", loopType.ToString().ToLower(),
                    "speed", speed,
                    "orienttopath", true,
                    "lookTime", 1.1,
                    "axis", "y"));
            }
            catch (Exception)
            {


            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCollider();
        UpgradeDetect(isStartUpgrade);
        ChangeColor(isStartUpgrade);
        if (fanControl.checkFan(transform, player.transform))
        {
            //第一次检测到
            if (detectNum == 0)
            {
                questionMark.SetActive(true);
                
                StartCoroutine(GetRest(restTime, questionMark));
            }
            else if (detectNum == 1)
            {
                exclamationMark.SetActive(true);
                LookAtCam(exclamationMark.transform);
                if (gameMode == GameMode.Normal)
                {
                    GameManager.Instance.GameOver();
                }
                else
                {
                    TeachGameManager.Instance.GameOver();
                }

            }
        }
        if (questionMark.activeSelf == true)
        {
            LookAtCam(questionMark.transform);
        }
        if (exclamationMark.activeSelf == true)
        {
            LookAtCam(exclamationMark.transform);
        }

    }

    IEnumerator GetRest(float restTime,GameObject mark)
    {
        yield return new WaitForSeconds(restTime);
        detectNum = 1;
        mark.SetActive(false);
        if (!haveupgrade)
        {
            isStartUpgrade = true;
            haveupgrade = !haveupgrade;
        }
    }

    private void UpgradeDetect(bool isStart)
    {
        if(isStart)        
            transform.Find("check").localScale = Vector3.Lerp(transform.Find("check").localScale, upgradeTrans, animTime * Time.deltaTime);
    }

    private void ChangeColor(bool isStart)
    {
        if(isStart)
            transform.Find("check").GetComponent<MeshRenderer>().material.SetVector("_Color", Vector4.Lerp(transform.Find("check").GetComponent<MeshRenderer>().material.GetVector("_Color"), new Vector4(1, 0, 0, 0.5f), Time.deltaTime)); 
    }

    public void myUpdateFunction()
    {
    }

    public void myCompleteFun()
    {
        RevertPath();
    }
    void RevertPath()
    {
        if (isRevert)
        {
            isRevert = !isRevert;
            iTween.MoveTo(gameObject, iTween.Hash(
            "path", iTweenPath.GetPathReversed(enemyType.ToString()),
            "delay", stayTime,
            "easetype", easeType,
            "looptype", loopType,
            "speed", speed,
            "orienttopath", true,
            "lookTime", 1.1,
            "axis", "y",
            "oncomplete", "myCompleteFun",
            "oncompletetarget", gameObject));
        }
        else {
            isRevert = !isRevert;
            iTween.MoveTo(gameObject, iTween.Hash(
            "path", iTweenPath.GetPath(enemyType.ToString()),
            "delay", stayTime,
            "easetype", easeType,
            "looptype", loopType,
            "speed", speed,
            "orienttopath", true,
            "lookTime", 1.1,
            "axis", "y",
            "oncomplete", "myCompleteFun",
            "oncompletetarget", gameObject));
        }
    }
    public void LookAtCam(Transform target)
    {
        if (UsePosition)
        {
            Vector3 vDir = cam.transform.position - target.position;
            vDir.Normalize();
            target.rotation = Quaternion.LookRotation(-vDir);
        }
        else
        {
            target.rotation = cam.transform.rotation;
        }
    }
 
    void UpdateCollider(float threShold = 0.1f)
    {
        if (meshMat && boxCollider)
        {
            if (meshMat.GetColor("_Color").a > threShold)
            {
                boxCollider.enabled = true;
            }
            else
            {
                boxCollider.enabled = false;
            }
        }
    }
}
