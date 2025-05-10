using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("volume", 0.5f);
    }

    public void OnAudioVolumeUpdate()
    {
        if (audioSource == null) return;
        audioSource.volume = PlayerPrefs.GetFloat("volume", 0.5f);
    }
}
