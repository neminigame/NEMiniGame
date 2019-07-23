using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalManager : MonoBehaviour
{
    public AudioClip LobbyAudio;
    public AudioClip PlayAudio;
    

    public Text ECG;
    public Text SpO2;
    public Text NIBP;
    [Range(-3,3)]
    public float offset;
    public AudioMixer audioMixer;
    public int preSceneNum = 0;
    public int curSceneNum = 0;
    public AudioSource audioSource;
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
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        curSceneNum = 0;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (curSceneNum == 5)
        {
            if (!ECG)
            {
                ECG = transform.Find("/xdt/Canvas/Texts/ECG").GetComponent<Text>();
                SpO2 = transform.Find("/xdt/Canvas/Texts/SpO2").GetComponent<Text>();
                NIBP = transform.Find("/xdt/Canvas/Texts/NIBP").GetComponent<Text>();
            }
            float x = audioSource.time / audioSource.clip.length;
            float t = (float)(Math.Sin(Math.PI * x - offset));
            ECG.color = new Color(ECG.color.r,ECG.color.g,ECG.color.b, t);
            SpO2.color = new Color(SpO2.color.r, SpO2.color.g, SpO2.color.b, t);
            NIBP.color = new Color(NIBP.color.r, NIBP.color.g, NIBP.color.b, t);
            audioMixer.SetFloat("pitchBend", 1f / audioSource.pitch);   
        }
    }
}
