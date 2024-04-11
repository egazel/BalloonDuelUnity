using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// by @kurtdekker - handy example of simple persistent settings.
//
// To use, just use the variables like any other:
//
//  SETTINGS.GlobalVolumeLevel = 1.0f;
//
//  if (SETTINGS.EnableTurboMode)
//  {
//      // Do turbo-mode stuff
//  }
//

public static class Settings
{
    private static string s_GlobalVolumeLevel = "GlobalVolumeLevel";
    public static float GlobalVolumeLevel
    {
        get
        {
            return PlayerPrefs.GetFloat(s_GlobalVolumeLevel, 1);
        }
        set
        {
            PlayerPrefs.SetFloat(s_GlobalVolumeLevel, value);
        }
    }

    private static string s_MusicVolumeLevel = "MusicVolumeLevel";
    public static float MusicVolumeLevel
    {
        get
        {
            return PlayerPrefs.GetFloat(s_MusicVolumeLevel, 1);
        }
        set
        {
            PlayerPrefs.SetFloat(s_MusicVolumeLevel, value);
        }
    }

    private static string s_SFXVolumeLevel = "SFXVolumeLevel";
    public static float SFXVolumeLevel
    {
        get
        {
            return PlayerPrefs.GetFloat(s_SFXVolumeLevel, 1);
        }
        set
        {
            PlayerPrefs.SetFloat(s_SFXVolumeLevel, value);
        }
    }

    /*private static string s_EnableTurboMode = "EnableTurboMode";
    public static bool EnableTurboMode
    {
        get
        {
            return PlayerPrefs.GetInt(s_EnableTurboMode, 1) != 0;
        }
        set
        {
            PlayerPrefs.SetInt(s_EnableTurboMode, value ? 1 : 0);
        }
    }*/
}