using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameMode
{
    Teaching,
    Normal
}
public class CountDown : MonoBehaviour
{
    [SerializeField]
    private Text TimeCountDown;
    private int TimeTotal = 0;
    private int _mileSecond;
    private int _second;
    private int _minute;
    private int _hour;
    private float _time;
    public static IsCountOK _Count;
    private PlayerControl playerControl;
    public enum IsCountOK {OK,NOTOK };
    public GameMode gameMode=GameMode.Normal;
    public GameObject gameoverPanel;

    private bool isGameOver;
    private void Awake()
    {
        
        _hour = 0;
        //ugui上的输入框文本同步
        string[] arrays = TimeCountDown.text.Split(':');
        if(arrays.Length==2)
        {
            float t = float.Parse(arrays[1]);
            _minute = int.Parse(arrays[0]);
            _second = (int)t;
            _mileSecond = (int)((t-_second)*1000);
        }
      
        
    }
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        _time = _second+_mileSecond*1.0f/1000;
        _Count = IsCountOK.NOTOK;
      //  reStartCountDown();
    }

    void Update()
    {
        if(_Count==IsCountOK.OK)
        {
           
            _time -= Time.deltaTime;
            if(_time<=0f)//倒计时结束且当前没有失败，判断为失败
            {
                TimeCountDown.text = string.Format("{0:D2}:{1:D2}.{2:D3}", 0, 0, 0);
                switch (gameMode)
                {
                    case GameMode.Teaching:
                        playerControl.gameManager.GameOver();
                        gameoverPanel.SetActive(true);
                        break;
                    case GameMode.Normal:
                        playerControl.gameManager.GameOver();
                        if (!isGameOver)
                        {
                           playerControl.gameManager.showGlitchAndOver(2.0f, 2.0f);
                            isGameOver = true;
                        }
                        _hour = 0;
                        _minute = 0;
                        _second = 0;
                        _mileSecond = 000;
                        TimeCountDown.text = string.Format("{0:D2}:{1:D2}.{2:D3}", _minute, _second, _mileSecond);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                _hour = (int)(_time / 3600);
                _minute = (int)((_time - _hour * 3600) / 60);
                _second = (int)(_time - _hour * 3600 - _minute * 60);
                _mileSecond = (int)((_time - (int)_time) * 1000);
                TimeCountDown.text = string.Format("{0:D2}:{1:D2}.{2:D3}", _minute, _second, _mileSecond);
            }
        }
        judgeColor(3f, Color.red);//时间3s内变为红色
    }

    //private IEnumerator CountDownT()
    //{
    //    _time = int.Parse(TimeCountDown.text);
    //    while (_time > 0)
    //    {
    //        yield return new WaitForSeconds(1/1000);
    //        _time -= 1;
    //        TimeCountDown.text = _time + "";
    //    }
    //}
  
    public void CountStop()
    {
      
        
        //_hour = 0;
        //_minute = 0;
        //_second = 0;
        //_mileSecond = 000;
        _Count = IsCountOK.NOTOK;
         TimeCountDown.text = string.Format("{0:D2}:{1:D2}.{2:D3}", _minute,_second ,_mileSecond);
    }
    public void reStartCountDown()
    {
        _time = 0;
        _Count = IsCountOK.OK;
    }
    public void TimePause()
    {
        if(_second!=0)
        if(_Count==IsCountOK.OK)
        {
            _Count = IsCountOK.NOTOK;
        }
        else
            _Count = IsCountOK.OK;
    }
    public void judgeColor(float t,Color col)
    {
        if(_second<t)
        {
            TimeCountDown.color = col;
        }
    }
}
