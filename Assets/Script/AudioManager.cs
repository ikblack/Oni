using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
   
    public static AudioManager instance;
    public bool bisPlayOnce;
    void Start () {
        instance = this;
        //play("Main",1f);
        //isLoop(true);
        DontDestroyOnLoad(this);
    }

    public void isLoop(bool isloop) {
        if (isloop)
        {
          this.GetComponent<AudioSource>().loop = true;
        }else
           this.GetComponent<AudioSource>().loop = false;

    }
    public void Pause()
    {
        this.GetComponent<AudioSource>().Pause();     
    }

    public void play()//默认播放BGM
    {
        this.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/BGM" );
        this.GetComponent<AudioSource>().Play();
    }
    public  void play(string musicName,float volume)
    {
        this.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/"+musicName);
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<AudioSource>().volume=volume;
    }
    public void delayPlay(string musicName,float time)
    {
        Invoke("play",time);
     
    }
    void OnGUI()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    play("level",1f);
        //    isLoop(false);
        //}
        if (Input.GetKeyDown(KeyCode.A))
        {
            play("BGM",1f);
            isLoop(true);

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            play("buff",1f);
            isLoop(false);
        }
    }



}
