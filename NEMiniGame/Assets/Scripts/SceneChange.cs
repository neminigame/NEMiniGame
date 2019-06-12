using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void loadScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}
