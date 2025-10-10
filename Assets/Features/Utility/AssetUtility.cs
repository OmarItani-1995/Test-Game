using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class AssetUtility
{
    public static List<T> LoadAllAssetsOfType<T>() where T : Object
    {
        List<T> assets = new List<T>();
#if UNITY_EDITOR
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}"); // Find all assets of type MyScriptableObject
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            T so = AssetDatabase.LoadAssetAtPath<T>(path);
            assets.Add(so);
        }
#endif
        return assets;
    }
}
