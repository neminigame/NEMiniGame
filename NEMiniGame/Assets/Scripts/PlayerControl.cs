using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Camera _camera;
    public GameObject model;
    public Rigidbody rig;
    public Vector3 dir;
    public float speed, rspeed;
    public bool show_line;
    private float _timeScale = 0.1f;
    private float tspeed, trspeed;//前进速度，旋转速度
    private Vector3 mouse1, mouse2;//第一次鼠标，第二次鼠标位置
    private Vector3 tdir;
    public List<Item> Items;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        rig = GetComponent<Rigidbody>();
        model = transform.Find("player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouse1 = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            ChangeTimeScale(0.1f);
            mouse2 = Input.mousePosition;
            if(Vector3.Distance(mouse1,mouse2)>10)
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
                Debug.DrawRay(rig.position, dir, Color.red);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ChangeTimeScale(1f);
            //松开鼠标赋予速度,取消指示线的显示
            tdir = dir;
            show_line = false;
            trspeed = rspeed;
            tspeed = speed;
        }
        //小球滚动前进
        Vector3 rotate_dir = Vector3.Cross(Vector3.up,tdir);
        model.transform.Rotate(rotate_dir,Time.deltaTime*trspeed*100,Space.World);
    }
    private void FixedUpdate()
    {
        rig.velocity = tdir * tspeed;
    }
    public void OnCollisionEnter(Collision collision)
    {
        //计算完全弹性碰撞更新移动方向
        if (collision.gameObject.tag == "wall")
        {
            ContactPoint cp = collision.contacts[0];
            Vector3 rdir = Vector3.Reflect(tdir, cp.normal);
            rdir.y = 0;
            tdir = rdir.normalized;
        }
        else if (collision.gameObject.tag == "StartAndEndPos")
        {
            if (GameManager.Instance.isFinished == true)
            {
                GameManager.Instance.Win();
            }
        }
    }
    void ChangeTimeScale(float val)
    {
        Time.timeScale = val;
        Time.fixedDeltaTime = val*0.02f;
    }
}
