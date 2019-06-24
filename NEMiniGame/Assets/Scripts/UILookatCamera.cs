using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookatCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Camera cam;
    void Awake()
    {
        if (cam == null)
            cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = cam.transform.forward;
        transform.rotation = cam.transform.rotation;
    }
}
