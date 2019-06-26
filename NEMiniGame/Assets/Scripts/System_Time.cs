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
       
        timeText.text = DateTime.Now.ToString(("1999年10月16日HH:mm:ss"));
    }



}
