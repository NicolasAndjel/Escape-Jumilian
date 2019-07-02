using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NextClip : MonoBehaviour
{
    public VideoPlayer vp;
    public VideoClip vc;
    float timer;
    public 
    // Start is called before the first frame update
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 2.9)
        {
            vp.clip = vc;
            vp.Play();      
            vp.isLooping = true;
        }
        timer += Time.deltaTime;
    }
}
