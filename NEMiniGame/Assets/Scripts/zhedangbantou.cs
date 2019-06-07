using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zhedangbantou : MonoBehaviour {

    public class mxr {
        public Material[] materials = null;
 
        public Material[] shadermaterials = null;
        public float ctime = 0.0f;
        public bool flag = false;
      
    }
    public Transform[] targetobject = null;
    public float TranspartentPower = 0.2f;
    public float fadespeed = 1f;
    private Dictionary<Renderer, mxr> hyx = new Dictionary<Renderer, mxr>();
    private List<Renderer> tt = new List<Renderer>();
    private int judgelayer;
	// Use this for initialization
	void Start () {
        judgelayer = 1 << LayerMask.NameToLayer("wall");
	}
	
	// Update is called once per frame
	void Update () {
        if (targetobject.Length<1) return;
        SetTransparent();
        UpdateTransparent();
        ClearTransparent();
	}
    public void SetTransparent()
    {
        var it=hyx.GetEnumerator();
        while(it.MoveNext())
        {
            mxr temp = it.Current.Value;
            temp.flag = false;
            foreach(var t in temp.materials)
            {
                Color c = t.GetColor("_Color");
                temp.ctime += Time.unscaledDeltaTime;
                c.a = Mathf.Lerp(1, TranspartentPower, temp.ctime*fadespeed);
                t.SetColor("_Color", c);
            }
        }
    }
    public void UpdateTransparent()
    {
        RaycastHit[] hit;
        for(int i=0;i< targetobject.Length;i++)
        {
            Vector3 viewdir = (targetobject[i].position - transform.position).normalized;
            float dis = Vector3.Distance(transform.position, targetobject[i].position);
            hit = Physics.RaycastAll(new Ray(transform.position, viewdir), dis, judgelayer);
            foreach (var t in hit)
            {
                Renderer[] render = t.transform.GetComponentsInChildren<Renderer>();
                foreach (var it in render)
                {
                    mxr parm = null;
                    hyx.TryGetValue(it, out parm);
                    if (parm == null)
                    {
                        parm = new mxr();
                        //如果先调用materials,后调用的shadermaterial是调用materials就会生成的那个。
                        //https://blog.csdn.net/xxxwuyufan/article/details/82181248
                        parm.shadermaterials = it.sharedMaterials;
                        parm.materials = it.materials;
                        hyx.Add(it, parm);
                        foreach (var c in parm.materials)
                            c.shader = Shader.Find("MiniGame/toon_alpha");
                    }

                    parm.flag = true;
                }
            }
        }
 
    }
    public void ClearTransparent()
    {
        tt.Clear();
        var it = hyx.GetEnumerator();
        while(it.MoveNext())
        {
            mxr parm = it.Current.Value;
            if (parm.flag==false)
            {
                it.Current.Key.materials = parm.shadermaterials;
               
                tt.Add(it.Current.Key);
            }
        }
        foreach (var c in tt)
            hyx.Remove(c);
    }
}
