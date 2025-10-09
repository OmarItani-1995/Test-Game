using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager_PlayerPrefs : SaveLoad_Base
{
    public override void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public override void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public override void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public override void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public override string LoadString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public override int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public override float LoadFloat(string key, float defaultValue = 0)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public override bool LoadBool(string key, bool defaultValue = false)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }
}
