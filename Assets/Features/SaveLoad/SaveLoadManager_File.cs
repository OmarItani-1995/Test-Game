using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager_File : SaveLoad_Base
{
    private Dictionary<string, string> _storage = new Dictionary<string, string>();

    private string FilePath => Application.persistentDataPath + "/savefile.txt";
    private bool loaded = false;
    void Save()
    {
        List<string> lines = new List<string>();
        foreach (var kvp in _storage)
        {
            lines.Add($"{kvp.Key}={kvp.Value}");
        }

        File.WriteAllLines(FilePath, lines);
    }
    
    void Load()
    {
        if (File.Exists(FilePath))
        {
            string[] lines = System.IO.File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    _storage[parts[0]] = parts[1];
                }
            }
        }

        loaded = true;
    }
    
    public override void SaveString(string key, string value)
    {
        if (!_storage.ContainsKey(key))
        {
            _storage.Add(key, value);
        }
        _storage[key] = value;
        Save();
    }

    public override void SaveInt(string key, int value)
    {
        if (!_storage.ContainsKey(key))
        {
            _storage.Add(key, value.ToString());
        }
        _storage[key] = value.ToString();
        Save();
    }

    public override void SaveFloat(string key, float value)
    {
        if (!_storage.ContainsKey(key))
        {
            _storage.Add(key, value.ToString());
        }
        _storage[key] = value.ToString();
        Save();
    }

    public override void SaveBool(string key, bool value)
    {
        if (!_storage.ContainsKey(key))
        {
            _storage.Add(key, value.ToString());
        }
        _storage[key] = value.ToString();
        Save();
    }

    public override string LoadString(string key, string defaultValue = "")
    {
        if (!loaded) Load();
        if (_storage.ContainsKey(key))
        {
            return _storage[key];
        }
        return defaultValue;
    }

    public override int LoadInt(string key, int defaultValue = 0)
    {
        if (!loaded) Load();
        if (_storage.ContainsKey(key))
        {
            if (int.TryParse(_storage[key], out int result))
            {
                return result;
            }
        }
        return defaultValue;
    }

    public override float LoadFloat(string key, float defaultValue = 0)
    {
        if (!loaded) Load();
        if (_storage.ContainsKey(key))
        {
            if (float.TryParse(_storage[key], out float result))
            {
                return result;
            }
        }
        return defaultValue;
    }

    public override bool LoadBool(string key, bool defaultValue = false)
    {
        if (!loaded) Load();
        if (_storage.ContainsKey(key))
        {
            if (bool.TryParse(_storage[key], out bool result))
            {
                return result;
            }
        }
        return defaultValue;
    }
}