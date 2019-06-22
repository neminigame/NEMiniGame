using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
public class GameManager : GameManagerBase
{
    public static GameManager Instance;
    public GameObject gameOver;
    public GameObject Cam;
    //private bool gameover;//判断游戏是否完成的标记

    //[SerializeField]
    //protected PlayerControl _playerControl;
    private Material _textMeshProUGUIMat;
    //public CinemachineFreeLook cf;
    //float yAccelTime, yDecelTime, xAccelTime, xDecelTime, yMaxSpeed, xMaxSpeed;
    //float yAccelTimeAfter, yDecelTimeAfter, xAccelTimeAfter, xDecelTimeAfter, yMaxSpeedAfter, xMaxSpeedAfter;
    //float _timeScale = 0.1f;

    //回到门时的动画
    //public bool isFinished;//是否收集齐
    //public PlayableDirector backDirector;
    //public int totalItems;
    //开始时的动画，判断是否执行小球控制
    //public PlayableDirector startDirector;

    //public bool isPause=false;//判断游戏是否暂停标记
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
        cf.m_YAxis.m_InputAxisName = "";
        cf.m_XAxis.m_InputAxisName = "";
        Initial();
        InitialCM();
        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
    }

    // Update is called once per frame
    void Update()
    {
        CMMouseOption();
       isFinished = judgeItem(_playerControl.Items);
        //开始时的动画，判断是否执行小球控制
        if (startDirector.state == PlayState.Playing)
            _playerControl.enabled = false;
        else if(!gameover)_playerControl.enabled = true;
        //
    }

    public override void GameOver()
    {
        //gameOver.SetActive(true);
        gameover = true;
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
    public override void Win()
    {
        
        backDirector.enabled = true;
        gameover = true;
        _playerControl.enabled = false;
        CountDown._Count = CountDown.IsCountOK.NOTOK;
        // GameOver();
        //关闭过渡摄像机
        Cam.SetActive(false);
    }
}
