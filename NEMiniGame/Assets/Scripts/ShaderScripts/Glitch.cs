using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch : MonoBehaviour
{
    [Range(0,1)]
    public float strength, speed;
    [Range(0,3)]
    public int downsample;
    public Shader Gshader;
    private Material Gmat;
    private void Awake()
    {
        strength = 0.05f;
        speed = 0.01f;
        Gshader = Shader.Find("MiniGame/Glitch");
        if(Gshader!=null)
            Gmat = new Material(Gshader);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (Gmat != null)
        {
            Gmat.SetFloat("_strength", strength);
            Gmat.SetFloat("_speed", speed);
            int w = source.width >> downsample;
            int h = source.height >> downsample;
            RenderTexture temp = RenderTexture.GetTemporary(w, h);
            Graphics.Blit(source, temp);
            Graphics.Blit(temp, destination,Gmat);
            RenderTexture.ReleaseTemporary(temp);
        }
        else Graphics.Blit(source, destination);
    }
}
