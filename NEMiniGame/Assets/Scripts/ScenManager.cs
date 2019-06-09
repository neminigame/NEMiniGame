using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void quit()
    {
        SceneManager.LoadScene(0);
    }
    public void reset()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f * 0.02f;
        SceneManager.LoadScene(1);
        
    }
}
