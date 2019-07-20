using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using DG.Tweening;

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
    public TeachingBrain teachBrain;
    public float waitTime = 2f;
    public bool WorkWithoutGameManager = true;
    [SerializeField]
    private Image tipImage;
    [SerializeField]
    private Button tipBtn;
    private Text tipBtnText;
    private float startTIme;
    private bool canPopTip=false;
    //float yAccelTime, yDecelTime, xAccelTime, xDecelTime, yMaxSpeed, xMaxSpeed;
    //float yAccelTimeAfter, yDecelTimeAfter, xAccelTimeAfter, xDecelTimeAfter, yMaxSpeedAfter, xMaxSpeedAfter;
    //float _timeScale = 0.1f;
    public void Awake()
    {
        Instance = this;
        _playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        if (WorkWithoutGameManager) tipBtnText = tipBtn.transform.Find("Text").GetComponent<Text>();
        canPopTip = false;
    }

    public override void GameOver()
    {
        base.GameOver();
        showGlitch(2.0f);
        Restart();
    }

    public override void Win()
    {
        backDirector.enabled = true;
        gameover = true;
        CountDown._Count = CountDown.IsCountOK.NOTOK;
        _playerControl.enabled = false;
        BlendCam.SetActive(false);
        camToHide.SetActive(true);
    }
    public void SetTeachingRobot(bool val,int routID)
    {
        TeachingRobot.SetActive(val);
        TeachingRobot.transform.position = teachBrain.posCollections[routID][0].position;
    }
    // Start is called before the first frame update
    void Start()
    {
        startTIme = Time.time;
        if (WorkWithoutGameManager)
        {
            cf.m_YAxis.m_InputAxisName = "";
            cf.m_XAxis.m_InputAxisName = "";
            base.InitialCM();
            SetTeachingRobot(false, 0);
        }
        else
        {
            SetTeachingRobot(true, 0);
        }

        //StartCoroutine(HideCam());
    }


    // Update is called once per frame
    void Update()
    {

        if (WorkWithoutGameManager)
        {
            CMMouseOption();
            isFinished = judgeItem(_playerControl.Items);
            //开始时的动画，判断是否执行小球控制
            if (startDirector.state == PlayState.Playing)
                _playerControl.enabled = false;
            else if (!gameover) _playerControl.enabled = true;
            if (Time.time - startTIme <= waitTime)
            {
                _playerControl.enabled = false;
            }
            //弹出提示
            else if (!canPopTip)
            {
                tipImage.gameObject.SetActive(true);
                tipBtn.gameObject.SetActive(true);
                tipImage.DOColor(Color.white, 1.5f);
                tipBtn.GetComponent<Image>().DOColor(Color.white, 1.5f);
                tipBtnText.DOColor(Color.black, 1.5f);
                canPopTip = true;
            }
        }
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
    public void HideTip()
    {
        tipImage.color = Color.clear;
        tipImage.gameObject.SetActive(false);
        tipBtn.GetComponent<Image>().color = Color.clear;
        tipBtn.gameObject.SetActive(false);
        tipBtnText.color = Color.clear;
        tipBtnText.gameObject.SetActive(false);
        _playerControl.enabled = true;
        #region 开始三屏字
        List<string> listText = new List<string>();
        listText.Add("不断按住鼠标左键进入子弹时间，反弹小球进行前进,按住右键调整视野");
        listText.Add("进入敌人扫描范围会引起警觉，数秒后在此被发现则通关失败");
        listText.Add("碰撞到敌人立即失败");
        ShowHint(listText, 3f);
        #endregion
    }

}
