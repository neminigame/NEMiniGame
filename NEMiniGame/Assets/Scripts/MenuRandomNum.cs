using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuRandomNum : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text ecg, spo2, nibp;
    private Animator anim;
    private float time;
    void Awake()
    {
        ecg = transform.Find("ECG").GetComponent<Text>();
        spo2 = transform.Find("SpO2").GetComponent<Text>();
        nibp = transform.Find("NIBP").GetComponent<Text>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        ChangeRandomNum();
    }
    public void ChangeRandomNum()
    {
        int index = anim.GetLayerIndex("Base Layer");
        AnimatorClipInfo[] fadeclip = anim.GetCurrentAnimatorClipInfo(index);
        //Debug.Log(time + " "+ fadeclip[0].clip.length);
        if (time>fadeclip[0].clip.length*2)
        {
            ecg.text = Random.Range(60, 70).ToString();
            spo2.text = Random.Range(96, 99).ToString();
            nibp.text = Random.Range(98, 110).ToString() + "/" + Random.Range(68, 80).ToString();
            time = 0f;
        }

    }
}
