using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed = 1.0f;
    public int direction = 1;
    public float durTime = 3f;//敌人持续同一个方向的时间
    private float _time = 0f;
    private bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateDirection();
        transform.Translate(Vector3.forward * Time.deltaTime * speed * direction);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAlive = false;
            Destroy(this.gameObject);
        }
    }
    void updateDirection()
    {
        if (Time.time - _time > durTime)
        {
            _time = Time.time;
            direction *= -1;
        }
    }
}
