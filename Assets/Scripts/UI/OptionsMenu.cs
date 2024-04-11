using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;

    void Start()
    {
        _musicSlider.value = Settings.MusicVolumeLevel;
        _SFXSlider.value = Settings.SFXVolumeLevel;
    }

    public void SetMusicVolume()
    {
        Settings.MusicVolumeLevel = _musicSlider.value;
    }

    public void SetSFXVolume()
    {
        Settings.SFXVolumeLevel = _SFXSlider.value;
    }
}
