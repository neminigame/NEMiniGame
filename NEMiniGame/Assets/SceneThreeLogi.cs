using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneThreeLogi : MonoBehaviour
{
    [SerializeField]
    private GameObject resignPaper;
    [SerializeField]
    private scene3npcType eneyType;
    enum scene3npcType
    {
        npc1 = 1,
        npc2,
        npc3
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (eneyType == scene3npcType.npc1 || eneyType == scene3npcType.npc2)
            if (!resignPaper.activeSelf)
                gameObject.SetActive(false);


    }
}
