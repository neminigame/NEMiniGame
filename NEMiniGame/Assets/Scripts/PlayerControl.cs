﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using System;

public enum Scene3State
{
    PrePause,
    Pause,
    AfterPause
}
public class PlayerControl : MonoBehaviour
{
    public GameMode gameMode = GameMode.Normal;
    public Camera _camera;
    public GameObject model;
    public Rigidbody rig;
    public Vector3 dir;
    public float speed, rspeed;
    public bool show_line;
    private float _timeScale = 0.1f;
    private float tspeed, trspeed;//前进速度，旋转速度
    private Vector3 mouse1, mouse2;//第一次鼠标，第二次鼠标位置
  //  private int JudgeIsBegin = 0;//判断是否第一次点鼠标，如果是则开始计时
    public Vector3 tdir;
    private Vector3 dropDir;
    public List<Item> Items;
    public bool isTeachingMode = false;
    public line line;
    public float factor = 1f;
    public GameManagerBase gameManager;
    private Vector3 rotate_dir;
    private bool isGameOver = false;
    private AudioSource audioSource;
    private Vector3 iniPos; //小球初始位置
    public PlayableDirector doorAnim;//小球载入动画
    // Start is called before the first frame update
    private float totaltime;
    private bool canCountdown=true;
    private Vector3 tempscaleAnim;
    private int hitIdentify2Times = 0;
    public bool isScene2AnimPlay = false;
    public Scene3State scene3State = Scene3State.PrePause;
    public bool isScene3Pause = false;
    public bool isScene4Pause = false;//是否拿齐物品
    public Scene4Controler scene4Controler = null;
    public bool hasHitIdentifer5 = false;
    public bool Scene3GetItem = false;
    public int Scene3Identifer2Num = 0;
    void Awake()
    {
        scene3State = Scene3State.PrePause;
        // JudgeIsBegin = 0;
        _camera = Camera.main;
        rig = GetComponent<Rigidbody>();
        model = transform.Find("player").gameObject;
        doorAnim = GameObject.Find("doorTimeline").GetComponent<PlayableDirector>();
        print(doorAnim.duration);

        Items.Clear();
        audioSource = GetComponent<AudioSource>();
        tempscaleAnim = transform.localScale;
        if (SceneManager.GetActiveScene().name == "Scene4")
            scene4Controler = transform.Find("/Enemys").GetComponent<Scene4Controler>();
        isScene2AnimPlay = false;
        isScene3Pause = false;
        isScene4Pause = false;
        Scene3Identifer2Num = 0;
        Scene3GetItem = false;
}
    private void Start()
    {


        if (gameMode == GameMode.Normal)
        {
            gameManager = GameManager.Instance;
        }
        else if (gameMode == GameMode.Teaching)
        {
            gameManager = TeachGameManager.Instance;
        }
        isGameOver = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isTeachingMode)
        {
            if (totaltime < doorAnim.duration)
            {
                totaltime += Time.deltaTime;
                return;
            }
            if (canCountdown)
            {
                iniPos = model.transform.position;
                canCountdown = false;
                print(iniPos);
            }
        }
        if (scene3State == Scene3State.Pause)
        {
            tdir = Vector3.zero;
        }

        //判断是否开始计时
        if ((Vector3.Distance(model.transform.position, iniPos) > 0)&&(!isTeachingMode))
        {
            CountDown._Count = CountDown.IsCountOK.OK;
        }
        if (Input.GetMouseButtonDown(0) && (!isTeachingMode))
        {
            mouse1 = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && (!isTeachingMode))
        {
     
            ChangeTimeScale(0.1f);
            mouse2 = Input.mousePosition;
            if (Vector3.Distance(mouse1, mouse2) > 10)
            {
                show_line = true;
                Vector3 screenP = _camera.WorldToScreenPoint(rig.position);
                mouse1.z = screenP.z;
                mouse2.z = screenP.z;
                Vector3 dir1 = _camera.ScreenToWorldPoint(mouse1);
                Vector3 dir2 = _camera.ScreenToWorldPoint(mouse2);
                dir1.y = rig.position.y;
                dir2.y = rig.position.y;
                //屏幕鼠标确定小球方向
                dir = dir2 - dir1;

                dir = dir.normalized; //归一化的运动方向
                // Debug.DrawRay(rig.position, dir, Color.red);
            }
        }
        if (Input.GetMouseButtonUp(0) && (!isTeachingMode))
        {
            if (scene4Controler && scene4Controler.scene4AnimState == Scene4AnimState.End && !hasHitIdentifer5)
                ChangeTimeScale(.3f);
            else 
                ChangeTimeScale(1f);
            
            //松开鼠标赋予速度,取消指示线的显示
            if (scene4Controler&& (scene4Controler.scene4AnimState==Scene4AnimState.isPlaying|| scene4Controler.scene4AnimState == Scene4AnimState.End) && Vector3.Dot(dir, new Vector3(1, 0, 0)) > 0)
            {
                //tdir = Vector3.zero;
                return;
            }
            tdir = dir;
            show_line = false;
            trspeed = rspeed;
            tspeed = speed;
          //  JudgeIsBegin += 1;
        }
        if (isTeachingMode)
        {

            tdir = dir;
            trspeed = rspeed;
            tspeed = speed;
            rig.velocity = tdir * tspeed * factor;
        }
        //小球滚动前进
        rotate_dir = Vector3.Cross(Vector3.up, tdir);
        model.transform.Rotate(rotate_dir, Time.deltaTime * trspeed * 100, Space.World);
    }
    private void FixedUpdate()
    {
        if (!isTeachingMode)
            rig.velocity = tdir * tspeed;
    }
    public void OnCollisionEnter(Collision collision)
    {
        //计算完全弹性碰撞更新移动方向
        if (collision.gameObject.tag == "wall")
        {
            // StartCoroutine(Dunzhen());
            ContactPoint cp = collision.contacts[0];
            Vector3 rdir = Vector3.Reflect(tdir, cp.normal);
            rdir.y = 0;
            transform.DOScale(CalculateScaleMatrix(tdir, 0.5f) * tempscaleAnim, 0.1f);//碰撞时的缩放动画
            tdir = rdir.normalized;
            //碰撞特效
            Material wallmat = collision.gameObject.transform.GetComponent<Renderer>().material;
            if (wallmat != null)
            {
                wallmat.SetVector("_Center", cp.point);
                wallmat.SetFloat("_speed", 0f);
                wallmat.SetFloat("_GridEmission", 20f);
                wallmat.SetFloat("_width", 1f);
            }

        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (gameMode == GameMode.Normal)
            {
                if (!isGameOver)
                {
                    //gameManager.showGlitchAndOver(2.0f, 2.0f);
                    isGameOver = true;
                }
            }
            else if (gameMode == GameMode.Teaching)
            {
                //gameManager.showGlitch(2.0f);
            }
            gameManager.GameOver();
        }
        if(audioSource)
            audioSource.Play();
    }

    public void DropItem(Item item)
    {
        //item.transform.SetParent(transform);
        item.isGround = false;
        item.transform.position = new Vector3(transform.position.x, transform.position.y+.5f, transform.position.z);
        item.canBeTakenByPlayer = false;
        item.gameObject.SetActive(true);//通知书掉落前出现
        if (UIManager.Instance)
        {
            UIManager.Instance.SetBubbleUI("糟糕，掉了");
        }
        StartCoroutine(DropCoroutine(item));
    }
    IEnumerator DropCoroutine(Item item)
    {
        while (!item.isGround)
        {
            item.isGround = false;
            item.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(new Vector3(-dropDir.x, 10f, -dropDir.z))*9f);//通知书掉落身后
            item.GetComponent<Rigidbody>().mass += .002f;
            yield return null;
        }
        Transform FriendParent = transform.Find("/Enemys/Friend");
        iTween.Stop(FriendParent.gameObject);
        Transform friendTransform = FriendParent.GetChild(0);
        Transform ExclamationMark = FriendParent.GetChild(2);
        Transform QuestionMark = FriendParent.GetChild(3);
        QuestionMark.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameManager.Instance.FriendComeTo(friendTransform, item.transform); //添加第三关的跟踪对象
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            transform.DOScale(tempscaleAnim, 0.25f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "door")
        {
            if (SceneManager.GetActiveScene().name == "Scene3")
            {

            }
            if (gameManager.isFinished == true)
            {
                string sceneName = SceneManager.GetActiveScene().name;

                switch (sceneName)
                {
                    case "TeachingScene":
                        { JsonProcess.requestJson().updateTeachState(true);
                          JsonProcess.requestJson().updateIsFirstLogin(false);
                        } break;
                    case "Scene1": JsonProcess.requestJson().updateHomeState(true); break;
                    case "Scene2": JsonProcess.requestJson().updateSchoolState(true); break;
                    case "Scene3": JsonProcess.requestJson().updateCompanyState(true); break;
                    case "Scene4": JsonProcess.requestJson().updateHospitalState(true); break;
                    default:
                        break;
                }
                gameManager.Win();
            }
        }
        else if (other.tag == "Identifer")
        {
            if (gameMode == GameMode.Teaching&& SceneManager.GetActiveScene().name != "Scene4")
            {
                if (other.name == "Identifer1")
                {
                    TeachGameManager.Instance.teachBrain.routID = 0;
                    TeachGameManager.Instance.SetTeachingRobot(false, 0);
                    TeachGameManager.Instance.SetTeachingRobot(true, 0);
                    if (TeachGameManager.Instance.hitIdentiferNum == 0)
                    {
                        TeachGameManager.Instance.ShowHint("注意躲开敌人视野前进");
                        tdir = Vector3.zero;
                        TeachGameManager.Instance.hitIdentiferNum++;
                    }
                }
                if (other.name == "Identifer2")
                {
                    TeachGameManager.Instance.teachBrain.routID = 1;
                    TeachGameManager.Instance.SetTeachingRobot(false, 1);
                    TeachGameManager.Instance.SetTeachingRobot(true, 1);
                    if (TeachGameManager.Instance.hitIdentiferNum == 1)
                    {
                        TeachGameManager.Instance.ShowHint("参考灵体，躲开敌人，拿到关键发光道具", 6);
                        tdir = Vector3.zero;
                        TeachGameManager.Instance.hitIdentiferNum++;
                    }
                }
            }
            else
            {
                if (other.name == "Identifer1")
                {
                    if (SceneManager.GetActiveScene().name == "Scene3")
                    {
                        if (!Scene3GetItem)
                        {
                            for (int i = 0; i < Items.Count; i++)
                            {
                                if (Items[i].ItemName == "病危通知书")
                                {
                                    Scene3GetItem = true;
                                    Debug.Log("病危通知书");
                                    if (scene3State == Scene3State.PrePause)
                                    {
                                        dropDir = tdir;
                                        tdir = Vector3.zero;
                                        this.enabled = false;
                                        GameManager.Instance.Scene3Control(Items[i]); //掉落病危通知书
                                    }
                                    scene3State = Scene3State.Pause;
                                //    Items.Remove(Items[i]); //就不设定item减少了
                                }
                            }
                        }
                    }
                    //玩家到达该点时npc朝前走
                    if (SceneManager.GetActiveScene().name == "Scene4" && gameMode==GameMode.Normal && isScene4Pause)
                    {
                        if (scene4Controler.scene4AnimState==Scene4AnimState.NotStart)
                        {
                            tdir = Vector3.zero;
                            this.enabled = false;
                            GameManager.Instance.Scene4Control();
                        }
                        scene4Controler.scene4AnimState = Scene4AnimState.isPlaying;
                    }
                    
                }
                if (other.name == "Identifer2")
                {
                    if (SceneManager.GetActiveScene().name == "Scene2")
                    {
                        for (int i = 0; i < Items.Count; i++)
                        {
                            if (Items[i].name == "WaterCup")
                            {
                                if (!isScene2AnimPlay)
                                {
                                    tdir = Vector3.zero;
                                    this.enabled = false;
                                    GameManager.Instance.Scene2Control();
                                }
                                isScene2AnimPlay = true;
                            }
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Scene3")
                    {
                        Scene3Identifer2Num++;
                        if (Scene3Identifer2Num == 2)
                        {
                            transform.Find("/Enemys/Friend").GetChild(0).GetComponent<FriendBrain>().SetGuardsAlpha(1f, true, 1f);
                        }
                    }

                }
                //if (other.name == "Identifer5"&&hasHitIdentifer5==false)
                //{
                //    ChangeTimeScale(1f);
                //    hasHitIdentifer5 = true;
                //}
            }
        }
    }
    public void ChangeTimeScale(float val)
    {
        if (!gameManager)
        {
            Time.timeScale = val;
            Time.fixedDeltaTime = val * 0.02f;
            return;
        }
        if (!gameManager.isPause)
        {
            Time.timeScale = val;
            Time.fixedDeltaTime = val * 0.02f;
        }
    }
    IEnumerator Dunzhen()
    {
        ChangeTimeScale(0.05f);
        yield return new WaitForSecondsRealtime(0.005f);
        ChangeTimeScale(1f);
    }
    //沿任意轴缩放公式
    static Matrix4x4 CalculateScaleMatrix(Vector3 dir, float k)
    {
        Matrix4x4 ScaleMat = Matrix4x4.identity;
        ScaleMat.m00 = (1F + (k - 1F) * dir[0] * dir[0]);
        ScaleMat.m01 = ((k - 1F) * dir[0] * dir[1]);
        ScaleMat.m02 = ((k - 1F) * dir[0] * dir[2]);

        ScaleMat.m10 = ((k - 1F) * dir[0] * dir[1]);
        ScaleMat.m11 = (1F + (k - 1F) * dir[1] * dir[1]);
        ScaleMat.m12 = ((k - 1F) * dir[1] * dir[2]);

        ScaleMat.m20 = ((k - 1F) * dir[0] * dir[2]);
        ScaleMat.m21 = ((k - 1F) * dir[1] * dir[2]);
        ScaleMat.m22 = (1F + (k - 1F) * dir[2] * dir[2]);
        return ScaleMat;
    }
}