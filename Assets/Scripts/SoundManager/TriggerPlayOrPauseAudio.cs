using System;
using UnityEngine;
using UnityEngine.Video;

public class TriggerPlayOrPauseAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private VideoPlayer videoPlayer;
    
    public void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        audioSource.Pause();
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        audioSource.Play();
    }

    public void PauseOrPlayAudio()
    {
        if (videoPlayer.isPlaying)
        {
            audioSource.Pause();
        }
        else if(!videoPlayer.isPlaying && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
