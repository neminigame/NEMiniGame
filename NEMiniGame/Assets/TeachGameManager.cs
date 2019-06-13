using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class TeachGameManager : GameManagerBase
{
    public static TeachGameManager Instance;
    //public CinemachineFreeLook cf;
    public GameObject Identifer1;
    public GameObject Identifer2;
    public GameObject TeachingRobot;
    public int hitIdentiferNum = 0;
    public GameObject Hint;
    public GameObject BlendCam;
    public GameObject camToHide;
    //float yAccelTime, yDecelTime, xAccelTime, xDecelTime, yMaxSpeed, xMaxSpeed;
    //float yAccelTimeAfter, yDecelTimeAfter, xAccelTimeAfter, xDecelTimeAfter, yMaxSpeedAfter, xMaxSpeedAfter;
    //float _timeScale = 0.1f;
    public void Awake()
    {
        Instance = this;
        _playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }

    public override void GameOver()
    {
        Restart();
    }

    public override void Win()
    {
        backDirector.enabled = true;
        gameover = true;
        _playerControl.enabled = false;
        BlendCam.SetActive(false);
        camToHide.SetActive(true);
    }
    public void SetTeachingRobot(bool val)
    {
        TeachingRobot.SetActive(val);
    }
    // Start is called before the first frame update
    void Start()
    {
        cf.m_YAxis.m_InputAxisName = "";
        cf.m_XAxis.m_InputAxisName = "";
        base.InitialCM();
        SetTeachingRobot(false);
        List<string> listText=new List<string>();
        listText.Add("不断按住鼠标左键进入子弹时间，反弹小球进行前进,按住右键调整视野");
        listText.Add("进入敌人扫描范围会引起警觉，数秒后在此被发现则通关失败");
        listText.Add("碰撞到敌人立即失败");
        ShowHint(listText,3f);
        //StartCoroutine(HideCam());
    }


    // Update is called once per frame
    void Update()
    {
        CMMouseOption();
        isFinished = judgeItem(_playerControl.Items);
        //开始时的动画，判断是否执行小球控制
        if (startDirector.state == PlayState.Playing)
            _playerControl.enabled = false;
        else if (!gameover) _playerControl.enabled = true;
        //Time.timeScale = 0f;
    }

    void Restart()
    {
        ShowHint("您已被敌人发现/撞击了敌人，请注意！！！");
    }
    IEnumerator IShowHint(List<string> text, float time = 4f)
    {
        for (int i = 0; i < text.Count; i++)
        {
            Hint.SetActive(true);
            Hint.transform.GetChild(0).GetComponent<Text>().text = text[i];
            yield return new WaitForSeconds(time);
            Hint.SetActive(false);
        }
    }
    IEnumerator IShowHint(string text, float time = 4f)
    {

        Hint.SetActive(true);
        Hint.transform.GetChild(0).GetComponent<Text>().text = text;
        yield return new WaitForSeconds(time);
        Hint.SetActive(false);
        
    }
    public void ShowHint(string text, float time = 4f)
    {
        StartCoroutine(IShowHint(text, time));
    }
    public void ShowHint(List<string> text, float time = 4f)
    {
        StartCoroutine(IShowHint(text, time));
    }
    IEnumerator HideCam(float time = 2)
    {
        yield return new WaitForSeconds(time);
        camToHide.SetActive(false);
    }
}
