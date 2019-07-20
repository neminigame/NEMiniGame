using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class TipsManager : MonoBehaviour
{
 
    public List<GameObject> tips;
    public GameObject nowtips;//目前显示的指示箭头
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            tips.Add(transform.GetChild(i).gameObject);
        }
        SetTip(1);
    }
    private void Update()
    {
        
    }
    public void SetTip(int num)
    {
        if(num>0)
        {
            ResetTip();
            nowtips = tips[num - 1];
            nowtips.SetActive(true);
            nowtips.transform.DOScale(1.1f, 0.6f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InExpo).SetUpdate(true);
        }

    }
    void ResetTip()
    {
        foreach(var i in tips)
        {
            i.SetActive(false);
        }
    }
}
