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
    [SerializeField]
    private Button home;
    [SerializeField]
    private Button school;
    [SerializeField]
    private Button company;
    [SerializeField]
    private Button hospital;
    [SerializeField]
    private GameObject BackInView; //列表界面的返回按钮
    [SerializeField]
    private GameObject mainPanel; //选择界面
    public List<GameObject> memetoLists;
    void Start()
    {
        pageNumber.text = string.Format("当前页码：0");
        pageView.OnPageChanged = pageChanged;
        setButtonState(home, false);
        setButtonState(school, false);
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
    void setButtonState(Button button,bool state) //设置按钮的状态
    {
        button.interactable = state;
    }
    //对应的view激活，其他的隐藏
    public void UnlockView(GameObject correspoingView) 
    {
        mainPanel.SetActive(false);//主界面关闭
        foreach (var view in memetoLists) //其他view关闭
        {
            if (view.name != correspoingView.name)
                view.SetActive(false);
        }
        BackInView.SetActive(true); //返回按钮激活
        correspoingView.SetActive(true); //对应view激活
        
    }
   public void GoBackToButtonList()
    {
        foreach (var view in memetoLists) //关闭所有view
        {
                view.SetActive(false);
        }
        BackInView.SetActive(false);//关闭返回按钮
        mainPanel.SetActive(true);//开启菜单
    }

    private void Update()
    {
        #region 随时获取json，更新按钮状态
        setButtonState(company, JsonProcess.requestJson().company_state);
        setButtonState(school, JsonProcess.requestJson().school_state);
        setButtonState(home, JsonProcess.requestJson().home_state);
        setButtonState(hospital, JsonProcess.requestJson().hospital_state);
        #endregion
    }
}
