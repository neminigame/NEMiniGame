using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOver;
    public float speed;
    public bool isFinished;//是否收集齐
    [SerializeField]
    private PlayerControl _playerControl;
    [SerializeField]
    private CountDown _countDown;
    private Material _textMeshProUGUIMat;
    void Awake()
    {
        Instance = this;
        _textMeshProUGUIMat = gameOver.GetComponent<TextMeshProUGUI>().fontMaterial;
        InitialGameMang();
        _playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        //_countDown = transform.Find("Canvas").GetComponent<CountDown>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameOver.GetComponent<TextMeshProUGUI>().fontMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        _playerControl.enabled = false;
        _countDown.CountStop();
        _textMeshProUGUIMat.SetFloat("_OutlineSoftness", Mathf.Lerp(_textMeshProUGUIMat.GetFloat("_OutlineSoftness"), 0f, Time.deltaTime * speed));
        _textMeshProUGUIMat.SetFloat("_FaceDilate", Mathf.Lerp(_textMeshProUGUIMat.GetFloat("_FaceDilate"), 0f, Time.deltaTime * speed));
    }
    public void InitialGameMang()
    {
        gameOver.SetActive(false);
        _textMeshProUGUIMat.SetFloat("_OutlineSoftness", 1f);
        _textMeshProUGUIMat.SetFloat("_FaceDilate", -1f);
        isFinished = false;
    }
    public void Win()
    {
        GameOver();
    }
}
