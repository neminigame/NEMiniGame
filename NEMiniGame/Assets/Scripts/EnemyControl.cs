using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public enum EnemyType
    {
        Dad = 1,
        Mom,
        DD,
        Dog
    }
    public enum LoopType
    {
        None,
        Loop,
        Pinpong
    }
    public EnemyType enemyType;
    public float speed = 1.0f;
    [Tooltip("巡视后停留时长")]
    public float stayTime = 3f;//敌人持续同一个方向的时间
    public LoopType loopType; 
    public iTween.EaseType easeType;
    public GameObject player;
    private bool isAlive = true; 
    private Transform target;
    private bool isRevert=false;

    // Start is called before the first frame update
    void Start()
    {
        if (loopType == LoopType.Pinpong)
        {
            isRevert = !isRevert;
            transform.position = (Vector3)iTweenPath.GetPath(enemyType.ToString()).GetValue(0);
            iTween.MoveTo(gameObject, iTween.Hash(
                "path", iTweenPath.GetPath(enemyType.ToString()),
                "delay", stayTime,
                "easetype", easeType,
                "looptype", iTween.LoopType.none,
                "speed", speed,
                "orienttopath", true,
                "lookTime", 1.1,
                "axis", "y",
                "oncomplete", "myCompleteFun",
                "oncompletetarget", gameObject));
        }
        else
        {
            transform.position = (Vector3)iTweenPath.GetPath(enemyType.ToString()).GetValue(0);
            iTween.MoveTo(gameObject, iTween.Hash(
                "path", iTweenPath.GetPath(enemyType.ToString()),
                "delay", stayTime,
                "easetype", easeType,
                "looptype", loopType.ToString().ToLower(),
                "speed", speed,
                "orienttopath", true,
                "lookTime", 1.1,
                "axis", "y"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fanControl.checkFan(transform, player.transform))
        {
            Debug.Log("检测到");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAlive = false;
            Destroy(this.gameObject);
        }
    }

    public void myUpdateFunction()
    {
    }

    public void myCompleteFun()
    {
        RevertPath();
    }
    void RevertPath()
    {
        if (isRevert)
        {
            isRevert = !isRevert;
            iTween.MoveTo(gameObject, iTween.Hash(
            "path", iTweenPath.GetPathReversed(enemyType.ToString()),
            "delay", stayTime,
            "easetype", easeType,
            "looptype", loopType,
            "speed", speed,
            "orienttopath", true,
            "lookTime", 1.1,
            "axis", "y",
            "oncomplete", "myCompleteFun",
            "oncompletetarget", gameObject));
        }
        else {
            isRevert = !isRevert;
            iTween.MoveTo(gameObject, iTween.Hash(
            "path", iTweenPath.GetPath(enemyType.ToString()),
            "delay", stayTime,
            "easetype", easeType,
            "looptype", loopType,
            "speed", speed,
            "orienttopath", true,
            "lookTime", 1.1,
            "axis", "y",
            "oncomplete", "myCompleteFun",
            "oncompletetarget", gameObject));
        }
    }
}
