using UnityEngine;
using System.Collections;

public class LookAtCam : MonoBehaviour
{
    public bool UsePosition = false;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (UsePosition)
        {
            Vector3 vDir = cam.transform.position - transform.position;
            vDir.Normalize();
            transform.rotation = Quaternion.LookRotation(-vDir); 
        }
        else
        {
            transform.rotation = cam.transform.rotation;
        }
    }
}
