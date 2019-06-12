using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string ItemName;
    public string description;
    public Sprite ItemImage;
    private PlayerControl _playerControl;

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
        }
    }
    public void BeTaken()
    {
        UIManager.Instance.ChangeVal(UIManager.Instance.currentID, ItemImage,1f);
        UIManager.Instance.currentID ++;
        UIManager.Instance.images.Add(ItemImage);
    }
}
