using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
{
    public TeachingBrain teachingBrain;
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        teachingBrain = transform.parent.GetComponent<TeachingBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "TeachingRobot")
        {
            other.GetComponent<PlayerControl>().ChangeTimeScale(.2f);
            if (ID + 1 < teachingBrain.posCollection.Count)
            {
                teachingBrain.startID = ID;
                teachingBrain.endID = ID + 1;
            }
            else
            {
                teachingBrain.ResetPos(0);
                teachingBrain.startID = 0;
                teachingBrain.endID = 1;
            }
            other.GetComponent<PlayerControl>().show_line = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TeachingRobot")
        {
            other.GetComponent<PlayerControl>().ChangeTimeScale(1f);
            other.GetComponent<PlayerControl>().show_line = false;
        }
    }
}
