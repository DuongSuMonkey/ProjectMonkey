using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackAudio : IPlayBackAudio
{
    public void PlayAudio(AudioSource audioSource,AudioClip recordedClip)
    {
        audioSource.PlayOneShot(recordedClip);
    }
}
