using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm_Handler : MonoBehaviour
{
    public AudioClip[] Music = new AudioClip[3];
    private int idx=0;
    AudioSource AS;

    void Awake()
    {
        AS = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!AS.isPlaying)
            RandomPlay();
    }

    void RandomPlay()
    {
        AS.clip = Music[idx];
        AS.Play();
        idx++;
        if(idx>2){
            idx=0;
        }
    }
}
