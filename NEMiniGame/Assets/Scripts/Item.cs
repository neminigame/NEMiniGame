using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    public string ItemName;
    public string description;
    public Sprite ItemImage;
    private PlayerControl _playerControl;
    public bool canBeTakenByPlayer = true;

    public string bubbleText;//气泡框内文本
    public bool isGround = true;
    private TipsManager tipsManager;
    public int tipnum;//获得物品后加载下一个提示编号
    private void Start()
    {
        isGround = true;
        canBeTakenByPlayer = true;
        _playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        try
        {
            tipsManager = GameObject.Find("tips").GetComponent<TipsManager>();
        }
        catch (System.Exception e)
        {

            Debug.Log("*LOG*<color=red>" + e + "</color>");
        } 
    }
    public enum ItemType {
        GiftMoney,
        GameBoy,
        Battery
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player"&& canBeTakenByPlayer)
        {
            gameObject.SetActive(false);
            _playerControl.Items.Add(this);
            BeTaken();
            if(tipsManager!=null)
            {
                tipsManager.SetTip(tipnum);
            }
            if (TeachGameManager.Instance)
            {
                TeachGameManager.Instance.ShowHint(new List<string>{ "拿全道具后返回门处，即可通过本关","依然注意不要碰到敌人"});
            }
            if(UIManager.Instance)
            {
                UIManager.Instance.SetBubbleUI(bubbleText);
            }
        }
        if (!canBeTakenByPlayer && collision.transform.tag == "wall")
        {
            isGround = true;
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
    public void BeTaken()
    {
        UIManager.Instance.ChangeVal(UIManager.Instance.currentID, ItemImage,1f);
        UIManager.Instance.currentID ++;
        UIManager.Instance.images.Add(ItemImage);
    }
}
