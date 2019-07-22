using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuideManager : MonoBehaviour
{
    public TeachingBrain teachingBrain;
    public PlayerControl playerControl;
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        teachingBrain = transform.parent.parent.GetComponent<TeachingBrain>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TeachingRobot")
        {
            if (ID + 1 < teachingBrain.posCollections[teachingBrain.routID].Count)
            {
                teachingBrain.startID = ID;
                teachingBrain.endID = ID + 1;
            }
            else
            {
                teachingBrain.ResetPos(teachingBrain.routID, 0);
                teachingBrain.startID = 0;
                teachingBrain.endID = 1;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "TeachingRobot")
        {
            //other.GetComponent<PlayerControl>().ChangeTimeScale(.2f);     
            playerControl.factor = .2f;
            playerControl.show_line = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TeachingRobot")
        {
            playerControl.factor = 1f;
            if(SceneManager.GetActiveScene().name != "Scene4")
                playerControl.ChangeTimeScale(1f);

            playerControl.show_line = false;
        }
    }
}
