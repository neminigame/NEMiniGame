using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [SerializeField]
    private Text pageNumber;
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private PageView pageView;
    // Use this for initialization
    void Start()
    {
        pageNumber.text = string.Format("当前页码：0");
        pageView.OnPageChanged = pageChanged;
    }

    void pageChanged(int index)
    {
        pageNumber.text = string.Format("当前页码：{0}", index.ToString());
    }

    public void onClick()
    {
        try
        {
            int idnex = int.Parse(inputField.text);
            pageView.pageTo(idnex);
        }
        catch (Exception ex)
        {
            Debug.LogWarning("请输入数字" + ex.ToString());
        }
    }

    void Destroy()
    {
        pageView.OnPageChanged = null;
    }

}
