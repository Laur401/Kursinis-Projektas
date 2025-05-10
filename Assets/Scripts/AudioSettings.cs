using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] List<Slider> audioSliders;
    void Start()
    {
        audioSource = FindAnyObjectByType<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("volume", 0.5f);
        foreach (var s in audioSliders)
            s.value = audioSource.volume;
    }

    public void OnAudioVolumeUpdate(Single value)
    {
        PlayerPrefs.SetFloat("volume", value);
        if (audioSource == null) return;
        audioSource.volume = PlayerPrefs.GetFloat("volume", 0.5f);
    }
    
}
