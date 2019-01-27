using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour
{

    VideoPlayer video;
    bool hasPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!video.isPlaying && hasPlayed){
            SceneManager.LoadScene("Main");
        }else if(video.isPlaying){
            hasPlayed = true;
        }
    }


}
