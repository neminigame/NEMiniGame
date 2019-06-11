using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachingBrain : MonoBehaviour
{
    public PlayerControl playerControl;
    public line line;
    public List<Transform> posCollection;
    public int startID, endID;

    // Start is called before the first frame update
    void Start()
    {
        playerControl.isTeachingMode = true;
        line.isTeachingMode = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            posCollection.Add(transform.GetChild(i));
            posCollection[i].GetComponent<GuideManager>().ID = i;
        }
        playerControl.transform.position = posCollection[0].position;
        startID = 0;
        endID = 1;
    }

    // Update is called once per frame
    void Update()
    {
        var t = posCollection[endID].position - posCollection[startID].position;
        playerControl.dir = t.normalized;
    }
    public void ResetPos(int ID)
    {
        playerControl.transform.position = posCollection[ID].position;
    }
}
