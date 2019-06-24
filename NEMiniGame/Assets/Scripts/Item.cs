﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    public string ItemName;
    public string description;
    public Sprite ItemImage;
    private PlayerControl _playerControl;

    public string bubbleText;//气泡框内文本
    
    private void Start()
    {
        _playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }
    public enum ItemType {
        GiftMoney,
        GameBoy,
        Battery
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            gameObject.SetActive(false);
            _playerControl.Items.Add(this);
            BeTaken();
            if (TeachGameManager.Instance)
            {
                TeachGameManager.Instance.ShowHint(new List<string>{ "拿全道具后返回门处，即可通过本关","依然注意不要碰到敌人"});
            }
            if(UIManager.Instance)
            {
                UIManager.Instance.SetBubbleUI(bubbleText);
            }
        }
    }
    public void BeTaken()
    {
        UIManager.Instance.ChangeVal(UIManager.Instance.currentID, ItemImage,1f);
        UIManager.Instance.currentID ++;
        UIManager.Instance.images.Add(ItemImage);
    }
}
