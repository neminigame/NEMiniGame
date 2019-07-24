using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenManager : MonoBehaviour
{
    public GameMode gameMode = GameMode.Normal;
    public GameManagerBase gameManager;
    // Use this for initialization
    void Start()
    {
        if (gameMode == GameMode.Normal)
        {
            gameManager = GameManager.Instance;
        }
        else if (gameMode == GameMode.Teaching)
        {
            gameManager = TeachGameManager.Instance;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void quit()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f * 0.02f;
        Application.Quit();
    }
    public void reset()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f * 0.02f;
        GlobalManager.Instance.ChangeScene(0);

    }
    public void reset(int id)
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f * 0.02f;
        GlobalManager.Instance.ChangeScene(id);
    }
    public void Setpause()
    {
        gameManager.isPause = true;
        Time.timeScale = 0f;
    }
    public void SetRun()
    {
        gameManager.isPause = false;
        Time.timeScale = 1f;
    }
}
