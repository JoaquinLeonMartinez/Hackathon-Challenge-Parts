using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    int index = 0;
    public AudioSource[] MyAudioSource;

    public AudioClip[] clips;

    public AudioClip clipPressButton;
    public AudioClip clipActivarPalanca;
    public AudioClip clipCogerPiezas;
    public AudioClip clipJump;
    public AudioClip clipScifiDoor;
    public AudioClip clipThunder3;
    public AudioClip clipGenerator;
    public AudioClip clipSlidinDoorStone;

    public bool activateSteps;
    public float stepSoundDuration = 0.2f;
    public float stepSoundTimer = 0.0f;

    void Start()
    {
        MyAudioSource = GetComponents<AudioSource>();
        activateSteps = false;
        stepSoundTimer = stepSoundDuration;
    }

    void Update()
    {
        if (activateSteps)
        {

            if (stepSoundTimer >= stepSoundDuration)
            {
                AudioClip clip = getRandomClip();
                GetAS().PlayOneShot(clip);
                stepSoundTimer = 0.0f;
                activateSteps = false;
            }
            else
            {
                stepSoundTimer += Time.deltaTime;
            }
        }
    }

    AudioSource GetAS()
    {
        index++;
        index %= MyAudioSource.Length;
        return MyAudioSource[index];
    }

    public void footstep(bool enabled_)
    {
        if(activateSteps && !enabled_)
            stepSoundTimer = stepSoundDuration;

        activateSteps = enabled_;
    }

    public AudioClip getRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void cogerPieza()
    {
        GetAS().PlayOneShot(clipCogerPiezas);
    }

    public void activarPalanca()
    {
        GetAS().PlayOneShot(clipActivarPalanca);
    }

    public void presionarBoton()
    {
        GetAS().PlayOneShot(clipPressButton);
    }

    public void playThunder3()
    {
        GetAS().PlayOneShot(clipThunder3);
    }

    public void playJump()
    {
        GetAS().PlayOneShot(clipJump);
    }

    public void playScifiDoor()
    {
        GetAS().PlayOneShot(clipScifiDoor);
    }

    public void playGenerator()
    {
        GetAS().PlayOneShot(clipGenerator);
    }

    public void playSlidinDoorStone()
    {
        GetAS().PlayOneShot(clipSlidinDoorStone);
    }
}
