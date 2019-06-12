using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBase : MonoBehaviour
{
    public CinemachineFreeLook cf;
    public bool isFinished;//是否能够通关
    public int totalItems;
    [SerializeField]
    protected PlayerControl _playerControl;
    float yAccelTime, yDecelTime, xAccelTime, xDecelTime, yMaxSpeed, xMaxSpeed;
    float yAccelTimeAfter, yDecelTimeAfter, xAccelTimeAfter, xDecelTimeAfter, yMaxSpeedAfter, xMaxSpeedAfter;
    float _timeScale = 0.1f;
    public virtual bool judgeItem(List<Item> item)
    {
        if (item.Count >= totalItems)
            return true;
        return false;
    }
    public virtual void GameOver()
    {

    
    }
    public virtual void Win()
    {

    }
    public void InitialCM()
    {
        yAccelTime = cf.m_YAxis.m_AccelTime;
        yDecelTime = cf.m_YAxis.m_DecelTime;
        xAccelTime = cf.m_XAxis.m_AccelTime;
        xDecelTime = cf.m_XAxis.m_DecelTime;
        yMaxSpeed = cf.m_YAxis.m_MaxSpeed;
        xMaxSpeed = cf.m_XAxis.m_MaxSpeed;
        yAccelTimeAfter = cf.m_YAxis.m_AccelTime * _timeScale;
        yDecelTimeAfter = cf.m_YAxis.m_DecelTime * _timeScale;
        xAccelTimeAfter = cf.m_XAxis.m_AccelTime * _timeScale;
        xDecelTimeAfter = cf.m_XAxis.m_DecelTime * _timeScale;
        yMaxSpeedAfter = cf.m_YAxis.m_MaxSpeed / _timeScale;
        xMaxSpeedAfter = cf.m_XAxis.m_MaxSpeed / _timeScale;
    }
    //每帧调用的虚拟相机设置
    public void CMMouseOption()
    {
        if (Input.GetMouseButtonDown(1))
        {
            cf.m_YAxis.m_InputAxisName = "Mouse Y";
            cf.m_XAxis.m_InputAxisName = "Mouse X";
        }
        else if (Input.GetMouseButtonUp(1))
        {
            cf.m_YAxis.m_InputAxisName = "";
            cf.m_XAxis.m_InputAxisName = "";
            cf.m_YAxis.m_InputAxisValue = 0;
            cf.m_XAxis.m_InputAxisValue = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            cf.m_YAxis.m_AccelTime = yAccelTimeAfter;
            cf.m_YAxis.m_DecelTime = yDecelTimeAfter;
            cf.m_XAxis.m_AccelTime = xAccelTimeAfter;
            cf.m_XAxis.m_DecelTime = xDecelTimeAfter;
            cf.m_YAxis.m_MaxSpeed = yMaxSpeedAfter;
            cf.m_XAxis.m_MaxSpeed = xMaxSpeedAfter;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cf.m_YAxis.m_AccelTime = yAccelTime;
            cf.m_YAxis.m_DecelTime = yDecelTime;
            cf.m_XAxis.m_AccelTime = xAccelTime;
            cf.m_XAxis.m_DecelTime = xDecelTime;
            cf.m_YAxis.m_MaxSpeed = yMaxSpeed;
            cf.m_XAxis.m_MaxSpeed = xMaxSpeed;
        }
    }
}
