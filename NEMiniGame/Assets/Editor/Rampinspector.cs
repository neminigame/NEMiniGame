using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
enum tex_type { Horizontal, Vertical };
[CustomEditor(typeof(CreatRamp))]
public class Rampinspector : Editor
{
    private Texture2D tex;
    private string s, tex_name;
    private tex_type e;
    private CreatRamp m;
    private int width;

    private void OnEnable()
    {
        m = (CreatRamp)target;
        width = 512;
        s = "512";
        tex_name = "ramp1";
    }
    public override void OnInspectorGUI()
    {

        draw();
        if (GUILayout.Button("生成"))
        {
            switch (e)
            {
                case tex_type.Horizontal:
                    tex = new Texture2D(width, 1, TextureFormat.RGBA32, false);
                    for (int i = 0; i < width; i++)
                        tex.SetPixel(i, 1, m.gardient.Evaluate((float)i / (width - 1)));
                    break;
                case tex_type.Vertical:
                    tex = new Texture2D(1, width, TextureFormat.RGBA32, false);
                    for (int i = 0; i < width; i++)
                        tex.SetPixel(1, i, m.gardient.Evaluate((float)i / (width - 1)));
                    break;
            }
            tex.Apply();
            byte[] by = tex.EncodeToPNG();
            string path = "Assets/" + tex_name + ".png";
            if (by.Length > 0)
            {
                File.WriteAllBytes(path, by);
            }
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("删除脚本"))
        {
            DestroyImmediate(m);
        }
    }
    void draw()
    {
        base.OnInspectorGUI();
        e = (tex_type)EditorGUILayout.EnumPopup("贴图方向", e);
        GUILayout.BeginHorizontal();
        GUILayout.Label("贴图长度:");
        s = GUILayout.TextField(s);
        width = int.Parse(s);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("贴图名称:");
        tex_name = GUILayout.TextField(tex_name);
        GUILayout.EndHorizontal();
    }
}
