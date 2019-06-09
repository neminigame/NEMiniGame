using System;
using UnityEngine;
using UnityEngine.UI;

public class System_Time : MonoBehaviour
{
    [SerializeField] private Text timeText; 
    void Start()
    {
        timeText = GetComponent<Text>();
    }

    void Update()
    {
       
        timeText.text = DateTime.Now.ToString(("yyyy年MM月dd日HH:mm:ss"));
    }



}
