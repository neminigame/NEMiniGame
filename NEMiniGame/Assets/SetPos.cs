using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPos : MonoBehaviour
{
    public Transform CircleCenter;
    public List<Transform> stus;
    public List<Transform> catchers;
    public float radius;
    public float radius2;
    public float RotateAng;
    public Transform WaterCup;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains("Class2Man"))
            {
                var temp = transform.GetChild(i);
                stus.Add(temp);
            }
            else if (transform.GetChild(i).name.Contains("Class2Catcher"))
            {
                var temp = transform.GetChild(i);
                catchers.Add(temp);
            }
        }
        for (int i = 0; i < catchers.Count; i++)
        {
            catchers[i].position = CircleCenter.position + new Vector3(radius2 * Mathf.Cos(2 * Mathf.PI * i / 3f + speed * Time.deltaTime), 0, radius2 * Mathf.Sin(2 * Mathf.PI * i / 3f + speed * Time.deltaTime));
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < stus.Count; i++)
        {
            stus[i].position = CircleCenter.position + new Vector3(radius * Mathf.Cos(2 * Mathf.PI * i / 12f + RotateAng), 0, radius * Mathf.Sin(2 * Mathf.PI * i / 12f + RotateAng));
            stus[i].LookAt(CircleCenter);
        }
        if (WaterCup)
        {

            WaterCup.position = CircleCenter.position + new Vector3(radius * Mathf.Cos(2 * Mathf.PI / 12f + RotateAng), 0, radius * Mathf.Sin(2 * Mathf.PI * 11 / 12f + RotateAng));
            WaterCup.LookAt(CircleCenter);
        }
        for (int i = 0; i < catchers.Count; i++)
        {
            catchers[i].Translate(Vector3.forward * speed * Time.deltaTime);
            catchers[i].LookAt(CircleCenter);
            catchers[i].Rotate(Vector3.up,Mathf.PI/2);
        }
        //WaterCup.position = CircleCenter.position;

    }
}
