using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class DI : MonoBehaviour
{
    private static DI instance = null;
    private static DI Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject diObject = new GameObject("DI");
                instance = diObject.AddComponent<DI>();
            }
            return instance;
        }
    }
    
    private Dictionary<Type, Object> _services = new Dictionary<Type, Object>();
    
    public static void Register<T>(T service) where T : class
    {
        var type = typeof(T);
        if (Instance._services.ContainsKey(type))
        {
            Debug.LogWarning($"Service of type {type} is already registered. Overwriting.");
        }
        Instance._services[type] = service;
    }
    
    public static T Get<T>() where T : class
    {
        var type = typeof(T);
        if (Instance._services.TryGetValue(type, out var service))
        {
            return service as T;
        }
        Debug.LogError($"Service of type {type} is not registered.");
        return null;
    }
}
