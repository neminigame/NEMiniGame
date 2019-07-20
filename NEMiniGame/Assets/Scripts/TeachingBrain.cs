using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachingBrain : MonoBehaviour
{
    public PlayerControl playerControl;
    public line line;
    public List<List<Transform>> posCollections;
    public List<Transform> posCollection1;
    public List<Transform> posCollection2;
    public int startID, endID;
    public int routID;
    // Start is called before the first frame update
    void Awake()
    {
        playerControl.isTeachingMode = true;
        if(line)
            line.isTeachingMode = true;
        posCollections = new List<List<Transform>>();

        for (int j = 0; j < transform.childCount; j++)
        {
            posCollections.Add(new List<Transform>());
            for (int i = 0; i < transform.GetChild(j).childCount; i++)
            {
                posCollections[j].Add(transform.GetChild(j).GetChild(i));
                posCollections[j][i].GetComponent<GuideManager>().ID = i;
                posCollections[j][i].GetComponent<GuideManager>().playerControl = playerControl;
            }
        }
        posCollection1 = posCollections[0];
        if (transform.childCount > 1)
            posCollection2 = posCollections[1];

        playerControl.transform.position = posCollections[routID][0].position;
        startID = 0;
        endID = 1;
    }

    // Update is called once per frame
    void Update()
    {
        var t = (posCollections[routID][endID].position - playerControl.transform.position).normalized;
        playerControl.dir = t;
    }
    public void ResetPos(int routeID,int ID)
    {
        playerControl.transform.position = posCollections[routID][ID].position;
    }
}
