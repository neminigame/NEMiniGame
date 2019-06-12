using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class PageView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

    private ScrollRect rect;                        //滑动组件  
    private float targethorizontal = 0;             //滑动的起始坐标  
    private bool isDrag = false;                    //是否拖拽结束  
    private List<float> posList = new List<float>();            //求出每页的临界角，页索引从0开始  
    private int currentPageIndex = -1;
    public Action<int> OnPageChanged;
    public RectTransform content;
    private bool stopMove = true;
    public float smooting = 4;      //滑动速度  
    public float sensitivity = 0;
    private float startTime;

    private float startDragHorizontal;
    public Transform toggleList;

    void Start()
    {
        rect = transform.GetComponent<ScrollRect>();
        var _rectWidth = GetComponent<RectTransform>();
        var tempWidth = ((float)content.transform.childCount * _rectWidth.rect.width);  //获取图片数量*scrollerview的宽度
        content.sizeDelta = new Vector2(tempWidth, _rectWidth.rect.height); //图片展示区域的长宽
     
        float horizontalLength = content.rect.width - _rectWidth.rect.width;   //content未显示的长度
        for (int i = 0; i < rect.content.transform.childCount; i++)   //循环图片的个数 
        {
            posList.Add(_rectWidth.rect.width * i / horizontalLength); //每个图片百分比
        }
    }

    void Update()
    {
        if (!isDrag && !stopMove)
        {
            startTime += Time.deltaTime;
            float t = startTime * smooting;
            rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targethorizontal, t);
            if (t >= 1)
                stopMove = true;
        }
        //Debug.Log(rect.horizontalNormalizedPosition);
    }

    public void pageTo(int index)
    {
        if (index >= 0 && index < posList.Count)
        {
            rect.horizontalNormalizedPosition = posList[index];
            SetPageIndex(index);
            GetIndex(index);
        }
    }
    private void SetPageIndex(int index)
    {
        if (currentPageIndex != index)
        {
            currentPageIndex = index;
            if (OnPageChanged != null)
                OnPageChanged(index);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        //开始拖动
        startDragHorizontal = rect.horizontalNormalizedPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float posX = rect.horizontalNormalizedPosition;
        posX += ((posX - startDragHorizontal) * sensitivity);
        posX = posX < 1 ? posX : 1;
        posX = posX > 0 ? posX : 0;
        int index = 0;

        float offset = Mathf.Abs(posList[index] - posX);
        //Debug.Log("offset " + offset);


        for (int i = 1; i < posList.Count; i++)
        {
            float temp = Mathf.Abs(posList[i] - posX);
            //Debug.Log("temp " + temp);
            //Debug.Log("i" + i);
            if (temp < offset)
            {
                index = i;
                offset = temp;
            }
            //Debug.Log("index " + index);
        }
        //Debug.Log(index);
        changeToggle(index);
        SetPageIndex(index);
        GetIndex(index);
        targethorizontal = posList[index]; //设置当前坐标，更新函数进行插值  
        isDrag = false;
        startTime = 0;
        stopMove = false;

    }

    public void GetIndex(int index)
    {
        var toogle = toggleList.GetChild(index).GetComponent<Toggle>();
        toogle.isOn = true;
    }

    public void changeToggle(int index)
    {
        for (int i=0;i<toggleList.childCount;i++)
            if(i!=index)
            {
                var toogle = toggleList.GetChild(i).GetComponent<Toggle>();
                toogle.isOn = false;
            }
    }
}
