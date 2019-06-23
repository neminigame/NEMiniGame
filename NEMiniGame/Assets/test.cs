using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class test : MonoBehaviour
{

    public List<List<Transform>> posCollections;
    public List<Transform> posCollection1;
    public List<Transform> posCollection2;

    // Start is called before the first frame update
    void Start()
    {
        posCollections = new List<List<Transform>>();
        posCollections.Add(new List<Transform>());
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            posCollections[0].Add(transform.GetChild(0).GetChild(i));
            posCollection1.Add(posCollections[0][i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
