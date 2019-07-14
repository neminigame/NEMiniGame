using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.AI;

public class FriendBrain : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        agent = transform.parent.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        SetDestination(target.position);
    }
    public void SetDestination(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "病危通知书")
        {
            StartCoroutine(CatchPlayerCoroutine());
            other.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
    IEnumerator CatchPlayerCoroutine(float time = 1f)
    {
        yield return new WaitForSeconds(time);
        target = GameManager.Instance._playerControl.transform;
    }
}
