using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    public AudioClip LobbyAudio;
    public AudioClip PlayAudio;


    public int preSceneNum = 0;
    public int curSceneNum = 0;
    private AudioSource audioSource;
    private static GlobalManager _Instance;
    public static GlobalManager Instance
    {
        get {
            if (_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<GlobalManager>().GetComponent<GlobalManager>();
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }
    public void setSceneNum(int id)
    {
        curSceneNum = id;
    }
    public void ChangeScene(int id)
    {
        preSceneNum = curSceneNum;
        curSceneNum = id;
        SceneManager.LoadScene(id);
        if (id != 0)
        {
            if(preSceneNum!=curSceneNum)
            {
                audioSource.clip = PlayAudio;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.clip = LobbyAudio;
            audioSource.loop = true;
            audioSource.Play();
        }

    }
    private void Awake()
    {
        if (_Instance == null)
            _Instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        curSceneNum = 0;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
