using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponent<Text>();
        text.DOText("AABBCCDDEEFFGG赵钱孙李周吴郑王冯陈楚卫",5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
