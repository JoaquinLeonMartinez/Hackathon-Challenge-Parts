using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Track
{
    public AudioClip Intro;
    public AudioClip Body;
}

public class SoundtrackManager : MonoBehaviour
{
    [SerializeField]
    public Track[] Tracks;
    AudioSource audioSource;
    int CurrentTrack = 0;
    bool intro = false;
    float timecount = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayTrack(0);
    }

    public void PlayTrack(int trackNumber)
    {
        CurrentTrack = trackNumber;

        audioSource.clip = Tracks[trackNumber].Intro;
        audioSource.Play();
        audioSource.loop = false;
        intro = true;
        timecount = 0;
    }

    private void Update()
    {
        if (intro)
        {
            timecount += Time.deltaTime;
            if(timecount >= audioSource.clip.length)
            {
                audioSource.clip = Tracks[CurrentTrack].Body;
                audioSource.loop = true;
                intro = false;
            }
        }
    }
}
