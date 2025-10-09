using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private Transform parent;
    private Queue<T> objects = new Queue<T>();
    private Func<T> OnCreate;
    public Pool(Func<T> OnCreatePrefab, int initialSize)
    {
        this.OnCreate = OnCreatePrefab;
        
        for (int i = 0; i < initialSize; i++)
        {
            var obj = OnCreatePrefab.Invoke();
            obj.gameObject.SetActive(false);
            objects.Enqueue(obj);
        }
    }

    public T Get()
    {
        if (objects.Count > 0)
        {
            var obj = objects.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var obj = OnCreate.Invoke();
            return obj;
        }
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }
}