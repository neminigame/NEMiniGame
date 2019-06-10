using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanningLine : MonoBehaviour
{
    public Material TransitionMat;
    public bool canPlay=false;
    private float _time;
    public float speed = 1;
    public bool distort;
    private void Awake()
    {
        Initiate();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlay)
        {
            _time += Time.deltaTime;
            TransitionMat.SetFloat("_Cutoff", speed *_time);
            if (speed * _time > 1)
            {
                canPlay = false;
                _time = 0;
            }
        }
        TransitionMat.SetFloat("_Distort", Convert.ToInt32(distort));
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, TransitionMat);
    }
    private void Initiate()
    {
        if (!TransitionMat)
        {
            Shader shader = Shader.Find("JackieZhou/Distort");
            TransitionMat = new Material(shader);
        }
        TransitionMat.SetFloat("_Cutoff", 0);
        canPlay = false;
        _time = 0;
    }
}
