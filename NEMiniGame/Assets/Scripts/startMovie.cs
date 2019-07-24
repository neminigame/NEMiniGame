using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class startMovie : MonoBehaviour
{
    public RenderTexture movieTexture;
    public VideoPlayer starVideo;
    // Start is called before the first frame update
    void Awake()
    {
        movieTexture = new RenderTexture(512, 512, 1, RenderTextureFormat.Default);
        starVideo = GetComponent<VideoPlayer>();
        starVideo.targetTexture = movieTexture;
        GetComponent<RawImage>().texture = movieTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if(starVideo.time>=starVideo.length)
        {
<<<<<<< HEAD
            GlobalManager.Instance.ChangeScene(1);//动画播放完毕进入教学关
=======
            GlobalManager.Instance.ChangeScene(5);//动画播放完毕进入教学关
>>>>>>> 4829de7940421594e02d45864da37f8f7a094205
            GlobalManager.Instance.audioSource.mute = false;
        }
    }
}
