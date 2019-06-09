using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOver;
    public float speed;
   
    [SerializeField]
    private PlayerControl _playerControl;
    private Material _textMeshProUGUIMat;
    public CinemachineFreeLook cf;
    float yAccelTime, yDecelTime, xAccelTime, xDecelTime, yMaxSpeed, xMaxSpeed;
    float yAccelTimeAfter, yDecelTimeAfter, xAccelTimeAfter, xDecelTimeAfter, yMaxSpeedAfter, xMaxSpeedAfter;
    float _timeScale = 0.1f;

    //回到门时的动画
    public bool isFinished;//是否收集齐
    public PlayableDirector backDirector;
    [SerializeField]
    private int totalItems;
    void Awake()
    {
        Instance = this;
        _textMeshProUGUIMat = gameOver.GetComponent<TextMeshProUGUI>().fontMaterial;

        _playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        InitialGameMang();
        //_countDown = transform.Find("Canvas").GetComponent<CountDown>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(gameOver.GetComponent<TextMeshProUGUI>().fontMaterial);
        InitialCM();
        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
    }

    // Update is called once per frame
    void Update()
    {
        CMMouseOption();
       isFinished= judgeItem(_playerControl.Items);
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        _playerControl.enabled = false;
        CountDown._Count = CountDown.IsCountOK.NOTOK;
        //_textMeshProUGUIMat.SetFloat("_OutlineSoftness", Mathf.Lerp(_textMeshProUGUIMat.GetFloat("_OutlineSoftness"), 0f, Time.deltaTime * speed));
        //_textMeshProUGUIMat.SetFloat("_FaceDilate", Mathf.Lerp(_textMeshProUGUIMat.GetFloat("_FaceDilate"), 0f, Time.deltaTime * speed));
    }
    public void InitialGameMang()
    {
        gameOver.SetActive(false);
        // _textMeshProUGUIMat.SetFloat("_OutlineSoftness", 1f);
        //  _textMeshProUGUIMat.SetFloat("_FaceDilate", -1f);
        isFinished = false;
    }
    public void Win()
    {
        backDirector.enabled = true;
       // GameOver();
    }
    //存储虚拟相机移动速度的初始值和改变后的值
    void InitialCM()
    {
        yAccelTime = cf.m_YAxis.m_AccelTime;
        yDecelTime = cf.m_YAxis.m_DecelTime;
        xAccelTime = cf.m_XAxis.m_AccelTime;
        xDecelTime = cf.m_XAxis.m_DecelTime;
        yMaxSpeed = cf.m_YAxis.m_MaxSpeed;
        xMaxSpeed = cf.m_XAxis.m_MaxSpeed;
        yAccelTimeAfter = cf.m_YAxis.m_AccelTime * _timeScale;
        yDecelTimeAfter = cf.m_YAxis.m_DecelTime * _timeScale;
        xAccelTimeAfter = cf.m_XAxis.m_AccelTime * _timeScale;
        xDecelTimeAfter = cf.m_XAxis.m_DecelTime * _timeScale;
        yMaxSpeedAfter = cf.m_YAxis.m_MaxSpeed / _timeScale;
        xMaxSpeedAfter = cf.m_XAxis.m_MaxSpeed / _timeScale;
    }
    //每帧调用的虚拟相机设置
    void CMMouseOption()
    {
        if (Input.GetMouseButton(1))
        {
            cf.m_YAxis.m_InputAxisName = "Mouse Y";
            cf.m_XAxis.m_InputAxisName = "Mouse X";
            //cf.m_YAxis.m_AccelTime = a;
            //cf.m_YAxis.m_DecelTime = b;
            //cf.m_XAxis.m_AccelTime = c;
            //cf.m_XAxis.m_DecelTime = d;
            if (Input.GetMouseButton(0))
            {
                cf.m_YAxis.m_AccelTime = yAccelTimeAfter;
                cf.m_YAxis.m_DecelTime = yDecelTimeAfter;
                cf.m_XAxis.m_AccelTime = xAccelTimeAfter;
                cf.m_XAxis.m_DecelTime = xDecelTimeAfter;
                cf.m_YAxis.m_MaxSpeed = yMaxSpeedAfter;
                cf.m_XAxis.m_MaxSpeed = xMaxSpeedAfter;
            }
            else
            {

            }
        }
        else
        {
            cf.m_YAxis.m_InputAxisName = "";
            cf.m_XAxis.m_InputAxisName = "";
        }
    }
    bool judgeItem(List<Item> item)
    {
        if (item.Count >= totalItems)
            return true;
        return false;
    }
}
