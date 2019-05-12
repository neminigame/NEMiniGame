using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Camera _camera;
    public Rigidbody rig;
    public Vector3 dir;
    public float speed, rspeed;
    public bool show_line;
    private float tspeed, trspeed;//前进速度，旋转速度
    private Vector3 mouse1, mouse2;//第一次鼠标，第二次鼠标位置
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        rig = GetComponentInChildren<Rigidbody>();
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
                tspeed = trspeed=0;//为了测试把速度变为0,本来应该是用子弹时间做
                Debug.DrawRay(rig.position, dir, Color.red);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //松开鼠标赋予速度,取消指示线的显示
            show_line = false;
            trspeed = rspeed;
            tspeed = speed;
        }
        //小球滚动前进
        Vector3 rotate_dir = Vector3.Cross(Vector3.up,dir);
        transform.Rotate(rotate_dir * trspeed,Space.World);
    }
    private void FixedUpdate()
    {
        rig.velocity = dir * tspeed;
    }
    public void OnCollisionEnter(Collision collision)
    {
        //计算完全弹性碰撞更新移动方向
        if(collision.gameObject.tag=="wall")
        {
           ContactPoint cp = collision.contacts[0];
            Vector3 rdir = Vector3.Reflect(dir, cp.normal);
            rdir.y = 0;
            dir = rdir.normalized;
        }
    }
}
