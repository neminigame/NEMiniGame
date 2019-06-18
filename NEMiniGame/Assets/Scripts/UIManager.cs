using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    private void Awake()
    {
        boxsLayout = transform.Find("BoxsLayout");
        scanline = Camera.main.GetComponent<ScanningLine>();
        Instance = this;
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
        StartCoroutine(EndUIscale(EndUI_point.gameObject, EndUI_point.transform.localScale.x, 0, 0.7f));
       
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
        scene.reset(3);
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
        scene.reset(0);
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
        scene.reset();
    }
}
