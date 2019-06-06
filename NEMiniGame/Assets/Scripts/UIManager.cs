using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Transform boxsLayout;
    public List<Sprite> images;
    public int currentID { get{ return _currentID; } set{_currentID = value;} }
    private int _currentID;
    private int totalItems;
    private bool isFinished;//是否收集齐物品

    private void Awake()
    {
        boxsLayout = transform.Find("BoxsLayout");
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeVal();
        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentID == totalItems)
        {
            isFinished = true;
            GameManager.Instance.isFinished = true;
        }
    }
    public void InitializeVal()
    {
        isFinished = false;
        _currentID = 0;
        for (int i = 0; i < boxsLayout.childCount; i++)
        {
            ChangeVal(i, null, 0);
        }
    }
    public void ChangeVal(int i, Sprite img, float alpha)
    {
        if (i < boxsLayout.childCount)
        {
            boxsLayout.GetChild(i).GetComponent<Image>().sprite = img;
            boxsLayout.GetChild(i).GetComponent<Image>().color = new Vector4(boxsLayout.GetChild(i).GetComponent<Image>().color.r,
                                                                                boxsLayout.GetChild(i).GetComponent<Image>().color.g,
                                                                                boxsLayout.GetChild(i).GetComponent<Image>().color.b,
                                                                                alpha);
        }
    }
    public void ChangeVal(int i, Sprite img)
    {
        if (i < boxsLayout.childCount)
        {
            boxsLayout.GetChild(i).GetComponent<Image>().sprite = img;
        }
    }
    public bool detectCurrentID(int val)
    {
        if (currentID == val)
            return true;
        else
            return false;
    }
    public void SetDieUI()
    {
    
    }
}
