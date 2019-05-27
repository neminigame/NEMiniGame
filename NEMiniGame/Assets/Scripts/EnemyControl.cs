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
        Dog
    }
    public enum LoopType
    {
        None,
        Loop,
        Pinpong
    }
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
    public float upgradeScale;
    public bool UsePosition = false;
    private Camera cam;
    private Vector3 upgradeTrans;
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
        exclamationMark = transform.Find("ExclamationMark").gameObject;
        if (loopType == LoopType.Pinpong)
        {
            isRevert = !isRevert;
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
        else
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
        
    }

    // Update is called once per frame
    void Update()
    {
        UpgradeDetect(isStartUpgrade);
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
                GameManager.Instance.GameOver();
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAlive = false;
            Destroy(this.gameObject);
        }
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
}
