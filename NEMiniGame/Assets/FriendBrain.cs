using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FriendBrain : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;

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
                UIManager.Instance.SetBubbleUI("被他发现了，快跑！！");
                GameManager.Instance._playerControl.scene3State = Scene3State.AfterPause;
            }
            foreach (Image i in BoxLayout.GetComponentsInChildren<Image>())
            {
                if (i.sprite.name == "DangerInfo")
                {
                    i.sprite = EmptySlot;
                    i.color =new Color(i.color.r,i.color.g,i.color.b, 65f);
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
    void changeSymbolState(Transform ExclamationMark, Transform QuestionMark) //问号变感叹号
    {
        if (QuestionMark.gameObject.activeSelf)
        {
            QuestionMark.gameObject.SetActive(false);
            ExclamationMark.gameObject.SetActive(true);
        }
    }
}
