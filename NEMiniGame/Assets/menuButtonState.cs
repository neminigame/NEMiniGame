using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuButtonState : MonoBehaviour
{
    [SerializeField]
    private Button Hospital;
    [SerializeField]
    private Button Home;
    [SerializeField]
    private Button School;
    [SerializeField]
    private Button Company;
  
    // Start is called before the first frame update
    void Start()
    {
        Hospital.interactable = false;
        Home.interactable = false;
        School.interactable = false;
        Company.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region 随时获取json，更新按钮状态
        Home.interactable = true;
        School.interactable = true;
        Company.interactable = true;
        Hospital.interactable = true;
        Home.interactable = JsonProcess.requestJson().teach_state;
        School.interactable = JsonProcess.requestJson().home_state;
        Company.interactable = JsonProcess.requestJson().school_state;
        Hospital.interactable = JsonProcess.requestJson().company_state;
        #endregion
    }
}
