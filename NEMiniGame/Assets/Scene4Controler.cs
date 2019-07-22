using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Scene4AnimState
{
    NotStart,
    isPlaying,
    End
}
public class Scene4Controler : MonoBehaviour
{
    public EnemyControl dadControl, momControl, ddControl, friendControl;
    public Scene4AnimState scene4AnimState = Scene4AnimState.NotStart;
    Material dadMat, momMat, ddMat, friendMat;
    Transform dad, mom, dd, friend;
    public float distance = 4f;
    // Start is called before the first frame update
    void Start()
    {
        dad = transform.Find("Dad");
        mom = transform.Find("Mom");
        dd = transform.Find("DD");
        friend = transform.Find("Friend");
        dadControl = transform.Find("Dad").GetComponent<EnemyControl>();
        momControl = transform.Find("Mom").GetComponent<EnemyControl>();
        ddControl = transform.Find("DD").GetComponent<EnemyControl>();
        friendControl = transform.Find("Friend").GetComponent<EnemyControl>();
        dadMat = dadControl.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial;
        momMat = momControl.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial;
        ddMat = ddControl.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial;
        friendMat = friendControl.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial;
        dadMat.SetVector("_Color", Vector4.one);
        momMat.SetVector("_Color", Vector4.one);
        ddMat.SetVector("_Color", Vector4.one);
        friendMat.SetVector("_Color", Vector4.one);
        scene4AnimState = Scene4AnimState.NotStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (scene4AnimState == Scene4AnimState.End)
        {
            var t = dadMat.GetVector("_Color");
            dadMat.SetVector("_Color", new Vector4(t.x, t.y, t.z, Mathf.Clamp(GameManager.Instance._playerControl.transform.position.x - dad.position.x, 0.0f, 1.0f)));
            t = momMat.GetVector("_Color");
            momMat.SetVector("_Color", new Vector4(t.x, t.y, t.z, Mathf.Clamp(GameManager.Instance._playerControl.transform.position.x - dad.position.x, 0.0f, 1.0f)));
            t = ddMat.GetVector("_Color");
            ddMat.SetVector("_Color", new Vector4(t.x, t.y, t.z, Mathf.Clamp(GameManager.Instance._playerControl.transform.position.x - dad.position.x, 0.0f, 1.0f)));
            t = friendMat.GetVector("_Color");
            friendMat.SetVector("_Color", new Vector4(t.x, t.y, t.z, Mathf.Clamp(GameManager.Instance._playerControl.transform.position.x - dad.position.x, 0.0f, 1.0f)));
        }
    }
    public void StartAnim()
    {
        dadControl.StartAnim();
        momControl.StartAnim();
        ddControl.StartAnim();
        friendControl.StartAnim();
        scene4AnimState = Scene4AnimState.isPlaying;
    }
}
