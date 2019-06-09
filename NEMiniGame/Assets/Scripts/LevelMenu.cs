using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject MenuButtons;
    [SerializeField] private GameObject LevelButtons;
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
}