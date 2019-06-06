using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_change : MonoBehaviour
{
    private Material mat;
    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }
    private void Update()
    {
        if (mat != null)
        {
            mat.SetFloat("_speed", Mathf.Lerp(mat.GetFloat("_speed"), 3.0f, 0.05f));
            mat.SetFloat("_GridEmission", Mathf.Lerp(mat.GetFloat("_GridEmission"), 0f, 0.09f));
            mat.SetFloat("_width", Mathf.Lerp(mat.GetFloat("_width"), 0f, 0.03f));
            if (mat.GetFloat("_GridEmission") < 0.2f)
            {
                mat.SetFloat("_GridEmission", 0f);
                mat.SetFloat("_speed", 0f);
                mat.SetFloat("_width", 1f);
            }

        }
    }
}
