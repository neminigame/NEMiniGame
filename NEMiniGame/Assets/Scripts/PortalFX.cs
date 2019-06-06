using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFX : MonoBehaviour
{
    [HeaderAttribute("factor，弹性公式参数，用于调整回弹频率")]
    public float factor = 0.1f;
    [Range(0.01f,1.0f)]
    public float speed=1.0f;
    public bool canStartAnim=false;
    public float durTime;
    [HeaderAttribute("系数k，用来控制力的大小,1为标准值")]
    [Range(0f,5.0f)]
    public float k=1;
    private float _time=0;
    private Material _portalMaterial;
    private void Awake()
    {
        //Vector3 length = GetComponent<MeshFilter>().mesh.bounds.size;
        //float xlength = length.x * transform.lossyScale.x;
        //float ylength = length.y * transform.lossyScale.y;
        //float zlength = length.z * transform.lossyScale.z;
        //Debug.Log(length.x);
        //Debug.Log(length.y);
        //Debug.Log(length.z);
        _portalMaterial = GetComponent<MeshRenderer>().sharedMaterial;
    }
    private void Update()
    {
        if (canStartAnim)
        {
            _time += Time.deltaTime;
           //Debug.Log(GetInterpolation(_time));
            _portalMaterial.SetFloat("_Force", GetInterpolation(speed * _time));
            if (_time > durTime)
            {
                canStartAnim = false;
                _time = 0;
                _portalMaterial.SetFloat("_Force", 1f);
            }
        }
    }

    public float GetInterpolation(float input)
    {
        return (float)(k * Mathf.Pow(2, -10 * input) * Mathf.Sin((input - factor / 4) * (2 * Mathf.PI) / factor) + 1);
    }
}
