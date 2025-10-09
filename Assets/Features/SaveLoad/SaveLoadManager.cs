using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// We use a bridge for the save load, it enables testing in editor + easy switching/adding between different save load methods
/// </summary>
public class SaveLoadManager : MonoBehaviour, ISaveLoad
{
    
    [SerializeField] private SaveLoad_Type _saveLoadType = SaveLoad_Type.PlayerPrefs;
    private SaveLoad_Base _saveLoad;
        
    void Awake()
    {
        DI.Register<ISaveLoad>(this);
        switch (_saveLoadType)
        {
            case SaveLoad_Type.PlayerPrefs:
                _saveLoad = new SaveLoadManager_PlayerPrefs();
                break;
            case SaveLoad_Type.File:
                _saveLoad = new SaveLoadManager_File();
                break;
            default:
                _saveLoad = new SaveLoadManager_PlayerPrefs();
                break;
        }
    }
    
    public void SaveString(string key, string value)
    {
        _saveLoad.SaveString(key, value);
    }

    public void SaveInt(string key, int value)
    {
        _saveLoad.SaveInt(key, value);
    }

    public void SaveFloat(string key, float value)
    {
        _saveLoad.SaveFloat(key, value);
    }

    public void SaveBool(string key, bool value)
    {
        _saveLoad.SaveBool(key, value);
    }

    public string LoadString(string key, string defaultValue = "")
    {
        return _saveLoad.LoadString(key, defaultValue);
    }

    public int LoadInt(string key, int defaultValue = 0)
    {   
        return _saveLoad.LoadInt(key, defaultValue);
    }

    public float LoadFloat(string key, float defaultValue = 0)
    {   
        return _saveLoad.LoadFloat(key, defaultValue);
    }

    public bool LoadBool(string key, bool defaultValue = false)
    {
        return _saveLoad.LoadBool(key, defaultValue);
    }
}

public enum SaveLoad_Type
{
    PlayerPrefs, File
}

public abstract class SaveLoad_Base
{
    public abstract void SaveString(string key, string value);
    public abstract void SaveInt(string key, int value);
    public abstract void SaveFloat(string key, float value);
    public abstract void SaveBool(string key, bool value);
    public abstract string LoadString(string key, string defaultValue = "");
    public abstract int LoadInt(string key, int defaultValue = 0);
    public abstract float LoadFloat(string key, float defaultValue = 0);
    public abstract bool LoadBool(string key, bool defaultValue = false);
}

public interface ISaveLoad
{
    void SaveString(string key, string value);
    void SaveInt(string key, int value);
    void SaveFloat(string key, float value);
    void SaveBool(string key, bool value);
    string LoadString(string key, string defaultValue = "");
    int LoadInt(string key, int defaultValue = 0);
    float LoadFloat(string key, float defaultValue = 0f);
    bool LoadBool(string key, bool defaultValue = false);
}
