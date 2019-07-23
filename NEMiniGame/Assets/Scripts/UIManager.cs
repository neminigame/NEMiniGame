using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Transform boxsLayout;
    public List<Sprite> images;
    public int currentID { get { return _currentID; } set { _currentID = value; } }
    private int _currentID;

    //private int totalItems;
    private bool isFinished;//是否收集齐物品

    public ScenManager scene;
    //结束时的UI动画
    public Image EndUI_black, EndUI_point;
    //继续时的UI动画
    public ScanningLine scanline;

    public GameObject PauseUI;//暂停时的UI面板

    //气泡框显示
    public GameObject bubble;
    private void Awake()
    {
        boxsLayout = transform.Find("BoxsLayout");
        scanline = Camera.main.GetComponent<ScanningLine>();
        Instance = this;
        bubble = GameObject.Find("Player").transform.Find("bubble").gameObject;

    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeVal();
        //totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentID == totalItems)
        //{
        //    isFinished = true;
        //    GameManager.Instance.isFinished = true;
        //}
    }
    public void InitializeVal()
    {
        isFinished = false;
        _currentID = 0;
        if(SceneManager.GetActiveScene().name!="Scene3"&&SceneManager.GetActiveScene().name != "Scene4")
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
    public void SetEndUI()
    {
        EndUI_black.enabled = true;
        EndUI_point.enabled = true;
    }
    public void SetEndUIInStartTrial()
    {
        EndUI_black.enabled = true;
        EndUI_point.enabled = true;
        StartCoroutine(EndUIscale2(EndUI_point.gameObject, EndUI_point.transform.localScale.x, 0, 0.7f));

    }

    public void SetResetUI()
    {

        StartCoroutine(ResetUI());
    }
    public void SetPauseUI()
    {
        PauseUI.SetActive(true);
    }
    public void ResetPauseUI()
    {
        PauseUI.SetActive(false);
    }
    public void SetBubbleUI(string text)
    {
        if (text.Length > 0)
        {
            //文本输入小写字母t用作换行符号，便于在inspector更改
            bubble.GetComponentInChildren<Text>().text = text.Replace('t', '\n');
            bubble.SetActive(true);
            StopCoroutine("BubbleUiAnim");
            bubble.GetComponentInChildren<CanvasGroup>().alpha = 1f;
            StartCoroutine("BubbleUiAnim",0.8f);
        }
    }
    IEnumerator BubbleUiAnim(float duration)
    {
        float time = 0f;
        CanvasGroup image= bubble.GetComponentInChildren<CanvasGroup>();
        yield return new WaitForSeconds(1.2f);
        while (time<duration)
        {
            time += Time.unscaledDeltaTime;
            image.alpha = Mathf.Lerp(1f, 0f, time / duration);
            yield return null;
        }
        bubble.SetActive(false);
    }
    IEnumerator ResetUI()
    {
        scanline.canPlay = true;
        
        while(scanline.TransitionMat.GetFloat("_Cutoff")<1)
        {
            yield return null;
        }
        scene.reset();
    }
    public void ResetTeach()
    {
        scene.reset(5);
    }
    IEnumerator EndUIscale(GameObject t,float start_scale,float end_scale, float duration)
    {
        float temptime = 0f;
        Vector3 tempscale;
        if (t == null)
        {
            yield return null;
        }
        while (temptime<duration)
        {
            //Debug.Log
            temptime += Time.unscaledDeltaTime;
            tempscale.x = Mathf.Lerp(start_scale, end_scale, temptime / duration);
            tempscale.y = Mathf.Lerp(start_scale, end_scale, temptime / duration);
            tempscale.z= Mathf.Lerp(start_scale, end_scale, temptime / duration);
            t.transform.localScale = tempscale;
            yield return null;
        }

        scene.reset(5);
    }
    IEnumerator EndUIscale2(GameObject t, float start_scale, float end_scale, float duration)
    {
        float temptime = 0f;
        Vector3 tempscale;
        if (t == null)
        {
            yield return null;
        }
        while (temptime < duration)
        {
            //Debug.Log
            temptime += Time.unscaledDeltaTime;
            tempscale.x = Mathf.Lerp(start_scale, end_scale, temptime / duration);
            tempscale.y = Mathf.Lerp(start_scale, end_scale, temptime / duration);
            tempscale.z = Mathf.Lerp(start_scale, end_scale, temptime / duration);
            t.transform.localScale = tempscale;
            yield return null;
        }

        scene.reset(1);
    }
}
