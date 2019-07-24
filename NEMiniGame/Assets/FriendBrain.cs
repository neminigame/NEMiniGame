using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FriendBrain : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    public Transform Scene3npc18;
    public Material Scene3npc18MeshMat;
    public Material Scene3npc18CheckMat;
    public List<Transform> Scene3Guards;
    public List<Material> Scene3GuardsMeshMat;
    public List<Material> Scene3GuardsCheckMat;

    [SerializeField]
    private GameObject BoxLayout;

    private Sprite EmptySlot;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Scene3")
        {
            agent = transform.parent.GetComponent<NavMeshAgent>();
            EmptySlot = BoxLayout.transform.GetChild(0).GetComponent<Image>().sprite;
        }
        Scene3npc18 = transform.Find("/Enemys/Scene3npc18");
        Scene3npc18MeshMat = Scene3npc18.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial;
        Scene3npc18CheckMat = Scene3npc18.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial;
        Scene3npc18MeshMat.SetVector("_Color",Vector4.one);
        var t = Scene3npc18CheckMat.GetVector("_Color");
        Scene3npc18CheckMat.SetVector("_Color", new Vector4(t.x, t.y, t.z, .3f));
        for (int i = 0; i < Scene3Guards.Count; i++)
        {
            Scene3GuardsMeshMat.Add(Scene3Guards[i].GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial);
            Scene3GuardsCheckMat.Add(Scene3Guards[i].GetChild(1).GetComponent<MeshRenderer>().sharedMaterial);
            Scene3GuardsMeshMat[i].SetVector("_Color", Vector4.zero);
            var temp = Scene3GuardsCheckMat[i].GetVector("_Color");
            Scene3GuardsCheckMat[i].SetVector("_Color", new Vector4(temp.x, temp.y, temp.z, .0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled && SceneManager.GetActiveScene().name == "Scene3")
            SetDestination(target.position);
    }
    public void SetDestination(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
    private void OnTriggerEnter(Collider other) //处理碰撞病危通知书
    {

        if (other.gameObject.name == "病危通知书")
        {
            StartCoroutine(CatchPlayerCoroutine());
            other.gameObject.SetActive(false);
            Transform FriendParent = transform.Find("/Enemys/Friend");
            Transform ExclamationMark = FriendParent.GetChild(2);
            Transform QuestionMark = FriendParent.GetChild(3);
            changeSymbolState(ExclamationMark, QuestionMark);
            if (UIManager.Instance)
            {
                UIManager.Instance.SetBubbleUI("被他发现了，快跑！！门口的人消失了！！");
                SetGuardAlpha(0f, true, 1.5f);
                GameManager.Instance._playerControl.scene3State = Scene3State.AfterPause;
            }
            foreach (Image i in BoxLayout.GetComponentsInChildren<Image>())
            {
                if (i.sprite.name == "DangerInfo")
                {
                    i.sprite = EmptySlot;
                    i.color = new Color(i.color.r, i.color.g, i.color.b, 65f);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
    IEnumerator CatchPlayerCoroutine(float time = 1f) //目标设为player
    {
        yield return new WaitForSeconds(time);
        target = GameManager.Instance._playerControl.transform;
    }
    public void SetGuardMeshAlpha(float alpha, bool isLerp = false, float duration = 0)
    {
        var colorTemp = Scene3npc18MeshMat.GetColor("_Color");
        if (isLerp)
        {
            Scene3npc18MeshMat.DOColor(new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha), "_Color", duration);
        }
        else
            Scene3npc18MeshMat.SetColor("_Color", new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha));

    }
    public void SetGuardCheckAlpha(float alpha, bool isLerp = false, float duration = 0)
    {
        var colorTemp = Scene3npc18CheckMat.GetColor("_Color");
        if (isLerp)
        {
            Scene3npc18CheckMat.DOColor(new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha), "_Color", duration);
        }
        else
            Scene3npc18CheckMat.SetColor("_Color", new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha));
    }
    public void SetGuardAlpha(float alpha, bool isLerp = false, float duration = 0)
    {
        SetGuardMeshAlpha(alpha, isLerp, duration);
        SetGuardCheckAlpha(0.3f * alpha, isLerp, duration);
    }
    public void SetGuardsMeshAlpha(float alpha, bool isLerp = false, float duration = 0)
    {
        for (int i = 0; i < Scene3Guards.Count; i++)
        {
            var colorTemp = Scene3GuardsMeshMat[i].GetColor("_Color");
            if (isLerp)
            {
                Scene3GuardsMeshMat[i].DOColor(new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha), "_Color", duration);
            }
            else
                Scene3GuardsMeshMat[i].SetColor("_Color", new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha));
        }

    }
    public void SetGuardsCheckAlpha(float alpha, bool isLerp = false, float duration = 0)
    {
        for (int i = 0; i < Scene3Guards.Count; i++)
        {
            var colorTemp = Scene3GuardsCheckMat[i].GetColor("_Color");
            if (isLerp)
            {
                Scene3GuardsCheckMat[i].DOColor(new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha), "_Color", duration);
            }
            else
                Scene3GuardsCheckMat[i].SetColor("_Color", new Color(colorTemp.r, colorTemp.g, colorTemp.b, alpha));
        }
    }
    public void SetGuardsAlpha(float alpha, bool isLerp = false, float duration = 0)
    {
        SetGuardsMeshAlpha(alpha, isLerp, duration);
        SetGuardsCheckAlpha(0.3f * alpha, isLerp, duration);
    }

    void changeSymbolState(Transform ExclamationMark, Transform QuestionMark) //问号变感叹号
    {
        if (QuestionMark.gameObject.activeSelf)
        {
            QuestionMark.gameObject.SetActive(false);
            ExclamationMark.gameObject.SetActive(true);
        }
    }
}
