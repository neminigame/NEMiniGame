using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public enum IsCountOK {OK,NOTOK };

    private void Awake()
    {
        _hour = 0;
        _minute = 0;
        _second = 0;
        _mileSecond = 000;
    }
    void Start()
    {
        _time = 0;
        _Count = IsCountOK.NOTOK;
      //  reStartCountDown();
    }

    void Update()
    {
        if(_Count==IsCountOK.OK)
        {
           
            _time += Time.deltaTime;
            _hour = (int)(_time / 3600);
            _minute = (int)((_time - _hour * 3600) / 60);
            _second = (int)(_time - _hour * 3600 - _minute * 60);
            _mileSecond = (int)((_time - (int)_time) * 1000);
            TimeCountDown.text = string.Format("{0:D2}:{1:D2}.{2:D3}", _minute, _second, _mileSecond);
        }
     
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
      
        
        _hour = 0;
        _minute = 0;
        _second = 0;
        _mileSecond = 000;
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
}
