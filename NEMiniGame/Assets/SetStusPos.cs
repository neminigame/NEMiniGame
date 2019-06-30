using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStusPos : MonoBehaviour
{
    public Transform CircleCenter;
    public List<Transform> stus;
    public float radius;
    public float RotateAng;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains("Class2"))
            {
                var temp = transform.GetChild(i);
                stus.Add(temp);
            }
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
    }
}
