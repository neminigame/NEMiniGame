using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour
{
    [SerializeField]
    private Text TimeCountDown;
    public int TimeTotal=10;
    private int _time;
    void Start()
    {
        reStartCountDown();
    }

    void Update()
    {

    }

    private IEnumerator CountDownT()
    {
        _time = int.Parse(TimeCountDown.text);
        while (_time > 0)
        {
            yield return new WaitForSeconds(1);
            _time -= 1;
            TimeCountDown.text = _time + "";
        }
    }
    public void CountStop()
    {
        StopCoroutine("CountDownT");
    }
    public void reStartCountDown()
    {
        TimeCountDown.text = TimeTotal.ToString();
        StartCoroutine("CountDownT");
    }
}
