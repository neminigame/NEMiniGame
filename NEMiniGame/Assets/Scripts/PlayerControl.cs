using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int JudgeIsBegin = 0;//判断是否第一次点鼠标，如果是则开始计时
    private Vector3 tdir;
    public List<Item> Items;
    public bool isTeachingMode = false;
    public line line;
    public float factor = 1f;
    public GameManagerBase gameManager;
    private Vector3 rotate_dir;
    private bool isGameOver=false;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        JudgeIsBegin = 0;
        _camera = Camera.main;
        rig = GetComponent<Rigidbody>();
        model = transform.Find("player").gameObject;
        Items.Clear();
        audioSource = GetComponent<AudioSource>();
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

        //判断是否开始计时
        if (JudgeIsBegin == 1)
        {
            if (isTeachingMode)
                return;
            CountDown._Count = CountDown.IsCountOK.OK;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (isTeachingMode)
                return;
            mouse1 = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            if (isTeachingMode)
                return;
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

                dir = dir.normalized;
                // Debug.DrawRay(rig.position, dir, Color.red);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isTeachingMode)
                return;
            ChangeTimeScale(1f);
            //松开鼠标赋予速度,取消指示线的显示
            tdir = dir;
            show_line = false;
            trspeed = rspeed;
            tspeed = speed;
            JudgeIsBegin += 1;
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
            if (!isGameOver)
            {
                gameManager.showGlitch(2.0f,2.0f);
                gameManager.GameOver();
                isGameOver = true;
            }
        }
        audioSource.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "door")
        {
            if (gameManager.isFinished == true)
            {
                gameManager.Win();
            }
        }
        else if (other.tag == "Identifer")
        {
            if (other.name == "Identifer1")
            {
                if (TeachGameManager.Instance.hitIdentiferNum == 0)
                {
                    TeachGameManager.Instance.ShowHint("注意躲开敌人视野前进");
                    tdir = Vector3.zero;
                    TeachGameManager.Instance.hitIdentiferNum++;
                }
            }
             if (other.name == "Identifer2")
            {
                TeachGameManager.Instance.SetTeachingRobot(true);
                if (TeachGameManager.Instance.hitIdentiferNum == 1)
                {
                    TeachGameManager.Instance.ShowHint("参考灵体，躲开敌人，拿到关键发光道具",6);
                    tdir = Vector3.zero;
                    TeachGameManager.Instance.hitIdentiferNum++;
                }
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
        if(!gameManager.isPause)
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
}