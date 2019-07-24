using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManagerBase : MonoBehaviour
{
    public CinemachineFreeLook cf;
    public bool isFinished=false;//是否能够通关
    public int totalItems;
    public bool isPause = false;//判断游戏是否暂停标记
    public PlayableDirector backDirector;
    //开始时的动画，判断是否执行小球控制
    public PlayableDirector startDirector;
    [SerializeField]
    protected Glitch glitch;
    protected bool gameover;//判断游戏是否完成的标记
    [SerializeField]
    public PlayerControl _playerControl;
    float yAccelTime, yDecelTime, xAccelTime, xDecelTime, yMaxSpeed, xMaxSpeed;
    float yAccelTimeAfter, yDecelTimeAfter, xAccelTimeAfter, xDecelTimeAfter, yMaxSpeedAfter, xMaxSpeedAfter;
    float _timeScale = 0.1f;
    Image gradientImage;
    Text tipText;
    public virtual bool judgeItem(List<Item> item) //判断数量
    {
        if (item.Count >= totalItems)
            return true;
        return false;
    }
    public virtual void GameOver()
    {
        Time.timeScale = 1;

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
    IEnumerator IShowGlitchAndOver(float time1=.3f,float time2=7f)
    {
        glitch.enabled = true;
        ShowBlackAndRestart(2f,5f);
        yield return new WaitForSeconds(time1);
        glitch.enabled = false;
        yield return new WaitForSeconds(time2);
        GlobalManager.Instance.ChangeScene(0);
    }
    IEnumerator IShowGlitch(float time1 = .3f)
    {
        glitch.enabled = true;
        ShowBlackAndRestart(2f, 5f);
        yield return new WaitForSeconds(time1);
        glitch.enabled = false;
    }
    public void showGlitch(float time1 = .3f)
    {
        StartCoroutine(IShowGlitch(time1));
    }
    public void showGlitchAndOver(float time1 = .3f, float time2 = 2f)
    {
        StartCoroutine(IShowGlitchAndOver(time1,time2));
    }
    void ShowBlackAndRestart(float time1 = 2f, float time2 = 5f)
    {
        if (gradientImage)
        {
            gradientImage.gameObject.SetActive(true);
            gradientImage.color = Color.clear;
            gradientImage.DOColor(Color.black, time1);
        }
        if (tipText)
        {
            tipText.gameObject.SetActive(true);
            tipText.color = Color.clear;
            tipText.DOColor(Color.white, time1);
            tipText.DOText("系统参数错误，正在重启……", time2);
        }
    }
    public virtual void Initial()
    {
        try
        {
            gradientImage = UIManager.Instance.transform.Find("GradientImage").GetComponent<Image>();
            tipText = UIManager.Instance.transform.Find("TipText").GetComponent<Text>();
            gradientImage.gameObject.SetActive(false);
            tipText.gameObject.SetActive(false);
        }
        catch (System.Exception)
        {
        }

    }
}
