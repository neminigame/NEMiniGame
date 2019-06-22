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
        if (GlobalManager.Instance.preSceneNum == 1)
        {
            GlobalManager.Instance.ChangeScene(1);
        }
        else
        {
            if (MenuButtons.activeSelf)
            {
                MenuButtons.SetActive(false);
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