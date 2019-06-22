using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class test : MonoBehaviour
{
    public CinemachineBlendListCamera cmb;
    // Start is called before the first frame update
    void Start()
    {
        cmb = this.GetComponent<CinemachineBlendListCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
