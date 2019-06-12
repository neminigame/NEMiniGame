using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachGameManager : GameManagerBase
{
    public static TeachGameManager Instance;
    //public CinemachineFreeLook cf;
    public GameObject Identifer;
    public GameObject TeachingRobot;
    public int hitIdentiferNum = 0;
    //float yAccelTime, yDecelTime, xAccelTime, xDecelTime, yMaxSpeed, xMaxSpeed;
    //float yAccelTimeAfter, yDecelTimeAfter, xAccelTimeAfter, xDecelTimeAfter, yMaxSpeedAfter, xMaxSpeedAfter;
    //float _timeScale = 0.1f;
    public void Awake()
    {
        Instance = this;
    }

    public override void GameOver()
    {
        Debug.Log("gameover");
    }
    public override void Win()
    {
        Debug.Log("win");
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
    }


    // Update is called once per frame
    void Update()
    {
        CMMouseOption();
        isFinished = judgeItem(_playerControl.Items);
    }

    void Restart()
    {

    }
}
