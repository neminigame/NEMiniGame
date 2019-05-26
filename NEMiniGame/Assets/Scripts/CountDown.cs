using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour
{
    [SerializeField]
    private Text TimeCountDown;
    void Start()
    {
        TimeCountDown.text = "10";
        StartCoroutine("CountDownT");
    }

    void Update()
    {

    }

    private IEnumerator CountDownT()
    {
        int tempT = int.Parse(TimeCountDown.text);
        while (tempT > 0)
        {
            yield return new WaitForSeconds(1);
            tempT -= 1;
            TimeCountDown.text = tempT + "";
        }


    }
}
