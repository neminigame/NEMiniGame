using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SecUIMenu : MonoBehaviour
{
    [SerializeField] private GameObject MenuButtons;
    [SerializeField] private GameObject LevelButtons;
    [SerializeField] private GameObject StartButtons;
    public GameObject startMovie;

    public void Awake()
    {
        MenuButtons.SetActive(true);
        LevelButtons.SetActive(false);
    }
    public void SwitchLevel()
    {
        if(MenuButtons.activeSelf)
        {
            MenuButtons.SetActive(false);
            LevelButtons.SetActive(true);
        }
        else
        {
            MenuButtons.SetActive(true);
            LevelButtons.SetActive(false);
        }
    }
    public void SwitchStart()
    {
        //如果之前的关卡都是第一关，那么还进入第一关
        if (GlobalManager.Instance.preSceneNum == 1)
        {
            GlobalManager.Instance.ChangeScene(1);
        }
        else
        {
            if (MenuButtons.activeSelf)
            {
                MenuButtons.SetActive(false);
                GlobalManager.Instance.audioSource.mute=true;
                // StartButtons.SetActive(true);
                startMovie.SetActive(true);
            }
            else
            {
                MenuButtons.SetActive(true);
                //StartButtons.SetActive(false);
            }
        }
    }
}