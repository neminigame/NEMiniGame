using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject[] floors;
    public Transform target;
    public float[] xSizes;
    private float size=0;
    CinemachineTrackedDolly ctd;
    private float num;
    private void Awake()
    {
        xSizes = new float[floors.Length];
        for (int i = 0; i < floors.Length; i++)
        {
            xSizes[i] = floors[i].GetComponent<MeshFilter>().mesh.bounds.size.x * floors[i].transform.localScale.x;
            size += floors[i].GetComponent<MeshFilter>().mesh.bounds.size.x * floors[i].transform.localScale.x;
        }
        num = floors.Length;
    }
    // Start is called before the first frame update
    void Start()
    {
        ctd = gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    // Update is called once per frame
    void Update()
    {
        ctd.m_PathPosition = target.position.x * num / size;
    }
}
